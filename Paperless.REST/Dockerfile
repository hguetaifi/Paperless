#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Container we use for final publish
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build container
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Copy the code into the container
WORKDIR /src
COPY ["Paperless.REST/Paperless.REST.csproj", "Paperless.REST/"]
COPY ["Paperless.Businesslogic.Logic/Paperless.Businesslogic.Logic.csproj","Paperless.Businesslogic.Logic/"]
COPY ["Paperless.Businesslogic.Entities/Paperless.Businesslogic.Entities.csproj","Paperless.Businesslogic.Entities/"]
COPY ["Paperless.Businesslogic.Interfaces/Paperless.Businesslogic.Interfaces.csproj","Paperless.Businesslogic.Interfaces/"]

# NuGet restore
RUN dotnet restore "Paperless.REST/Paperless.REST.csproj"
COPY ["Paperless.REST/", "Paperless.REST/"]
COPY ["Paperless.Businesslogic.Logic/","Paperless.Businesslogic.Logic/"]
COPY ["Paperless.Businesslogic.Entities/","Paperless.Businesslogic.Entities/"]
COPY ["Paperless.Businesslogic.Interfaces/","Paperless.Businesslogic.Interfaces/"]

# Build the API
WORKDIR "Paperless.REST"
RUN dotnet build "Paperless.REST.csproj" -c Release -o /app/build

# Publish it
FROM build AS publish
RUN dotnet publish "Paperless.REST.csproj" -c Release -o /app/publish

# Make the final image for publishing
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set entrypoint
ENTRYPOINT ["dotnet", "Paperless.REST.dll"]


