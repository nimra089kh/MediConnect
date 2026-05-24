# MediConnect — Smart Hospital Management Platform

Enterprise-grade healthcare API built with ASP.NET Core 8,
Clean Architecture, and CQRS pattern.

## Tech Stack
- ASP.NET Core 8 Web API
- Entity Framework Core 8 + SQL Server
- JWT Authentication + Refresh Tokens
- CQRS with MediatR
- Repository Pattern + Unit of Work
- FluentValidation
- Swagger/OpenAPI

## Architecture
Clean Architecture with 4 layers:
- Domain — Entities, Enums, Exceptions
- Application — Use Cases, CQRS, Interfaces
- Infrastructure — EF Core, Repositories, JWT
- API — Controllers, Middleware

## Features
- Multi-tenant hospital system
- JWT authentication with RBAC
- Doctor availability & slot management
- Appointment booking with double-booking prevention
- Tenant isolation on every query
- Soft deletes throughout

## API Endpoints
POST /api/auth/login
POST /api/auth/register
POST /api/doctors/availability
POST /api/doctors/generate-slots
GET  /api/doctors/{id}/slots
POST /api/appointments/book
GET  /api/appointments/my