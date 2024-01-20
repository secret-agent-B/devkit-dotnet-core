#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["apps/core/api/payment/paymaya/Devkit.Payment.PayMaya/Devkit.Payment.PayMaya.csproj", "apps/core/api/payment/paymaya/Devkit.Payment.PayMaya/"]
COPY ["apps/core/sdk/communication/Devkit.Http/Devkit.Http.csproj", "apps/core/sdk/communication/Devkit.Http/"]
COPY ["apps/core/sdk/communication/Devkit.ServiceBus/Devkit.ServiceBus.csproj", "apps/core/sdk/communication/Devkit.ServiceBus/"]
COPY ["apps/core/api/communication/Devkit.Communication.Payment/Devkit.Communication.Payment.csproj", "apps/core/api/communication/Devkit.Communication.Payment/"]
COPY ["apps/core/api/payment/Devkit.Payment/Devkit.Payment.csproj", "apps/core/api/payment/Devkit.Payment/"]
COPY ["apps/core/sdk/metrics/Devkit.Metrics/Devkit.Metrics.csproj", "apps/core/sdk/metrics/Devkit.Metrics/"]
COPY ["apps/core/sdk/patterns/Devkit.Patterns/Devkit.Patterns.csproj", "apps/core/sdk/patterns/Devkit.Patterns/"]
COPY ["apps/core/sdk/data/Devkit.Data/Devkit.Data.csproj", "apps/core/sdk/data/Devkit.Data/"]
RUN dotnet restore "apps/core/api/payment/paymaya/Devkit.Payment.PayMaya/Devkit.Payment.PayMaya.csproj"
COPY . .
WORKDIR "/src/apps/core/api/payment/paymaya/Devkit.Payment.PayMaya"
RUN dotnet build "Devkit.Payment.PayMaya.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devkit.Payment.PayMaya.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devkit.Payment.PayMaya.dll"]