FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Build.props ./

WORKDIR /src
COPY ["src/Services/Banner/Query/GrpcService/GrpcService.csproj", "src/Services/Banner/Query/GrpcService/"]
COPY ["src/Services/Banner/Query/Infrastructure/Infrastructure.csproj", "src/Services/Banner/Query/Infrastructure/"]
COPY ["src/Services/Banner/Query/Application/Application.csproj", "src/Services/Banner/Query/Application/"]
COPY ["src/Contracts/Contracts.csproj", "src/Contracts/"]
RUN dotnet restore "src/Services/Banner/Query/GrpcService/GrpcService.csproj"
COPY . .
WORKDIR "/src/src/Services/Banner/Query/GrpcService"
RUN dotnet build "GrpcService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcService.dll"]
