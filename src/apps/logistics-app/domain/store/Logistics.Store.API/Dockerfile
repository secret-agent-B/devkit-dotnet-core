#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["apps/logistics-app/domain/store/Logistics.Store.API/Logistics.Store.API.csproj", "apps/logistics-app/domain/store/Logistics.Store.API/"]
COPY ["apps/logistics-app/communication/Logistics.Communication.Store/Logistics.Communication.Store.csproj", "apps/logistics-app/communication/Logistics.Communication.Store/"]
COPY ["apps/core/sdk/communication/Devkit.ServiceBus/Devkit.ServiceBus.csproj", "apps/core/sdk/communication/Devkit.ServiceBus/"]
COPY ["apps/core/api/payment/coins.ph/Devkit.Payment.CoinsPH/Devkit.Payment.CoinsPH.csproj", "apps/core/api/payment/coins.ph/Devkit.Payment.CoinsPH/"]
COPY ["apps/core/sdk/webapi/Devkit.WebAPI/Devkit.WebAPI.csproj", "apps/core/sdk/webapi/Devkit.WebAPI/"]
COPY ["apps/core/sdk/metrics/Devkit.Metrics/Devkit.Metrics.csproj", "apps/core/sdk/metrics/Devkit.Metrics/"]
COPY ["apps/core/sdk/patterns/Devkit.Patterns/Devkit.Patterns.csproj", "apps/core/sdk/patterns/Devkit.Patterns/"]
COPY ["apps/core/sdk/data/Devkit.Data/Devkit.Data.csproj", "apps/core/sdk/data/Devkit.Data/"]
COPY ["apps/core/api/payment/Devkit.Payment/Devkit.Payment.csproj", "apps/core/api/payment/Devkit.Payment/"]
COPY ["apps/core/sdk/communication/Devkit.Http/Devkit.Http.csproj", "apps/core/sdk/communication/Devkit.Http/"]
COPY ["apps/core/api/communication/Devkit.Communication.Security/Devkit.Communication.Security.csproj", "apps/core/api/communication/Devkit.Communication.Security/"]
RUN dotnet restore "apps/logistics-app/domain/store/Logistics.Store.API/Logistics.Store.API.csproj"
COPY . .
WORKDIR "/src/apps/logistics-app/domain/store/Logistics.Store.API"
RUN dotnet build "Logistics.Store.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Logistics.Store.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logistics.Store.API.dll"]