# KaspelTestTask
ASP.NET Core Web API project. Built as a solution of Kaspel entry task.

# Getting started
1. Set up database in SQL Server
     - Create user ```Kaspel```;
     - Create database ```Kaspel``` and assign created user as an owner.

Here's how it's done with ```sqlcmd```:
```
create login Kaspel with password = "Kaspel", check_policy = off
go

create database Kaspel
go

exec sp_changedbowner 'Kaspel'
go
```

2. Clone the project. Edit ```...\KaspelTestTask\KaspelTestTask.WebAPI\appsettings.json``` if necessary.
3. Build and run the project. Program will apply migrations and populate database with an initial set of data.

# API Definition
## Get a book with specific ID
Request:
```http
GET /api/Book/{id}
```
Response:
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "string",
  "author": "string",
  "publicationDate": "2023-03-10",
  "price": 0
}
```
## Get all books
Request:
```http
GET /api/Book/
```
Response:
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "title": "string",
    "author": "string",
    "publicationDate": "2023-03-10",
    "price": 0
  }
]
```
Acceptable parameters:
- ```?title={string}``` - filter results by Title
- ```?publicationDate={$date}``` - filter results by Publication Date
## Get an order with specific ID
Request:
```http
GET /api/Order/{id}
```
Response:
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "orderDate": "2023-03-10",
  "orderedBooks": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "title": "string",
      "quantity": 0,
      "price": 0
    }
  ]
}
```
## Get all orders
Request:
```http
GET /api/Order/
```
Response:
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "orderDate": "2023-03-10",
    "orderedBooks": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "title": "string",
        "quantity": 0,
        "price": 0
      }
    ]
  }
]
```
Acceptable parameters:
- ```?id={$uuid}``` - filter results by Id
- ```?publicationDate={$date}``` - filter results by Publication Date
## Create an order
Request:
```http
POST /api/Order/
```
Request body:
```json
[
  {
    "bookId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "quantity": 0
  }
]
```
Response:
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "orderDate": "2023-03-10",
  "orderedBooks": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "title": "string",
      "quantity": 0,
      "price": 0
    }
  ]
}
```

# Powered by
- [ASP.NET Core 7](https://github.com/dotnet/aspnetcore) - .NET Foundation and Contributors
- [Entity Framework Core 7](https://github.com/dotnet/efcore) - .NET Foundation and Contributors
- [MediatR](https://github.com/jbogard/MediatR) - Jimmy Bogard
- [GuardClauses](https://github.com/ardalis/GuardClauses) - Steve Smith
- [Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture) - Jason Taylor
