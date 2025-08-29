# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["BackendAssignment-NoTest.slnx", "./BackendAssignment.slnx"]
COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]
COPY ["src/BackendAssignment.Core/BackendAssignment.Core.csproj", "src/BackendAssignment.Core/"]
COPY ["src/BackendAssignment.Infrastructure/BackendAssignment.Infrastructure.csproj", "src/BackendAssignment.Infrastructure/"]
COPY ["src/BackendAssignment.UseCases/BackendAssignment.UseCases.csproj", "src/BackendAssignment.UseCases/"]
COPY ["src/BackendAssignment.Web/BackendAssignment.Web.csproj", "src/BackendAssignment.Web/"]

# Restore packages
RUN dotnet restore "BackendAssignment.slnx"

# Copy everything else
COPY . .

# Build the application
WORKDIR "/src/src/BackendAssignment.Web"
RUN dotnet build "BackendAssignment.Web.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "BackendAssignment.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Install SQLite runtime
RUN apt-get update && \
    apt-get install -y --no-install-recommends sqlite3 && \
    rm -rf /var/lib/apt/lists/*

# Copy published application
COPY --from=publish /app/publish .

# Create directory for SQLite database
RUN mkdir -p /app/data

# Expose port
EXPOSE 8080
EXPOSE 8081

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Set entrypoint
ENTRYPOINT ["dotnet", "BackendAssignment.Web.dll"]
