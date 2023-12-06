#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8084

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MCMicroservice/MCMicroservice.csproj", "MCMicroservice/"]
RUN dotnet restore "MCMicroservice/MCMicroservice.csproj"
COPY . .
WORKDIR "/src/MCMicroservice"
RUN dotnet build "MCMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MCMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MCMicroservice.dll"]