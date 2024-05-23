#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AgendaService.WorkerProcess/AgendaService.WorkerProcess.csproj", "AgendaService.WorkerProcess/"]
COPY ["CSF.Domain/CSF.Domain.csproj", "CSF.Domain/"]
COPY ["CSF.Entities/CSF.Entities.csproj", "CSF.Entities/"]
COPY ["CSF.Helpers/CSF.Helpers.csproj", "CSF.Helpers/"]
COPY ["CSF.Repository.SqlServer/CSF.Repository.SqlServer.csproj", "CSF.Repository.SqlServer/"]
COPY ["CSF.Services/CSF.Services.csproj", "CSF.Services/"]
COPY ["CSF.ViewModel/CSF.ViewModel.csproj", "CSF.ViewModel/"]
COPY ["CSF.USerService/CSF.UserService.csproj", "CSF.USerService/"]
RUN dotnet restore "AgendaService.WorkerProcess/AgendaService.WorkerProcess.csproj"
COPY . .
WORKDIR "/src/AgendaService.WorkerProcess"
RUN dotnet build "AgendaService.WorkerProcess.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgendaService.WorkerProcess.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgendaService.WorkerProcess.dll"]