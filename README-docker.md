# Docker Setup for BackendAssignment

This document provides instructions for running the BackendAssignment application using Docker.

## Prerequisites

- Docker Desktop installed on your machine
- Docker Compose (usually included with Docker Desktop)

## Quick Start

1. **Build and run the application:**
   ```bash
   docker-compose up --build
   ```

2. **Access the application:**
   - API: http://localhost:8080
   - Swagger documentation: http://localhost:8080/swagger

3. **Run in detached mode:**
   ```bash
   docker-compose up -d --build
   ```

4. **Stop the application:**
   ```bash
   docker-compose down
   ```

## Docker Compose Services

### BackendAssignment.Web
- **Ports:** 8080 (HTTP), 8081 (HTTPS - if configured)
- **Environment:** Development
- **Database:** SQLite (persisted in `./data` directory)
- **Logs:** Stored in `./logs` directory

## Environment Variables

The following environment variables can be configured:

- `ASPNETCORE_ENVIRONMENT`: Application environment (Development/Production)
- `ConnectionStrings__SqliteConnection`: SQLite connection string
- `ConnectionStrings__DefaultConnection`: SQL Server connection string (optional)

## Database Configuration

By default, the application uses SQLite with the database file stored in:
- Container path: `/app/data/database.sqlite`
- Host path: `./data/database.sqlite` (persisted locally)

### Using SQL Server (Optional)

To use SQL Server instead of SQLite:

1. Uncomment the SQL Server section in `docker-compose.yml`
2. Update the connection string in the environment variables
3. Run `docker-compose up --build`

## Building the Docker Image Manually

You can build the Docker image without using Docker Compose:

```bash
# Build the image
docker build -f src/BackendAssignment.Web/Dockerfile -t backendassignment.web .

# Run the container
docker run -p 8080:8080 -v $(pwd)/data:/app/data -v $(pwd)/logs:/app/logs backendassignment.web
```

## Troubleshooting

### Common Issues

1. **Port conflicts:** If ports 8080 or 8081 are already in use, modify the port mapping in `docker-compose.yml`
2. **Permission issues:** On Linux, ensure the `./data` and `./logs` directories have proper write permissions
3. **Database not created:** The application should create the SQLite database automatically on first run

### Viewing Logs

```bash
# View logs from Docker Compose
docker-compose logs

# View logs for specific service
docker-compose logs backendassignment.web

# Follow logs in real-time
docker-compose logs -f
```

### Cleaning Up

```bash
# Stop and remove containers, networks
docker-compose down

# Remove all Docker resources (containers, images, volumes)
docker system prune -a

# Remove specific volumes
docker volume rm $(docker volume ls -q)
```

## Development Workflow

1. Make code changes locally
2. Rebuild and restart containers:
   ```bash
   docker-compose up --build
   ```
3. The application will automatically restart with changes

## Production Deployment

For production deployment, consider:

1. Using a proper database (PostgreSQL, SQL Server) instead of SQLite
2. Setting `ASPNETCORE_ENVIRONMENT=Production`
3. Configuring proper logging and monitoring
4. Setting up reverse proxy (nginx, Traefik)
5. Implementing health checks and proper security configurations
