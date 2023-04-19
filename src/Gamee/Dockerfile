#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Gamee/Api.csproj", "src/Gamee/"]
COPY ["src/Infra.IoC/Infra.IoC.csproj", "src/Infra.IoC/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infra.Data/Infra.Data.csproj", "src/Infra.Data/"]
RUN dotnet restore "src/Gamee/Api.csproj"
COPY . .
WORKDIR "/src/src/Gamee"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]