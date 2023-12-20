# Family Sub-Ledger Services

## Local Setup

Before running the API project you need to add missing application configuration.
Configuration items to be configured have the following format:
```json
{
  "ConnectionStrings": {
    "Core": "[postgres connection string]"
  }
}
```

## Docker Image Build

Docker image is built from within the docker environment,
so there is no need to install the SDK on your host machine to build the image.

To build the image for the API run the following command from the project root directory:
```shell
docker build -f "src/GK.FSL.Api/Dockerfile" -t "germonenko/fsl-core-api:dev" .
```

## Docker Compose Configuration

To run the whole system you can use [docker compose file](./docker-compose.yaml).
But make sure to configure missing environment variables, this can be done by adding `.env` file.

## Migrations

To add a new migration, run the following command from the project root directory:
```shell
dotnet ef migrations add {MIGRATION_NAME} -p src/GK.FSL.Migrations -s src/GK.FSL.Api -o ./
```
where {MIGRATION_NAME} should be substituted with your migration name.
