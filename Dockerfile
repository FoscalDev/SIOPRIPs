# Usa la imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SIOP/SIOP.csproj", "SIOP/"]
COPY ["SIOP.FEVRIPS/SIOP.FEVRIPS.csproj", "SIOP.FEVRIPS/"]
RUN dotnet restore "SIOP/SIOP.csproj"
COPY . .
WORKDIR "/src/SIOP"
RUN dotnet build "SIOP.csproj" -c Release -o /app/build

# Publica la aplicaci�n
FROM build AS publish
RUN dotnet publish "SIOP.csproj" -c Release -o /app/publish

# Crea la imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SIOP.dll"]