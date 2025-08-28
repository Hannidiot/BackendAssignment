# Design Doc

## Entities

- Order Table
    1. OrderId GUID - PK
    2. CustomerName string
    3. CreatedAt timestamp(utc)
- OrderItem Table
    1. OrderId
    2. ProductId
    3. Quantity
- Product Table
    1. ProductId
    2. ProductName

## Library

- EF Core (also to manage migrations)
- DI
- MS Host
- async
- logger
- error handling (use middleware to catch exception)
- unit test + integration test
- Controllers/Services/Repositories architecture

## Assumption

1. Products are always sufficient.
2. Database is only used by this service. (so it is safe to run migrations when service startup)

## Future work

1. a dedicated migration runner
