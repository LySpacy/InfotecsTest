FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["InfotecsTest.API/InfotecsTest.API.csproj", "InfotecsTest.API/"]
COPY ["InfotecsTest.Application/InfotecsTest.Application.csproj", "InfotecsTest.Application/"]
COPY ["InfotecsTest.Domain/InfotecsTest.Domain.csproj", "InfotecsTest.Domain/"]
COPY ["InfotecsTest.Infrustraction/InfotecsTest.Infrustraction.csproj", "InfotecsTest.Infrustraction/"]

RUN dotnet restore "InfotecsTest.API/InfotecsTest.API.csproj"

COPY . .

WORKDIR "/src/InfotecsTest.API"

RUN dotnet publish "InfotecsTest.API.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=true

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "InfotecsTest.API.dll"]