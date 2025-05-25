#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Elastic-search-aspnet-core/Elastic-search-aspnet-core.csproj", "Elastic-search-aspnet-core/"]
COPY ["Elastic.Application/Elastic.Application.csproj", "Elastic.Application/"]
COPY ["Elastic.Domain/Elastic.Domain.csproj", "Elastic.Domain/"]
COPY ["Elastic.Infrastructure/Elastic.Infrastructure.csproj", "Elastic.Infrastructure/"]
RUN dotnet restore "./Elastic-search-aspnet-core/Elastic-search-aspnet-core.csproj"
COPY . .
WORKDIR "/src/Elastic-search-aspnet-core"
RUN dotnet build "./Elastic-search-aspnet-core.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Elastic-search-aspnet-core.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Elastic-search-aspnet-core.dll"]