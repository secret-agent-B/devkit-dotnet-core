// -----------------------------------------------------------------------
// <copyright file="SubmitOrderCommandFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Fakers
{
    using System;
    using System.Linq;
    using Devkit.Test;
    using Logistics.Orders.API.Business.Orders.Commands.UpdateOrder;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// The SubmitOrderFaker generates instances of SubmitOrderCommands.
    /// </summary>
    public class UpdateOrderCommandFaker : FakerBase<UpdateOrderCommand>
    {
        /// <summary>
        /// Generates the specified count.
        /// </summary>
        /// <returns>
        /// A list of T.
        /// </returns>
        public override UpdateOrderCommand Generate()
        {
            this.Faker
                .RuleFor(x => x.UserName, f => f.Person.UserName)
                .RuleFor(x => x.RecipientName, f => f.Person.FullName)
                .RuleFor(x => x.RecipientPhone, f => f.Person.Phone)
                .RuleFor(x => x.ItemName, f => f.Commerce.ProductName())
                .RuleFor(x => x.Destination, f => new LocationVM
                {
                    DisplayAddress = f.Address.FullAddress(),
                    Lat = f.Address.Latitude(),
                    Lng = f.Address.Longitude()
                })
                .RuleFor(x => x.EstimatedDistance, f => new DistanceVM
                {
                    Text = $"{Math.Round(f.Random.Double(1, 10), 2) } km",
                    Value = f.Random.Int(100, 1000),
                })
                .RuleFor(x => x.EstimatedItemWeight, f => f.Random.Double(1, 10))
                .RuleFor(x => x.ItemPhoto, $"image/jpeg;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs=")
                .RuleFor(x => x.Origin, f => new LocationVM
                {
                    DisplayAddress = f.Address.FullAddress(),
                    Lat = f.Address.Latitude(),
                    Lng = f.Address.Longitude()
                })
                .RuleFor(x => x.OriginPhoto, $"image/jpeg;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs=")
                .RuleFor(x => x.RequestSignature, f => f.PickRandom(new[] { true, false }))
                .RuleFor(x => x.RequestInsulation, f => f.PickRandom(new[] { true, false }))
                .RuleFor(x => x.SpecialInstructions, f => f.Rant.Reviews(f.Commerce.ProductName(), f.Random.Int(1, 5))
                    .Select(x => new SpecialInstructionVM
                    {
                        Description = x,
                        IsCompleted = false
                    }).ToList());

            var order = this.Faker.Generate();
            var split = order.EstimatedDistance.Text.Split(' ');
            var distance = Math.Round(double.Parse(split[0]), 2);
            var deliveryCost = new DeliveryCostVMFaker().Generate(distance); order.Cost = deliveryCost;

            order.Cost = deliveryCost;

            return order;
        }
    }
}