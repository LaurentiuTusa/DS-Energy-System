#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8085

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ChatMicroservice/ChatMicroservice.csproj", "ChatMicroservice/"]
RUN dotnet restore "ChatMicroservice/ChatMicroservice.csproj"
COPY . .
WORKDIR "/src/ChatMicroservice"
RUN dotnet build "ChatMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatMicroservice.dll"]