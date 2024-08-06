#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BabyGazelle.Templates/BabyGazelle.Templates.VerticalSlice/BabyGazelle.Templates.VerticalSlice.csproj", "BabyGazelle.Templates/BabyGazelle.Templates.VerticalSlice/"]
RUN dotnet restore "./BabyGazelle.Templates/BabyGazelle.Templates.VerticalSlice/BabyGazelle.Templates.VerticalSlice.csproj"
COPY . .
WORKDIR "/src/BabyGazelle.Templates/BabyGazelle.Templates.VerticalSlice"
RUN dotnet build "./BabyGazelle.Templates.VerticalSlice.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BabyGazelle.Templates.VerticalSlice.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BabyGazelle.Templates.VerticalSlice.dll"]