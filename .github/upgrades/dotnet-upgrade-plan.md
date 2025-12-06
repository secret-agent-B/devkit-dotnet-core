# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade apps/core/sdk/data/Devkit.Data/Devkit.Data.csproj
4. Upgrade apps/core/sdk/metrics/Devkit.Metrics/Devkit.Metrics.csproj
5. Upgrade apps/core/sdk/communication/Devkit.ServiceBus/Devkit.ServiceBus.csproj
6. Upgrade apps/core/sdk/patterns/Devkit.Patterns/Devkit.Patterns.csproj
7. Upgrade apps/core/sdk/webapi/Devkit.WebAPI/Devkit.WebAPI.csproj
8. Upgrade apps/core/api/communication/Devkit.Communication.Security/Devkit.Communication.Security.csproj
9. Upgrade apps/core/api/communication/Devkit.Communication.ChatR/Devkit.Communication.ChatR.csproj
10. Upgrade apps/core/api/payment/Devkit.Payment/Devkit.Payment.csproj
11. Upgrade apps/core/sdk/communication/Devkit.Http/Devkit.Http.csproj
12. Upgrade apps/core/api/communication/Devkit.Communication.FileStore/Devkit.Communication.FileStore.csproj
13. Upgrade apps/core/api/rating/Devkit.Ratings/Devkit.Ratings.csproj
14. Upgrade apps/core/sdk/test/Devkit.Test/Devkit.Test.csproj
15. Upgrade apps/core/api/chatr/Devkit.ChatR/Devkit.ChatR.csproj
16. Upgrade apps/core/api/communication/Devkit.Communication.Payment/Devkit.Communication.Payment.csproj
17. Upgrade apps/core/api/payment/coins.ph/Devkit.Payment.CoinsPH/Devkit.Payment.CoinsPH.csproj
18. Upgrade apps/core/api/file-store/Devkit.FileStore/Devkit.FileStore.csproj
19. Upgrade apps/core/api/auth/Devkit.Auth/Devkit.Auth.csproj
20. Upgrade apps/core/api/communication/Devkit.Communication.ChatR.Fakes/Devkit.Communication.ChatR.Fakes.csproj
21. Upgrade apps/core/api/communication/Devkit.Communication.Payment.Fakes/Devkit.Communication.Payment.Fakes.csproj
22. Upgrade apps/core/api/payment/paymaya/Devkit.Payment.PayMaya/Devkit.Payment.PayMaya.csproj
23. Upgrade apps/core/api/communication/Devkit.Communication.FileStore.Fakes/Devkit.Communication.FileStore.Fakes.csproj
24. Upgrade apps/core/api/communication/Devkit.Communication.Security.Fakes/Devkit.Communication.Security.Fakes.csproj
25. Upgrade apps/core/api/gateway/Devkit.Gateway/Devkit.Gateway.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|
| src/docker-compose.dcproj                      | Explicitly excluded         |
| src/tests/Devkit.ServiceBus.Test/Devkit.ServiceBus.Test.csproj | Explicitly excluded |
| src/tests/Devkit.Ratings.Test/Devkit.Ratings.Test.csproj | Explicitly excluded |
| src/tests/Devkit.ChatR.Test/Devkit.ChatR.Test.csproj | Explicitly excluded |
| src/tests/Devkit.Payment.CoinsPH.Test/Devkit.Payment.CoinsPH.Test.csproj | Explicitly excluded |
| src/tests/Devkit.FileStore.Test/Devkit.FileStore.Test.csproj | Explicitly excluded |

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                   | Current Version | New Version | Description                                   |
|:-----------------------------------------------|:---------------:|:-----------:|:----------------------------------------------|
| FluentValidation.AspNetCore                    | 11.3.1          | null        | Deprecated; replace per upstream guidance      |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 8.0.8       | 10.0.0      | Recommended for .NET 10.0                     |
| Microsoft.AspNetCore.Mvc.NewtonsoftJson        | 8.0.8          | 10.0.0      | Recommended for .NET 10.0                     |
| Microsoft.AspNetCore.Mvc.Testing               | 8.0.8          | 10.0.0      | Recommended for .NET 10.0                     |
| Microsoft.EntityFrameworkCore.Design           | 8.0.8          | 10.0.0      | Recommended for .NET 10.0                     |
| Microsoft.EntityFrameworkCore.Sqlite           | 8.0.8          | 10.0.0      | Recommended for .NET 10.0                     |
| Microsoft.Extensions.Caching.StackExchangeRedis| 8.0.8          | 10.0.0      | Recommended for .NET 10.0                     |
| Microsoft.Extensions.Configuration.Binder      | 8.0.2          | 10.0.0      | Recommended for .NET 10.0                     |
| Microsoft.Extensions.DependencyInjection.Abstractions | 8.0.1    | 10.0.0      | Recommended for .NET 10.0                     |
| Microsoft.Extensions.Hosting                   | 8.0.0          | 10.0.0      | Recommended for .NET 10.0                     |
| Microsoft.VisualStudio.Azure.Containers.Tools.Targets | 1.21.0 | null        | Incompatible with .NET 10.0; remove           |
| Newtonsoft.Json                                | 13.0.3         | 13.0.4      | Patch update                                   |
| Ocelot                                         | 23.3.3         | 24.0.1      | Deprecated older; update per upstream          |
| Serilog.Sinks.Elasticsearch                    | 10.0.0         | 10.0.0      | Deprecated; migrate to replacement sink        |
| System.Drawing.Common                          | 8.0.8          | 10.0.0      | Recommended for .NET 10.0                     |

### Project upgrade details
This section contains details about each project upgrade and modifications that need to be done in the project.

#### apps/core/sdk/metrics/Devkit.Metrics/Devkit.Metrics.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Serilog.Sinks.Elasticsearch should be reviewed; upstream deprecated; migrate per guidance

#### apps/core/sdk/patterns/Devkit.Patterns/Devkit.Patterns.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - FluentValidation.AspNetCore deprecated; remove and configure FluentValidation per current guidance

#### apps/core/sdk/webapi/Devkit.WebAPI/Devkit.WebAPI.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.Mvc.NewtonsoftJson update to `10.0.0`
  - Microsoft.Extensions.Caching.StackExchangeRedis update to `10.0.0`
  - Microsoft.Extensions.Configuration.Binder update to `10.0.0`
  - Microsoft.Extensions.DependencyInjection.Abstractions update to `10.0.0`

#### apps/core/api/rating/Devkit.Ratings/Devkit.Ratings.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets remove (no supported version)
  - Microsoft.AspNetCore.Mvc.Testing update to `10.0.0`

#### apps/core/sdk/test/Devkit.Test/Devkit.Test.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.Mvc.Testing update to `10.0.0`

#### apps/core/api/chatr/Devkit.ChatR/Devkit.ChatR.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets remove (no supported version)

#### apps/core/api/payment/coins.ph/Devkit.Payment.CoinsPH/Devkit.Payment.CoinsPH.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### apps/core/api/file-store/Devkit.FileStore/Devkit.FileStore.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets remove (no supported version)

#### apps/core/api/auth/Devkit.Auth/Devkit.Auth.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets remove (no supported version)
  - Microsoft.AspNetCore.Identity.EntityFrameworkCore update to `10.0.0`
  - Microsoft.EntityFrameworkCore.Design update to `10.0.0`
  - Microsoft.EntityFrameworkCore.Sqlite update to `10.0.0`

#### apps/core/api/gateway/Devkit.Gateway/Devkit.Gateway.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets remove (no supported version)
  - Ocelot update to `24.0.1`

#### apps/core/sdk/communication/Devkit.Http/Devkit.Http.csproj

NuGet packages changes:
  - Newtonsoft.Json update to `13.0.4`

#### apps/core/api/communication/Devkit.Communication.FileStore/Devkit.Communication.FileStore.csproj

NuGet packages changes:
  - System.Drawing.Common update to `10.0.0`

#### apps/core/api/payment/Devkit.Payment/Devkit.Payment.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### apps/core/sdk/communication/Devkit.ServiceBus/Devkit.ServiceBus.csproj

#### apps/core/api/communication/Devkit.Communication.Security/Devkit.Communication.Security.csproj

#### apps/core/api/communication/Devkit.Communication.ChatR/Devkit.Communication.ChatR.csproj

#### apps/core/api/communication/Devkit.Communication.Payment/Devkit.Communication.Payment.csproj

#### apps/core/api/communication/Devkit.Communication.ChatR.Fakes/Devkit.Communication.ChatR.Fakes.csproj

#### apps/core/api/communication/Devkit.Communication.Payment.Fakes/Devkit.Communication.Payment.Fakes.csproj

#### apps/core/api/payment/paymaya/Devkit.Payment.PayMaya/Devkit.Payment.PayMaya.csproj

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets remove (no supported version)
  - Microsoft.Extensions.Hosting update to `10.0.0`
