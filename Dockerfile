# Set the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory
WORKDIR /app/api/Motocomplex

# Copy the .csproj and restore dependencies
COPY api/Motocomplex/*.csproj ./
RUN dotnet restore

# Copy the entire project and build
COPY api/Motocomplex/ ./
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app/api/Motocomplex
COPY --from=build-env /app/api/Motocomplex/out .

# Copy the appsettings.json file
COPY api/Motocomplex/appsettings.json .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Motocomplex.dll"]