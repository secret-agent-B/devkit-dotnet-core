#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["apps/core/api/chatr/Devkit.ChatR/Devkit.ChatR.csproj", "apps/core/api/chatr/Devkit.ChatR/"]
COPY ["apps/core/sdk/webapi/Devkit.WebAPI/Devkit.WebAPI.csproj", "apps/core/sdk/webapi/Devkit.WebAPI/"]
COPY ["apps/core/sdk/communication/Devkit.ServiceBus/Devkit.ServiceBus.csproj", "apps/core/sdk/communication/Devkit.ServiceBus/"]
COPY ["apps/core/sdk/metrics/Devkit.Metrics/Devkit.Metrics.csproj", "apps/core/sdk/metrics/Devkit.Metrics/"]
COPY ["apps/core/sdk/patterns/Devkit.Patterns/Devkit.Patterns.csproj", "apps/core/sdk/patterns/Devkit.Patterns/"]
COPY ["apps/core/sdk/data/Devkit.Data/Devkit.Data.csproj", "apps/core/sdk/data/Devkit.Data/"]
COPY ["apps/core/api/communication/Devkit.Communication.ChatR/Devkit.Communication.ChatR.csproj", "apps/core/api/communication/Devkit.Communication.ChatR/"]
RUN dotnet restore "apps/core/api/chatr/Devkit.ChatR/Devkit.ChatR.csproj"
COPY . .
WORKDIR "/src/apps/core/api/chatr/Devkit.ChatR"
RUN dotnet build "Devkit.ChatR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devkit.ChatR.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devkit.ChatR.dll"]