# Backend Assignment

This repo is modified based on asp.net core 9 template project - [CleanArchitecture](https://github.com/ardalis/CleanArchitecture/blob/main/LICENSE)

## Getting Started

Pull this repo
```shell
git clone https://github.com/Hannidiot/BackendAssignment.git
cd BackendAssignment
```

### 1. Run this project

#### 1.1. Using Docker Compose (recommended)

Run command below, and everything should be ready to play within minutes.
```shell
docker compose up -d

# service should listen on http://localhost:8080/swagger
```

#### 1.2. Windows

pls make sure you have installed required dotnet sdk on your local machine

1. build the project
```shell
dotnet build
```

2. run the Web project (db migrations should be applied automatically)
```shell
dotnet run --project src/BackendAssignment.Web
```

### 2. Test this project

```shell
dotnet test
```

you can also launch the program and call api manually via 
- swagger ui
- http scripts under `src/BackendAssignment.Web/api.http`

#### 2.1. Test Data

You can find test data under `src\BackendAssignment.Infrastructure\Data\SeedData.cs`

## Design Assumption

- No Stock entity, product is alwasy sufficient
- For orders with the same order id, the first order correctly saved to database will succeed and all others will fail due to PK constraint violation.

