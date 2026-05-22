# MediConnect — Smart Hospital Management Platform

A production-grade Hospital Management REST API built with ASP.NET Core 8 and Clean Architecture. Designed to serve multiple hospitals with complete appointment lifecycle management and AI-powered symptom analysis.

---

## What It Does

MediConnect connects hospitals, doctors, and patients through a secure, role-based API. Each hospital operates in complete isolation — doctors, patients, and appointments are fully separated per tenant.

**For Patients**
- Register and log in securely
- Browse available doctor slots
- Book, view, and cancel appointments
- Get AI-powered symptom analysis before booking

**For Doctors**
- Confirm or reject incoming appointment requests
- Cancel appointments when needed

**For Admins**
- Set doctor availability schedules
- Generate appointment slots automatically
- Manage hospital operations

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 Web API |
| Database | SQL Server + Entity Framework Core 8 |
| Authentication | JWT Bearer Tokens + Refresh Tokens |
| AI | Groq LLaMA 3.3 (70B) |
| Validation | FluentValidation |
| Docs | Swagger / OpenAPI |
| Architecture | Clean Architecture |

---

## Architecture

The project follows Clean Architecture with strict dependency rules — each layer only depends inward.

```
MediConnect.Domain          → Entities, Enums, Exceptions
MediConnect.Application     → Use Cases, Interfaces, DTOs
MediConnect.Infrastructure  → EF Core, Repositories, JWT, AI
MediConnect.API             → Controllers, Middleware, Program.cs
```

**Key Patterns Used**
- Repository Pattern + Unit of Work (atomic transactions)
- Dependency Injection throughout
- Result Pattern (no exception-based business logic)
- Soft Deletes on all entities (healthcare data is never hard-deleted)
- Global Query Filters for tenant isolation

---

## API Endpoints

### Auth
```
POST   /api/auth/login           Register or log in
POST   /api/auth/register        Create a patient account
```

### Doctors
```
POST   /api/doctors/availability        Set working hours (Admin)
POST   /api/doctors/generate-slots      Auto-generate time slots (Admin)
GET    /api/doctors/{id}/slots          View available slots (Patient)
```

### Appointments
```
POST   /api/appointments/book           Book a slot (Patient)
GET    /api/appointments/my             View my appointments (Patient)
PUT    /api/appointments/{id}/status    Confirm or reject (Doctor)
PUT    /api/appointments/{id}/cancel    Cancel appointment (Patient/Doctor)
```

### AI
```
POST   /api/ai/symptom-check    Analyze symptoms with LLaMA 3.3 (Patient)
```

---

## Security

- **JWT authentication** on all endpoints except login/register
- **Role-based access control** — Patient, Doctor, Admin each have separate permissions
- **Tenant isolation** — every database query filters by HospitalId from the verified token
- **GUID primary keys** — no sequential integers exposed to clients
- **BCrypt password hashing**
- **Refresh token rotation** stored in database

---

## Appointment State Machine

```
PENDING  →  CONFIRMED   (Doctor confirms)
PENDING  →  REJECTED    (Doctor rejects)
PENDING  →  CANCELLED   (Patient cancels — up to 2 hours before)
CONFIRMED → CANCELLED   (Doctor cancels anytime)
CONFIRMED → COMPLETED   (Appointment done)
CONFIRMED → NO_SHOW     (Patient did not arrive)
```

---

## Database Schema

22 tables across 7 logical groups — all with `CreatedAt`, `UpdatedAt`, `CreatedBy` audit fields and `IsActive` soft delete.

| Group | Tables |
|---|---|
| Identity & Auth | Users, RefreshTokens, Roles, UserRoles |
| Hospital Structure | Hospitals, Departments, Specializations |
| Doctor Module | Doctors, DoctorAvailability, AppointmentSlots |
| Patient Module | Patients, MedicalHistory, MedicalDocuments |
| Appointment Module | Appointments, AppointmentStatusHistory |
| Transactions | Payments, Refunds, Feedback |
| System | Notifications, AuditLogs |

---

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Groq API key (free at groq.com)

### Setup

**1. Clone the repository**
```bash
git clone https://github.com/nimra089kh/MediConnect.git
cd MediConnect
```

**2. Create `appsettings.Development.json` in `src/MediConnect.API/`**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=MediConnectDb;Trusted_Connection=true;TrustServerCertificate=True;"
  },
  "Jwt": {
    "SecretKey": "YourSecretKeyMinimum32CharactersLong"
  },
  "OpenAI": {
    "ApiKey": "gsk_your_groq_key_here"
  }
}
```

**3. Run migrations**
```bash
cd src/MediConnect.API
dotnet ef database update --project ../MediConnect.Infrastructure
```

**4. Run the API**
```bash
dotnet run
```

**5. Open Swagger**
```
http://localhost:5283/swagger
```

Default seed credentials:
```
Admin:  admin@mediconnect.com  /  Admin@123
Doctor: doctor@mediconnect.com /  Doctor@123
```

---

## Project Status

| Feature | Status |
|---|---|
| Authentication & RBAC | Done |
| Multi-tenant isolation | Done |
| Appointment lifecycle | Done |
| AI symptom checker | Done |
| Email notifications | Planned |
| Stripe payments | Planned |
| React frontend | Planned |
| Docker support | Planned |

---

## Author

Built by **Nimra** — self-taught .NET developer based in Karachi, Pakistan.

Open to Junior .NET Developer opportunities.

[LinkedIn](https://linkedin.com/in/your-profile) · [GitHub](https://github.com/nimra089kh)
