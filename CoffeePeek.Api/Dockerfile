FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CoffeePeek.Api/CoffeePeek.Api.csproj", "CoffeePeek.Api/"]
COPY ["CoffeePeek.Contract/CoffeePeek.Contract.csproj", "CoffeePeek.Contract/"]
COPY ["CoffeePeek.BuildingBlocks/CoffeePeek.BuildingBlocks.csproj", "CoffeePeek.BuildingBlocks/"]
COPY ["CoffeePeek.BusinessLogic/CoffeePeek.BusinessLogic.csproj", "CoffeePeek.BusinessLogic/"]
COPY ["CoffeePeek.Data/CoffeePeek.Data.csproj", "CoffeePeek.Data/"]
COPY ["CoffeePeek.Shared.Extensions/CoffeePeek.Shared.Extensions.csproj", "CoffeePeek.Shared.Extensions/"]
RUN dotnet restore "CoffeePeek.Api/CoffeePeek.Api.csproj"
COPY . .
WORKDIR "/src/CoffeePeek.Api"
RUN dotnet build "CoffeePeek.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CoffeePeek.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeePeek.Api.dll"]
