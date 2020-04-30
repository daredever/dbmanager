FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Infra/DbManager.Infra.WebApi/DbManager.Infra.WebApi.csproj", "Infra/DbManager.Infra.WebApi/"]
COPY ["Infra/DbManager.Infra.SqlServerRepos/DbManager.Infra.SqlServerRepos.csproj", "Infra/DbManager.Infra.SqlServerRepos/"]
COPY ["Domain/DbManager.Domain.Repositories/DbManager.Domain.Repositories.csproj", "Domain/DbManager.Domain.Repositories/"]
COPY ["Domain/DbManager.Domain.Models/DbManager.Domain.Models.csproj", "Domain/DbManager.Domain.Models/"]
COPY ["Domain/DbManager.Domain.Services/DbManager.Domain.Services.csproj", "Domain/DbManager.Domain.Services/"]
COPY ["App/DbManager.App.Services/DbManager.App.Services.csproj", "App/DbManager.App.Services/"]
COPY ["Infra/DbManager.Infra.HttpServices/DbManager.Infra.HttpServices.csproj", "Infra/DbManager.Infra.HttpServices/"]
RUN dotnet restore "Infra/DbManager.Infra.WebApi/DbManager.Infra.WebApi.csproj"
COPY . .
WORKDIR "/src/Infra/DbManager.Infra.WebApi"
RUN dotnet build "DbManager.Infra.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DbManager.Infra.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DbManager.Infra.WebApi.dll"]