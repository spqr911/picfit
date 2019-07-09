FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["picfit.api/picfit.api.csproj", "picfit.api/"]
COPY ["picfit.application/picfit.application.csproj", "picfit.application/"]
COPY ["picfit.infrastructure/picfit.infrastructure.csproj", "picfit.infrastructure/"]
RUN dotnet restore "picfit.api/picfit.api.csproj"
COPY . .
WORKDIR "/src/picfit.api"
RUN dotnet build "picfit.api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "picfit.api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "picfit.api.dll"]