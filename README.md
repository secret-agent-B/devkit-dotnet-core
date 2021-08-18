# Devkit Microservice Framework

DMF if a framework for designing a microservice ecosystem in .NET Core 5. The ecosystem consists of 3 major parts; Gateway, Domain APIs, Support APIs, Infrastructure Services. DMF's goal is to minimize the time needed to setup a microservice ecosystem using .NET Core and to provide libraries that will help developers write clean and testable code.

![High Level Design](/docs/images/high-level.png)

# Table of Contents

1. [Run the demo APIs](#run-the-demo-apis)
2. [Gateway](#gateway)
   1. [Security](#gateway-security)
   2. [Routing](#gateway-routing)
3. [Domain APIs](#domain-apis)

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
