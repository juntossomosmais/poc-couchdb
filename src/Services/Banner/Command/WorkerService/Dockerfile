FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Build.props ./

WORKDIR /src

COPY ["src/Services/Banner/Command/WorkerService/WorkerService.csproj", "src/Services/Banner/Command/WorkerService/"]
COPY ["src/Services/Banner/Command/Infrastructure/Infrastructure.csproj", "src/Services/Banner/Command/Infrastructure/"]
COPY ["src/Services/Banner/Command/Application/Application.csproj", "src/Services/Banner/Command/Application/"]
COPY ["src/Services/Banner/Command/Domain/Domain.csproj", "src/Services/Banner/Command/Domain/"]
COPY ["src/Contracts/Contracts.csproj", "src/Contracts/"]

RUN dotnet restore "src/Services/Banner/Command/WorkerService/WorkerService.csproj"
COPY . .
WORKDIR "/src/src/Services/Banner/Command/WorkerService"
RUN dotnet build "WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkerService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]
