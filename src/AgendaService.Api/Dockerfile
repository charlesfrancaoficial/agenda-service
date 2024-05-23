#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AgendaService.Api/AgendaService.Api.csproj", "AgendaService.Api/"]
COPY ["AgendaApi.AsyncMessaging.RabbitMQService/AgendaApi.AsyncMessaging.RabbitMQService.csproj", "AgendaApi.AsyncMessaging.RabbitMQService/"]
COPY ["CSF.AssyncMessaging.Domain/CSF.AssyncMessaging.Domain.csproj", "CSF.AssyncMessaging.Domain/"]
COPY ["CSF.Domain/CSF.Domain.csproj", "CSF.Domain/"]
COPY ["CSF.Entities/CSF.Entities.csproj", "CSF.Entities/"]
COPY ["CSF.Helpers/CSF.Helpers.csproj", "CSF.Helpers/"]
COPY ["CSF.Repository.SqlServer/CSF.Repository.SqlServer.csproj", "CSF.Repository.SqlServer/"]
COPY ["CSF.Services/CSF.Services.csproj", "CSF.Services/"]
COPY ["CSF.ViewModel/CSF.ViewModel.csproj", "CSF.ViewModel/"]
COPY ["CSF.Repository.MongoDb/CSF.Repository.MongoDb.csproj", "CSF.Repository.MongoDb/"]
COPY ["CSF.USerService/CSF.UserService.csproj", "CSF.USerService/"]
RUN dotnet restore "AgendaService.Api/AgendaService.Api.csproj"
COPY . .
WORKDIR "/src/AgendaService.Api"
RUN dotnet build "AgendaService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgendaService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgendaService.Api.dll"]