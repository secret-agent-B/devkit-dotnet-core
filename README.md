# Devkit Microservice Framework

DMF if a framework for designing a microservice ecosystem in .NET Core 5. The ecosystem consists of 3 major parts; Gateway, Domain APIs, Support APIs, Infrastructure Services. DMF's goal is to minimize the time needed to setup a microservice ecosystem using .NET Core and to provide libraries that will help developers write clean and testable code.

![High Level Design](/docs/images/high-level.png)

# Table of Contents

1. [Run the demo APIs](#run-the-demo-apis)
2. [Gateway](#gateway)
   1. [Security](#gateway-security)
   2. [Routing](#gateway-routing)
3. [Domain APIs](#domain-apis)
4. [How Tos](#how-tos)
   1. [Service Bus](#service-bus)

## Run the demo APIs

You'll need **Dockers** :) installed.

1. Open up your command line on the root directory of the project
2. Enter `cd infrastructure`
3. Then `docker-compose up -d` to run backing Docker containers
4. Back to the root with `cd .. `
5. Then `docker-compose up -d` to run the APIs or Open up `Devkit.sln` file in Visual Studio and run the APIs from there.

## Gateway

Gateway is an outward facing API that routes requests coming from clients, think of it as the single point of entry to the system. This API uses [Ocelot](https://github.com/ThreeMammals/Ocelot/tree/master) middleware to manage routing to `Domain APIs` also known as internal APIs. It also utilizes [Consul](https://www.consul.io/docs) as service registry. So when a request comes in Ocelot uses the `ServiceName` to look up a APIs registered in Consul with that name then `Consul` returns back the routing info to the API that needs to handle the request.

#### Gateway Security

Protect endpoints be defining requirements in the `RouteClaimsRequirement` within the route configuration of the endpoint.

```jsonc
{
  "Routes": [
    {
      "UpstreamPathTemplate": "/inventory-api/{catchAll}",
      "UpstreamHttpMethod": ["Get"],
      "DownstreamScheme": "http",
      "DownstreamPathTemplate": "/{catchAll}",
      "AuthenticationOptions": {
        "AuthenticationProviderKey": "devkit-security"
      },
      "RouteClaimsRequirement": {
        "client_id": "web-app",
        "permissions": "inventory.read"
      },
      "ServiceName": "inventory-api"
    }
  ]
}
```

#### Gateway Routing

Below is an example of a routing configuration for Ocelot. The `ServiceName` refers to the API key that is registered in Consul service registry. The `ServiceName` is the name or key of the service that is registered in Consul. The Devkit.API library should automatically register your API with Consul. For more information about how you can configure the service see [Domain API setup](#creating-a-new-project).

```jsonc
{
  "Routes": [
    {
      "ServiceName": "demo-api",
      "UpstreamHttpMethod": ["Get"],
      "DownstreamScheme": "http",
      "UpstreamPathTemplate": "/d/hello-world",
      "DownstreamPathTemplate": "/hi"
    }
  ]
}
```

The `UpstreamHttpMethod` is an array of accepted HTTP verbs. The `UpstreamPathTemplate` is the route that the clients will need to send the request to. Let's say that the Gateway url is `https://my-app.io`; the URL to send the request to for this route is `https://my-app.io/d/hello-world`. `DownstreamPathTemplate` is the action route in your controller class. This is the internal API route where the request gets handled. If you need help with routing configs refer to the Ocelot [documentation](https://ocelot.readthedocs.io/en/latest/features/routing.html).

## Domain APIs

Domain APIs are your own APIs that handles requests coming in from the `Gateway`.

### Creating a new API

It is very easy to get a service up and running using the framework, reference the `Devkit.WebAPI` project into your new .NET Core 5.0 Web API project. The `Devkit.WebAPI` framework references 3 Devkit projects, `Devkit.Patterns`, `Devkit.Metrics`, and `Devkit.ServiceBus`. `Devkit.Patterns` has wrapper classes for implementing the CQRS pattern with the `MediatR` library. `Devkit.Metrics` handles logging to `ElasticSearch`

### Add other APIs here...

## How Tos

### Service Bus

This feature provides messaging between microservices with [RabbitMQ](https://www.rabbitmq.com/) and [MassTransit](https://masstransit-project.com/). **MassTransit** is by far the easiest and intuitive message queue library to use out there for C#. I'm not gonna dive into as to why I picked AMQP vs HTTP for internal messaging you can still use libraries like [Refit](https://github.com/reactiveui/refit) if you prefer to go that route.

To setup messaging you need to create the messages or events that you can send and receive between microservices. I usually just create a new project per microservice or domain. Example, within the Devkit solution you will see a project named `Logistics.Communication.Orders`. This project contains 2 folders `DTOs` and `Messages` that are related to orders.

`Messages` folder contains interfaces about events within the application that want to you publish. An example of an event is the `IOrderSubmitted` interface - when someone created a new order and you want other microservices to use pieces of information about an order that was just submitted.

```c#
// Example of an Event. This event is Published when a user successfully submitted an order.
namespace Logistics.Communication.Orders.Messages.Events
{
    public interface IOrderSubmitted
    {
        string ClientUserName { get; }

        int ProductId { get; }

        double Price { get; }

        ...
    }
}
```

`Messages` folder is also where you place interfaces to send `Request`. Request is used for querying information from a microservice like `IGetUser`.
Messages can only be interfaces that implements `Devkit.ServiceBus.Interfaces.IRequest`, this forces a pattern where nobody can put logic into these messages, and that they only act as message contracts. The response to a `Request` is defined within the `DTOs` folder. `DTOs` are just interfaces used as a contract to send and receive response from a `Request`.

```c#
// Example of a Request. This is what you send to the bus to request for user information from microservice.
namespace Devkit.Communication.Security.Messages
{
    public interface IGetUser : Devkit.ServiceBus.Interfaces.IRequest
    {
        string UserName { get; set; }
    }
}
```

To response to the `IGetUser` request you'll need to create a consumer class in the receiving microservice. Store your consumers within a folder to make it easier for us later on to register them.

```c#
// Message consumer for IGetUser
namespace Devkit.Security.ServiceBus.Consumers
{
    public class GetUserConsumer : Devkit.ServiceBus.MessageConsumerBase<IGetUser>
    {
        protected async override Task ConsumeRequest(ConsumeContext<IGetUser> context)
        {
            var user = await FindByNameAsync(context.Message.UserName);

            if (user == null)
            {
                // send an error message back
                await context.RespondAsync<IConsumerException>(new
                {
                    ErrorMessage = $"Could not find user by user name ({context.Message.UserName})"
                });
            }
            else
            {
                // send the user infromation back
                await context.RespondAsync<IUserDTO>(new
                {
                    user.Profile.FirstName,
                    user.Profile.LastName,
                    user.UserName,
                    user.PhoneNumber
                });
            }
        }
    }
}
```

Notice that we send anonymous types back using `RespondAsync<TResponse>`. You might be wondering what the heck would happen if someone changed the contract/interface? This is where `MassTransit.Analyzers` come in, `Devkit.ServiceBus` library comes with this library that will help identify anonymous types being sent or published through `MassTransit` that does not agree with the interface.

![MassTransit.Analyzer warning](/docs/images/mt-analyzer-warning.png)

To wire up the consumers we will use `MassTransit`'s `IServiceCollectionBusConfigurator` and its extension method `AddConsumersFromNamespaceContaining<T>`. Using the `AddConsumersFromNamespaceContaining` method, pass in one of your consumers. This will add all the consumer in the same or deeper namespace.

```c#
namespace Devkit.Security.ServiceBus
{
    using MassTransit;
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    public class SecurityBusRegistry : Devkit.ServiceBus.Interfaces.IBusRegistry
    {
        public void RegisterConsumers(IServiceCollectionBusConfigurator configurator)
        {
            configurator.AddConsumersFromNamespaceContaining<GetUserConsumer>();
        }
    }
}
```

### Testing with Service Bus
