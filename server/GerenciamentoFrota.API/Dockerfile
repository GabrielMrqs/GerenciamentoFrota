#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GerenciamentoFrota.API/GerenciamentoFrota.API.csproj", "GerenciamentoFrota.API/"]
RUN dotnet restore "GerenciamentoFrota.API/GerenciamentoFrota.API.csproj"
COPY . .
WORKDIR "/src/GerenciamentoFrota.API"
RUN dotnet build "GerenciamentoFrota.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GerenciamentoFrota.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GerenciamentoFrota.API.dll"]