// -----------------------------------------------------------------------
// <copyright file="OrderBusExtension.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.ServiceBus.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Communication.Security.DTOs;
    using Devkit.Communication.Security.Messages;
    using Devkit.ServiceBus.Exceptions;
    using Devkit.ServiceBus.Interfaces;
    using Logistics.Orders.API.Business.ViewModels;
    using MassTransit;
    using Newtonsoft.Json.Linq;
    using Serilog;

    /// <summary>
    /// The GetUserHelper is a utility class for getting user information from the service bus.
    /// </summary>
    public static class OrderBusExtension
    {
        /// <summary>
        /// Sets the user infos.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="getUsersClient">The get users client.</param>
        /// <returns>A task.</returns>
        public static async Task SetUserInfos(this OrderVM order, IRequestClient<IGetUsers> getUsersClient)
        {
            var tmpOrder = new List<OrderVM>
            {
                order
            };

            await tmpOrder.SetUserInfos(getUsersClient);
        }

        /// <summary>
        /// Sets the user infos.
        /// </summary>
        /// <param name="orders">The orders.</param>
        /// <param name="getUsersClient">The get users client.</param>
        public static async Task SetUserInfos(this IList<OrderVM> orders, IRequestClient<IGetUsers> getUsersClient)
        {
            if (!orders.Any())
            {
                return;
            }

            // Get distinct user names
            var userNames = orders
                .Select(x => x.ClientUserName)
                .Union(orders.Select(x => x.DriverUserName))
                .Union(orders.SelectMany(x => x.Statuses).Select(x => x.UserName))
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            // Get user information from service bus
            var (usersResponse, requestException) = await getUsersClient.GetResponse<IListResponse<IUserDTO>, IConsumerException>(new
            {
                UserNames = userNames
            });

            if (!usersResponse.IsCompletedSuccessfully)
            {
                Log.Logger.Error((await requestException).Message.ErrorMessage);
            }

            var users = (await usersResponse).Message;

            foreach (var order in orders)
            {
                // Assign clients
                var clientUser = users.Items.SingleOrDefault(x => x.UserName == order.ClientUserName);

                if (clientUser != null)
                {
                    order.Client = JObject.FromObject(clientUser);
                }

                // Try to skip is driver information does not exist
                if (!string.IsNullOrEmpty(order.DriverUserName))
                {
                    // Assign drivers
                    var driverUser = users.Items.SingleOrDefault(x => x.UserName == order.DriverUserName);

                    if (driverUser != null)
                    {
                        order.Driver = JObject.FromObject(driverUser);
                    }
                }

                // Assign statuses
                foreach (var status in order.Statuses)
                {
                    var user = users.Items.SingleOrDefault(x => x.UserName == status.UserName);

                    if (user != null)
                    {
                        status.User = JObject.FromObject(user);
                    }
                }
            }
        }
    }
}