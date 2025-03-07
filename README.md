# Devkit Microservice Framework

DMF provides a base framework for developing microservices using .NET Core. The framework provides security, consistent patterns with developing your API endpoints, service-to-service communication through AMQP, and helper libraries that will help with testing, etc. I use this for my personal projects to help speed up my prototyping or development.

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

You'll need **Docker** installed.

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

It is very easy to get a service up and running using the framework, reference the `Devkit.WebAPI` project into your new .NET Core 5.0 Web API project. The `Devkit.WebAPI` framework references 3 Devkit projects, `Devkit.Patterns`, `Devkit.Metrics`, and `Devkit.ServiceBus`. `Devkit.Patterns` has wrapper classes for implementing the CQRS pattern with the `MediatR` library. `Devkit.Metrics` handles logging to `ElasticSearch`.

TODO: Cover support libraries here.

### Add other APIs here...

---

## How Tos

### Service Bus

This feature provides messaging between microservices with [RabbitMQ](https://www.rabbitmq.com/) and [MassTransit](https://masstransit-project.com/). **MassTransit** is by far the easiest message queue library to use out there for C#. I'm not gonna dive into as to why I picked AMQP vs HTTP for internal messaging you can still use libraries like [Refit](https://github.com/reactiveui/refit) if you prefer to go that route.

To setup messaging you need to create the messages or events that you can send and receive between microservices. I usually just create a new project per microservice or domain. Example, within the Devkit solution you will see a project named `Logistics.Communication.Orders`. This project contains 2 folders `DTOs` and `Messages` that are related to orders.

`Messages` folder contains interfaces about events within the application that want to you publish. An example of an event is the `IOrderSubmitted` interface - when someone created a new order and you want other microservices to use pieces of information about an order that was just submitted.

```c#
// Example of an Event. This event is Published when a user successfully submitted an order.
public interface IOrderSubmitted
{
    string ClientUserName { get; }

    int ProductId { get; }

    double Price { get; }

    ...
}
```

`Messages` folder is also where you place interfaces to send `Request`. Request is used for querying information from a microservice like `IGetUser`.
Messages can only be interfaces that implements `Devkit.ServiceBus.Interfaces.IRequest`, this forces a pattern where nobody can put logic into these messages, and that they only act as message contracts. The response to a `Request` is defined within the `DTOs` folder. `DTOs` are just interfaces used as a contract to send and receive response from a `Request`.

```c#
// Example of a Request. This is what you send to the bus to request for user information from microservice.
public interface IGetUser : Devkit.ServiceBus.Interfaces.IRequest
{
    string UserName { get; set; }
}
```

To response to the `IGetUser` request you'll need to create a consumer class in the receiving microservice. Store your consumers within a folder to make it easier for us later on to register them.

```c#
// Message consumer for IGetUser
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
```

Notice that we send anonymous types back using `RespondAsync<TResponse>`. You might be wondering what the heck would happen if someone changed the contract/interface? This is where `MassTransit.Analyzers` come in, `Devkit.ServiceBus` library comes with this library that will help identify anonymous types being sent or published through `MassTransit` that does not agree with the interface.

![MassTransit.Analyzer warning](/docs/images/mt-analyzer-warning.png)

To wire up the consumers we will use `MassTransit`'s `IServiceCollectionBusConfigurator` and its extension method `AddConsumersFromNamespaceContaining<T>`. Using the `AddConsumersFromNamespaceContaining` method, pass in one of your consumers. This will add all the consumer in the same or deeper namespace.

```c#
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;

public class SecurityBusRegistry : Devkit.ServiceBus.Interfaces.IBusRegistry
{
    public void RegisterConsumers(IServiceCollectionBusConfigurator configurator)
    {
        configurator.AddConsumersFromNamespaceContaining<GetUserConsumer>();
    }
}
```

#### Testing with Service Bus (Unit Test)

I rely heavily on `MassTransit`'s `InMemoryTestHarness` for testing. With unit test you basically just new up an instance of an `InMemoryTestHarness` then add the consumer call the `Start` method.

```c#
this.TestHarness = new InMemoryTestHarness();
this.TestHarness.Consumer<FakeGetUserConsumer>();
this.TestHarness.Start().Wait();

// Pass the IBus within the test harness, it's where messages will be sent to.
var mediatRHandler = new CreateOrderHandler(this.Repository, this.TestHarness.Bus);
mediatRHandler.Handle(command, CancellationToken.None);

// Check the messages to confirm that we published the event.
Assert.True(await this.TestHarness.Published.Any<IOrderCreated>());
```

#### Testing with Service Bus using IClassFixture<AppTestFixture<TStartup>>

The `Devkit.Test` project contains a base class called `IntegrationTestBase<TSUT, TStartup>` where TSUT is the command or query that is being tested, and TStartup is the Startup class that will help us spin up your service to send HTTP requests to test the entire flow; from the controller all the way to the command or query handler and so on. With this setup you can test to make sure that the messages are being sent out and the data that needs to be sent out are all in place.

In some microservice, you will need to register fake consumers to respond to `Requests`. Below is a fake registry that adds a fake consumer that responds with bogus values

```c#
public class TestOrdersBusRegistry : IBusRegistry
{
    public void RegisterConsumers(IServiceCollectionBusConfigurator configurator)
    {
        configurator.AddConsumer<FakeGetUserConsumer>();
    }
}

public class FakeGetUserConsumer : Devkit.ServiceBus.Test.FakeMessageConsumerBase<IGetUser>
{
    protected async override Task ConsumeRequest(ConsumeContext<IGetUser> context)
    {
        await context.RespondAsync<IUserDTO>(new
        {
            this.Faker.Person.FirstName,
            this.Faker.Person.LastName,
            context.Message.UserName,
            PhoneNumber = this.Faker.Phone.PhoneNumber()
        });
    }
}
```

Register the fake registry into DI like the example below. When your handler sends a `Request` for data through the service bus, the fake consumer registered within the fake registry will respond to the request. I usually create one IntegrationBase class per project so that I can just resuse my setup to test different endpoints.

```c#
public abstract class OrdersIntegrationTestBase<TRequest> : Devkit.Test.IntegrationTestBase<TRequest, Logistics.Orders.API.Startup>
{
    protected OrdersIntegrationTestBase(AppTestFixture<Startup> testFixture)
        : base(testFixture)
    {
        testFixture.ConfigureTestServices(services =>
        {
            services.AddSingleton<IBusRegistry, TestOrdersBusRegistry>();
        });
    }
    ...
}
```
