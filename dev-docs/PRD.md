<!-- Last updated: 2026-06-11 -->
<!-- Last change: Initial PRD creation -->

# Hillary's Haircare - Product Requirements Document

## Problem Statement

Hillary's Haircare needs a management tool to track stylists, customers, services, and appointments. Staff should be able to schedule appointments, assign services, calculate costs, and manage the active roster of stylists without using paper records or spreadsheets.

## Target Users

Salon staff (receptionist, manager) operating the application as an internal tool. There is no public-facing or customer-facing interface in scope.

## Core Requirements

The following user stories define the required functionality. Each maps to an open GitHub issue.

**Stylists**
- View a list of all stylists (#7)
- Add a new stylist (#8)
- Deactivate a stylist (mark as inactive without deleting) (#9)

**Customers**
- View a list of all customers (#10)
- Add a new customer (#11)

**Services**
- View a list of all services with prices (#12)
- Add a new service (#13)
- Edit an existing service (name or price) (#4)

**Appointments**
- View all appointments, including stylist, customer, and services booked (#3)
- Create a new appointment, selecting a stylist, customer, date, and one or more services (#2)
- Cancel an appointment (mark as canceled without deleting) (#5)
- Calculate and display the total cost of an appointment by summing its services (#6)

## Technical Stack

### Stack Decisions

| Layer | Technology | Rationale |
|-------|-----------|-----------|
| Backend | ASP.NET Core (Minimal API) | Book 2 pattern: endpoints mapped directly in Program.cs, no controllers |
| ORM | Entity Framework Core | Book 2 pattern: DbContext, DbSet, Include() for related data |
| Database | PostgreSQL via Npgsql | Already configured; matches NSS curriculum tooling |
| Frontend | React (Vite) | Already scaffolded; Book 2 teaches React consuming a .NET API |
| API style | REST | HTTP verbs and status codes are a core learning objective |

## Data Model

Five tables, already migrated (with one gap noted below):

```
Stylist          Customer           Service
--------         --------           -------
Id               Id                 Id
Name             FirstName          Name
IsActive         LastName           Price
                 Email
                 Phone

Appointment                AppointmentService (join table)
-----------                ----------------------
Id                         Id
StylistId (FK)             AppointmentId (FK)
CustomerId (FK)            ServiceId (FK)
AppointmentDate
IsCanceled
```

**Gap:** `AppointmentService` exists as a model but is not registered as a `DbSet` in `HillarysHaircareDbContext`. This needs to be added before appointment services can be queried.

## Scope

### In Scope (v1)

- All 12 user stories listed above
- REST API endpoints for each story (GET, POST, PUT)
- React frontend with views for each entity and the appointment workflow
- Navigation properties and EF Core `Include()` to return related data in a single response
- Total cost calculation on the appointment detail view

### Out of Scope

- Authentication or login; any user can perform any action
- Appointment rescheduling (edit date/stylist/customer after creation)
- Deleting any record; soft deletes (IsCanceled, IsActive flags) are used instead
- Customer-facing booking portal
- Payment processing or invoicing

## Success Criteria

- All 12 GitHub issues closed with working features
- Each API endpoint returns appropriate HTTP status codes (200, 201, 204, 404)
- Appointment detail includes stylist name, customer name, list of services, and total cost
- No hardcoded data; all content is stored in and retrieved from the database
- React frontend renders each view without console errors

## Learning Goals

This is an NSS Book 2 project. The primary objectives are:

- **Minimal API pattern**: Map all endpoints in `Program.cs` using `app.MapGet`, `app.MapPost`, `app.MapPut`, and `app.MapDelete`
- **EF Core fundamentals**: Use `DbContext`, `DbSet`, and `Include()` to retrieve related entities
- **Many-to-many relationships**: Model and query the `AppointmentService` join table
- **REST conventions**: Choose the correct HTTP verb and status code for each operation
- **React + .NET integration**: `fetch` from the React frontend to the .NET API with CORS configured
- **Navigation properties**: Add them to model classes so EF Core can populate related data
