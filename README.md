# Devkit Microservice Framework

A base framework for building microservices with .NET. Devkit provides authentication, CQRS patterns, service-to-service messaging over AMQP, centralized logging, and testing utilities to accelerate API development.

![High Level Design](/docs/images/high-level.png)

## Table of Contents

- [Prerequisites](#prerequisites)
- [Architecture Overview](#architecture-overview)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Infrastructure](#infrastructure)
- [Gateway](#gateway)
  - [Security](#gateway-security)
  - [Routing](#gateway-routing)
- [Domain APIs](#domain-apis)
  - [Creating a New API](#creating-a-new-api)
  - [SDK Libraries](#sdk-libraries)
- [Service Bus](#service-bus)
  - [Defining Messages and Events](#defining-messages-and-events)
  - [Consuming Messages](#consuming-messages)
  - [Registering Consumers](#registering-consumers)
  - [Contract Safety with Analyzers](#contract-safety-with-analyzers)
- [Testing](#testing)
  - [Unit Testing with Service Bus](#unit-testing-with-service-bus)
  - [Integration Testing](#integration-testing)
- [Service Ports](#service-ports)

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) (see `global.json`)
- [Docker](https://www.docker.com/get-started) and Docker Compose

## Architecture Overview

The system follows a microservice architecture with these key components:

| Component | Technology | Purpose |
|-----------|-----------|---------|
| API Gateway | [Ocelot](https://github.com/ThreeMammals/Ocelot) | Single entry point, routing, and authorization |
| Service Discovery | [Consul](https://www.consul.io/) | Dynamic service registration and lookup |
| Message Bus | [RabbitMQ](https://www.rabbitmq.com/) + [MassTransit](https://masstransit-project.com/) | Async inter-service communication |
| Database | [MongoDB](https://www.mongodb.com/) | Data persistence per service |
| Logging | [Elasticsearch](https://www.elastic.co/) + [Kibana](https://www.elastic.co/kibana) | Centralized log aggregation and visualization |
| Caching | [Redis](https://redis.io/) | General caching and real-time features |

Clients send requests to the **Gateway**, which authenticates via JWT and routes to the appropriate **Domain API** using Consul for service discovery. Domain APIs communicate internally through the **Service Bus** (RabbitMQ/MassTransit). Each service owns its own database.

## Getting Started

1. Start the infrastructure containers:
   ```bash
   cd infrastructure
   docker-compose up -d
   ```
2. Start the API services (choose one):
   ```bash
   # Option A: Docker Compose
   cd src
   docker-compose up -d

   # Option B: Visual Studio
   # Open src/Devkit.sln and run from the IDE
   ```

## Project Structure

```
src/
├── apps/
│   ├── core/
│   │   ├── api/           # Shared APIs (Gateway, Security, FileStore, Ratings)
│   │   └── sdk/           # Reusable libraries (WebAPI, Patterns, Metrics, Test, Http)
│   └── logistics-app/
│       ├── communication/  # Message contracts and DTOs (per domain)
│       └── domain/         # Domain APIs (Orders, Store, Vehicles)
├── configs/                # Externalized JSON configuration files
│   ├── gateway/            # Ocelot routing configs
│   ├── infrastructure/     # Bus, logging configs
│   └── services/           # Per-service configs
└── tests/                  # Unit and integration test projects
```

## Infrastructure

The `infrastructure/docker-compose.yaml` file provisions all backing services:

| Service | Port | Description |
|---------|------|-------------|
| Consul | 8500 | Service registry and health checks |
| RabbitMQ | 5672, 15672 | Message broker (management UI on 15672) |
| MongoDB | 27017 | Database |
| Elasticsearch | 9200 | Log storage and search |
| Kibana | 5601 | Log visualization dashboard |
| Redis (cache) | 6382 | General-purpose cache |
| Redis (chatr) | 6381 | Real-time features |
| RedisInsight | 8001 | Redis management UI |
| Portainer | 9000 | Container management UI |

## Gateway

The Gateway is the single entry point for all client requests. It uses [Ocelot](https://github.com/ThreeMammals/Ocelot) middleware to route requests to internal Domain APIs and [Consul](https://www.consul.io/) for dynamic service discovery. When a request comes in, Ocelot looks up the target service by `ServiceName` in Consul, which returns the routing information for that service.

### Gateway Security

Protect endpoints by defining claim requirements in `RouteClaimsRequirement` within the route configuration:

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

The Gateway supports role-based policies including `admin-policy`, `driver-policy`, `client-policy`, and `test-policy`, each mapped to specific roles.

### Gateway Routing

Below is a basic routing configuration. The `ServiceName` must match the name registered in Consul — the `Devkit.WebAPI` library handles service registration automatically.

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

| Property | Description |
|----------|-------------|
| `UpstreamHttpMethod` | Array of accepted HTTP verbs |
| `UpstreamPathTemplate` | The public route clients send requests to (e.g., `https://my-app.io/d/hello-world`) |
| `DownstreamPathTemplate` | The controller action route on the internal service |
| `ServiceName` | Consul service name used for dynamic routing |

For more details, see the Ocelot [routing documentation](https://ocelot.readthedocs.io/en/latest/features/routing.html).

## Domain APIs

Domain APIs handle requests routed from the Gateway. Each API owns its domain logic and data.

### Creating a New API

To create a new service, reference the `Devkit.WebAPI` project in a new .NET Web API project. `Devkit.WebAPI` brings in the core framework libraries automatically.

### SDK Libraries

| Library | Purpose |
|---------|---------|
| `Devkit.WebAPI` | Base Web API setup, Consul registration, middleware pipeline |
| `Devkit.Patterns` | CQRS pattern implementation using [MediatR](https://github.com/jbogard/MediatR) |
| `Devkit.Metrics` | Centralized logging to Elasticsearch |
| `Devkit.ServiceBus` | MassTransit-based messaging abstractions, bus configuration |
| `Devkit.Http` | HTTP client utilities for service-to-service communication |
| `Devkit.Test` | Test harness, integration test base classes, and fake consumer support |

---

## Service Bus

The framework uses [RabbitMQ](https://www.rabbitmq.com/) and [MassTransit](https://masstransit-project.com/) for asynchronous inter-service communication. If you prefer HTTP-based communication, you can use libraries like [Refit](https://github.com/reactiveui/refit) instead.

### Defining Messages and Events

Create a dedicated communication project per domain (e.g., `Logistics.Communication.Orders`) with two folders:

- **`Messages/`** — Interfaces defining events and requests
- **`DTOs/`** — Interfaces defining response contracts

**Events** represent something that happened in the system:

```csharp
// Published when a user successfully submits an order.
public interface IOrderSubmitted
{
    string ClientUserName { get; }
    int ProductId { get; }
    double Price { get; }
}
```

**Requests** query data from another service. They must implement `Devkit.ServiceBus.Interfaces.IRequest`, ensuring messages remain pure contracts with no logic:

```csharp
// Requests user information from the Security service.
public interface IGetUser : Devkit.ServiceBus.Interfaces.IRequest
{
    string UserName { get; set; }
}
```

### Consuming Messages

To respond to an `IGetUser` request, create a consumer in the receiving service:

```csharp
public class GetUserConsumer : Devkit.ServiceBus.MessageConsumerBase<IGetUser>
{
    protected async override Task ConsumeRequest(ConsumeContext<IGetUser> context)
    {
        var user = await FindByNameAsync(context.Message.UserName);

        if (user == null)
        {
            await context.RespondAsync<IConsumerException>(new
            {
                ErrorMessage = $"Could not find user by user name ({context.Message.UserName})"
            });
        }
        else
        {
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

### Registering Consumers

Use `MassTransit`'s `AddConsumersFromNamespaceContaining<T>` to register all consumers in a namespace at once. Implement `IBusRegistry` to wire them up:

```csharp
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

### Contract Safety with Analyzers

Responses use anonymous types with `RespondAsync<TResponse>`. The `MassTransit.Analyzers` package (included with `Devkit.ServiceBus`) detects mismatches between anonymous types and their target interfaces at compile time:

![MassTransit.Analyzer warning](/docs/images/mt-analyzer-warning.png)

---

## Testing

### Unit Testing with Service Bus

Use MassTransit's `InMemoryTestHarness` for unit tests. This provides an in-memory bus that captures all published and sent messages:

```csharp
this.TestHarness = new InMemoryTestHarness();
this.TestHarness.Consumer<FakeGetUserConsumer>();
this.TestHarness.Start().Wait();

var handler = new CreateOrderHandler(this.Repository, this.TestHarness.Bus);
handler.Handle(command, CancellationToken.None);

// Verify the event was published
Assert.True(await this.TestHarness.Published.Any<IOrderCreated>());
```

### Integration Testing

The `Devkit.Test` library provides `IntegrationTestBase<TSUT, TStartup>` for full end-to-end testing — from the HTTP request through the controller, handler, and service bus.

#### Setting Up Fake Consumers

Register fake consumers to simulate responses from other services during integration tests:

```csharp
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

#### Creating a Reusable Test Base

Create one integration base class per domain to reuse the test setup across endpoints:

```csharp
public abstract class OrdersIntegrationTestBase<TRequest>
    : Devkit.Test.IntegrationTestBase<TRequest, Logistics.Orders.API.Startup>
{
    protected OrdersIntegrationTestBase(AppTestFixture<Startup> testFixture)
        : base(testFixture)
    {
        testFixture.ConfigureTestServices(services =>
        {
            services.AddSingleton<IBusRegistry, TestOrdersBusRegistry>();
        });
    }
}
```

---

## Service Ports

Quick reference for all services when running locally with Docker Compose:

| Service | Port |
|---------|------|
| Gateway | 8001 |
| Security API | 8002 |
| FileStore API | 8003 |
| Ratings API | 8005 |
| Orders API | 8011 |
| Store API | 8012 |
| Vehicles API | 8013 |
