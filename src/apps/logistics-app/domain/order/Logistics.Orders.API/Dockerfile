#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["apps/logistics-app/domain/order/Logistics.Orders.API/Logistics.Orders.API.csproj", "apps/logistics-app/domain/order/Logistics.Orders.API/"]
COPY ["apps/logistics-app/communication/Logistics.Communication.Orders/Logistics.Communication.Orders.csproj", "apps/logistics-app/communication/Logistics.Communication.Orders/"]
COPY ["apps/core/sdk/communication/Devkit.ServiceBus/Devkit.ServiceBus.csproj", "apps/core/sdk/communication/Devkit.ServiceBus/"]
COPY ["apps/core/sdk/webapi/Devkit.WebAPI/Devkit.WebAPI.csproj", "apps/core/sdk/webapi/Devkit.WebAPI/"]
COPY ["apps/core/sdk/metrics/Devkit.Metrics/Devkit.Metrics.csproj", "apps/core/sdk/metrics/Devkit.Metrics/"]
COPY ["apps/core/sdk/patterns/Devkit.Patterns/Devkit.Patterns.csproj", "apps/core/sdk/patterns/Devkit.Patterns/"]
COPY ["apps/core/sdk/data/Devkit.Data/Devkit.Data.csproj", "apps/core/sdk/data/Devkit.Data/"]
COPY ["apps/core/api/communication/Devkit.Communication.ChatR/Devkit.Communication.ChatR.csproj", "apps/core/api/communication/Devkit.Communication.ChatR/"]
COPY ["apps/core/api/communication/Devkit.Communication.FileStore/Devkit.Communication.FileStore.csproj", "apps/core/api/communication/Devkit.Communication.FileStore/"]
COPY ["apps/core/api/communication/Devkit.Communication.Security/Devkit.Communication.Security.csproj", "apps/core/api/communication/Devkit.Communication.Security/"]
RUN dotnet restore "apps/logistics-app/domain/order/Logistics.Orders.API/Logistics.Orders.API.csproj"
COPY . .
WORKDIR "/src/apps/logistics-app/domain/order/Logistics.Orders.API"
RUN dotnet build "Logistics.Orders.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Logistics.Orders.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logistics.Orders.API.dll"]