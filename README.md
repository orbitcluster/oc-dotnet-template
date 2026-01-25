# oc-dotnet-template

A boilerplate template for ASP.NET Core 8.0 Web API applications, designed for extensibility and best practices.

## Features

- **.NET 8.0**: Built on the latest LTS version of .NET.
- **Dockerized**: Include a multi-stage Dockerfile for optimized images.
- **Security-First**: Runs as a non-root user in the Docker container.
- **Dev-Ready**: Includes `docker-compose.yml` for local development.
- **CI/CD Integration**: Pre-configured GitHub Actions for CI (`app-workflow-ci`) and reusable workflows.
- **Health Checks**: Built-in health check endpoint at `/health`.

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)

### Local Development

1. **Clone the repository**:

   ```bash
   git clone https://github.com/orbitcluster/oc-dotnet-template.git
   cd oc-dotnet-template
   ```

2. **Run with .NET CLI**:

   ```bash
   dotnet restore src/webApp/OcDotnetTemplate.csproj
   dotnet run --project src/webApp/OcDotnetTemplate.csproj
   ```

   The API will be available at `http://localhost:5202` (or the port configured in `launchSettings.json`).

3. **Run with Docker Compose**:
   ```bash
   docker-compose up --build
   ```
   The API will be available at `http://localhost:8080`.

### Building the Docker Image

To build the Docker image manually:

```bash
docker build -t oc-dotnet-template .
```

## Project Structure

- `src/webApp`: The main ASP.NET Core Web API project.
- `.github/workflows`: GitHub Actions for CI/CD.
  - `ci-workflow-call.yml`: Reusable workflow call.
  - `ci-action.yml`: Standard CI action.
- `Dockerfile`: Multi-stage build definition.
- `docker-compose.yml`: Local development orchestration.

## License

[MIT](LICENSE)
