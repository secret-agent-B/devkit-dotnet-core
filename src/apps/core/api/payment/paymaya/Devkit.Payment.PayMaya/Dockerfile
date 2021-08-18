#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["apps/core/api/payment/paymaya/Devkit.Payment.PayMaya/Devkit.Payment.PayMaya.csproj", "apps/core/api/payment/paymaya/Devkit.Payment.PayMaya/"]
COPY ["apps/core/api/communication/Devkit.Communication.Payment/Devkit.Communication.Payment.csproj", "apps/core/api/communication/Devkit.Communication.Payment/"]
COPY ["apps/core/sdk/communication/Devkit.ServiceBus/Devkit.ServiceBus.csproj", "apps/core/sdk/communication/Devkit.ServiceBus/"]
COPY ["apps/core/api/payment/Devkit.Payment/Devkit.Payment.csproj", "apps/core/api/payment/Devkit.Payment/"]
COPY ["apps/core/sdk/patterns/Devkit.Patterns/Devkit.Patterns.csproj", "apps/core/sdk/patterns/Devkit.Patterns/"]
COPY ["apps/core/sdk/data/Devkit.Data/Devkit.Data.csproj", "apps/core/sdk/data/Devkit.Data/"]
COPY ["apps/core/sdk/metrics/Devkit.Metrics/Devkit.Metrics.csproj", "apps/core/sdk/metrics/Devkit.Metrics/"]
COPY ["apps/core/sdk/communication/Devkit.Http/Devkit.Http.csproj", "apps/core/sdk/communication/Devkit.Http/"]
RUN dotnet restore "apps/core/api/payment/paymaya/Devkit.Payment.PayMaya/Devkit.Payment.PayMaya.csproj"
COPY . .
WORKDIR "/src/apps/core/api/payment/paymaya/Devkit.Payment.PayMaya"
RUN dotnet build "Devkit.Payment.PayMaya.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devkit.Payment.PayMaya.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devkit.Payment.PayMaya.dll"]