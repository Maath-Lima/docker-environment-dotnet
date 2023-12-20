FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY ["src/Docker.Commands.API/*.csproj", "Docker.Commands.API/"]
COPY ["src/Docker.Commands.Data/*.csproj", "Docker.Commands.Data/"] 
COPY ["src/Docker.Commands.Models/*.csproj", "Docker.Commands.Models/"] 
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
	dotnet restore "Docker.Commands.API/Docker.Commands.API.csproj"

COPY /src .

WORKDIR "/app/Docker.Commands.API"

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
	dotnet build "Docker.Commands.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
	dotnet publish "Docker.Commands.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Docker.Commands.API.csproj"]