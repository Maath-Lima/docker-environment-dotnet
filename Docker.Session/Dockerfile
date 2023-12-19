FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY ["src/Docker.Session/*.csproj", "Docker.Session/"]
COPY ["src/Docker.Session.Data/*.csproj", "Docker.Session.Data/"] 
COPY ["src/Docker.Session.Models/*.csproj", "Docker.Session.Models/"] 
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
	dotnet restore "Docker.Session/Docker.Session.API.csproj"

COPY /src .

WORKDIR "/app/Docker.Session"

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
	dotnet build "Docker.Session.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
	dotnet publish "Docker.Session.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Docker.Session.API.csproj"]