// -----------------------------------------------------------------------
// <copyright file="OrderFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Fakers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using Devkit.Test;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using MongoDB.Driver.GeoJsonObjectModel;

    /// <summary>
    /// The OrderFaker creates fake orders.
    /// </summary>
    public class OrderFaker : FakerBase<Order>
    {
        /// <summary>
        /// Generates the specified count.
        /// </summary>
        /// <returns>
        /// A list of T.
        /// </returns>
        public override Order Generate()
        {
            var clientUserName = new Faker().Person.Email;

            this.Faker
                .RuleFor(x => x.ItemName, f => f.Commerce.ProductName())
                .RuleFor(x => x.ClientUserName, clientUserName)
                .RuleFor(x => x.RecipientName, f => f.Person.FullName)
                .RuleFor(x => x.RecipientPhone, f => f.Person.Phone)
                .RuleFor(x => x.CreatedOn, f => f.Date.Recent(1))
                .RuleFor(x => x.CurrentStatus, StatusCode.Booked.Value)
                .RuleFor(x => x.Destination, f => new Location
                {
                    DisplayAddress = f.Address.FullAddress(),
                    Coordinates = new GeoJson2DGeographicCoordinates(f.Address.Longitude(), f.Address.Latitude())
                })
                .RuleFor(x => x.DriverUserName, f => f.Person.Email)
                .RuleFor(x => x.EstimatedDistance, f => new Distance
                {
                    Text = $"{Math.Round(f.Random.Double(1, 10), 2) } km",
                    Value = f.Random.Int(100, 1000),
                })
                .RuleFor(x => x.EstimatedItemWeight, f => f.Random.Double(1, 10))
                .RuleFor(x => x.ItemPhoto, $"image/jpeg;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs=")
                .RuleFor(x => x.Origin, f => new Location
                {
                    DisplayAddress = f.Address.FullAddress(),
                    Coordinates = new GeoJson2DGeographicCoordinates(f.Address.Longitude(), f.Address.Latitude())
                })
                .RuleFor(x => x.OriginPhoto, $"image/jpeg;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs=")
                .RuleFor(x => x.LastUpdatedOn, f => f.Date.Recent(0))
                .RuleFor(x => x.RequestSignature, f => f.PickRandom(new[] { true, false }))
                .RuleFor(x => x.RequestInsulation, f => f.PickRandom(new[] { true, false }))
                .RuleFor(x => x.SpecialInstructions, f => f.Rant.Reviews(f.Commerce.ProductName(), f.Random.Int(1, 5))
                    .Select(x => new SpecialInstruction
                    {
                        Description = x,
                        IsCompleted = f.PickRandom(true, false)
                    }).ToList())
                .RuleFor(x => x.Statuses, f => new List<Status>
                {
                    new Status {
                        Value = StatusCode.Booked.Value,
                        Comments = f.Rant.Review(),
                        Timestamp = DateTime.UtcNow,
                        UserName = clientUserName
                    }
                });

            var order = this.Faker.Generate();
            var split = order.EstimatedDistance.Text.Split(' ');
            var distance = Math.Round(double.Parse(split[0]), 2);
            var deliveryCost = new DeliveryCostFaker().Generate(distance);

            order.Cost = deliveryCost;

            return order;
        }

        /// <summary>
        /// Generates the specified status code.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <returns>
        /// An order.
        /// </returns>
        public Order Generate(StatusCode statusCode)
        {
            var clientUserName = new Faker().Person.UserName;

            this.Faker
                .RuleFor(x => x.ItemName, f => f.Commerce.ProductName())
                .RuleFor(x => x.ClientUserName, clientUserName)
                .RuleFor(x => x.RecipientName, f => f.Person.FullName)
                .RuleFor(x => x.RecipientPhone, f => f.Person.Phone)
                .RuleFor(x => x.CreatedOn, f => f.Date.Recent(1))
                .RuleFor(x => x.CurrentStatus, statusCode.Value)
                .RuleFor(x => x.Destination, f => new Location
                {
                    DisplayAddress = f.Address.FullAddress(),
                    Coordinates = new GeoJson2DGeographicCoordinates(f.Address.Longitude(), f.Address.Latitude())
                })
                .RuleFor(x => x.DriverUserName, f => f.Person.UserName)
                .RuleFor(x => x.EstimatedDistance, f => new Distance
                {
                    Text = $"{Math.Round(f.Random.Double(1, 10), 2) } km",
                    Value = f.Random.Int(100, 1000),
                })
                .RuleFor(x => x.EstimatedItemWeight, f => f.Random.Double(1, 10))
                .RuleFor(x => x.ItemPhoto, $"image/jpeg;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs=")
                .RuleFor(x => x.Origin, f => new Location
                {
                    DisplayAddress = f.Address.FullAddress(),
                    Coordinates = new GeoJson2DGeographicCoordinates(f.Address.Longitude(), f.Address.Latitude())
                })
                .RuleFor(x => x.OriginPhoto, $"image/jpeg;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs=")
                .RuleFor(x => x.LastUpdatedOn, f => f.Date.Recent(0))
                .RuleFor(x => x.RequestSignature, f => f.PickRandom(new[] { true, false }))
                .RuleFor(x => x.RequestInsulation, f => f.PickRandom(new[] { true, false }))
                .RuleFor(x => x.SpecialInstructions, f => f.Rant.Reviews(f.Commerce.ProductName(), f.Random.Int(1, 5))
                    .Select(x => new SpecialInstruction
                    {
                        Description = x,
                        IsCompleted = f.PickRandom(true, false)
                    }).ToList())
                .RuleFor(x => x.Statuses, f => new List<Status>
                {
                    new Status {
                        Value = statusCode.Value,
                        Comments = f.Rant.Review(),
                        Timestamp = DateTime.UtcNow,
                        UserName = clientUserName
                    }
                });

            var order = this.Faker.Generate();
            var split = order.EstimatedDistance.Text.Split(' ');
            var distance = Math.Round(double.Parse(split[0]), 2);
            var deliveryCost = new DeliveryCostFaker().Generate(distance);

            order.Cost = deliveryCost;

            return order;
        }
    }
}