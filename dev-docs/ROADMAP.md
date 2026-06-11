<!-- Last updated: 2026-06-11 -->
<!-- Last change: Initial roadmap creation -->

# Hillary's Haircare - Implementation Roadmap

Generated from: dev-docs/PRD.md

## Steps

- [ ] **Step 1: Fix Scaffold Gaps**
  The existing scaffold is missing two things before any endpoints can be built: `AppointmentService` is not registered in the DbContext, and no model has navigation properties. Fix both, then run a new migration to create the `AppointmentServices` table.

  Deliverables:
  - `HillarysHaircareDbContext` has `DbSet<AppointmentService> AppointmentServices`
  - All models have navigation properties (e.g., `Appointment` has `Stylist`, `Customer`, `List<AppointmentService>`)
  - `AppointmentServices` table exists in the database

  **Acceptance Criteria:**
  - **Given** the updated DbContext, **when** `dotnet ef migrations add AddAppointmentServices` runs, **then** a migration file is generated that creates the `AppointmentServices` table.
  - **Given** the migration, **when** `dotnet ef database update` runs, **then** the database has all five tables with no errors.

- [ ] **Step 2: Stylist API Endpoints**
  Implement three endpoints covering issues #7, #8, and #9. Stylists are a good starting point because they have no foreign key dependencies.

  Endpoints:
  - `GET /api/stylists` (returns all stylists)
  - `POST /api/stylists` (creates a new stylist)
  - `PUT /api/stylists/{id}` (updates a stylist; used to deactivate)

  **Acceptance Criteria:**
  - **Given** stylists exist in the database, **when** `GET /api/stylists` is called, **then** the response is 200 with a JSON array of all stylists.
  - **Given** a valid stylist object in the request body, **when** `POST /api/stylists` is called, **then** the response is 201 and the new stylist appears in a subsequent GET.
  - **Given** an existing stylist, **when** `PUT /api/stylists/{id}` is called with `"isActive": false`, **then** the response is 204 and the stylist is marked inactive.
  - **Given** a non-existent id, **when** `PUT /api/stylists/{id}` is called, **then** the response is 404.

- [ ] **Step 3: Stylist React Views**
  Build the React UI for stylist management. This step introduces the core React pattern for the project: fetch on mount, render a list, handle a form submission.

  Views:
  - Stylist list page: displays all stylists with name and active status
  - Add Stylist form: submits a POST, refreshes the list on success
  - Deactivate button: on each stylist row, sends a PUT with `isActive: false`

  **Acceptance Criteria:**
  - **Given** the app loads, **when** the user navigates to the stylists page, **then** all stylists are displayed with their active/inactive status.
  - **Given** the add form, **when** the user submits a name, **then** the new stylist appears in the list without a page reload.
  - **Given** an active stylist, **when** the user clicks Deactivate, **then** the stylist is marked inactive in the UI.

- [ ] **Step 4: Customer API Endpoints**
  Implement two endpoints covering issues #10 and #11. Customers, like Stylists, have no foreign key dependencies.

  Endpoints:
  - `GET /api/customers` (returns all customers)
  - `POST /api/customers` (creates a new customer)

  **Acceptance Criteria:**
  - **Given** customers exist in the database, **when** `GET /api/customers` is called, **then** the response is 200 with a JSON array of all customers.
  - **Given** a valid customer object, **when** `POST /api/customers` is called, **then** the response is 201 and the new customer appears in a subsequent GET.

- [ ] **Step 5: Customer React Views**
  Build the React UI for customer management, following the same pattern established in Step 3.

  Views:
  - Customer list page: displays all customers with name, email, and phone
  - Add Customer form: submits a POST, refreshes the list on success

  **Acceptance Criteria:**
  - **Given** the app loads, **when** the user navigates to the customers page, **then** all customers are displayed with their contact details.
  - **Given** the add form, **when** the user submits a complete customer, **then** the new customer appears in the list without a page reload.

- [ ] **Step 6: Service API Endpoints**
  Implement three endpoints covering issues #12, #13, and #4. Services have no foreign key dependencies but introduce the edit (PUT) pattern.

  Endpoints:
  - `GET /api/services` (returns all services)
  - `POST /api/services` (creates a new service)
  - `PUT /api/services/{id}` (edits name or price)

  **Acceptance Criteria:**
  - **Given** services exist in the database, **when** `GET /api/services` is called, **then** the response is 200 with a JSON array including name and price for each service.
  - **Given** a valid service object, **when** `POST /api/services` is called, **then** the response is 201.
  - **Given** an existing service, **when** `PUT /api/services/{id}` is called with updated values, **then** the response is 204 and the changes are reflected in a subsequent GET.
  - **Given** a non-existent id, **when** `PUT /api/services/{id}` is called, **then** the response is 404.

- [ ] **Step 7: Service React Views**
  Build the React UI for service management, including an inline or separate edit form (the first edit experience in the project).

  Views:
  - Service list page: displays all services with name and price
  - Add Service form: submits a POST, refreshes the list
  - Edit Service form: pre-populated with existing values, submits a PUT

  **Acceptance Criteria:**
  - **Given** the app loads, **when** the user navigates to the services page, **then** all services are displayed with prices.
  - **Given** the edit form, **when** the user changes the price and submits, **then** the updated price is reflected in the list.

- [ ] **Step 8: Appointment API Endpoints**
  Implement four endpoints covering issues #2, #3, #5, and #6. This is the most complex step: appointments depend on all three other entities, involve the join table, and require calculating a total cost.

  Endpoints:
  - `GET /api/appointments` (all appointments, including stylist, customer, and services via `Include`)
  - `GET /api/appointments/{id}` (single appointment with total cost)
  - `POST /api/appointments` (creates the appointment and its `AppointmentService` records)
  - `PUT /api/appointments/{id}` (cancels by setting `IsCanceled = true`)

  **Acceptance Criteria:**
  - **Given** appointments exist, **when** `GET /api/appointments` is called, **then** each appointment in the response includes the stylist name, customer name, and list of services booked.
  - **Given** an appointment with two services priced at $30 and $50, **when** `GET /api/appointments/{id}` is called, **then** the response includes a total cost of $80.
  - **Given** a valid appointment payload with a stylist, customer, date, and one or more service ids, **when** `POST /api/appointments` is called, **then** the appointment and its service links are persisted and the response is 201.
  - **Given** an active appointment, **when** `PUT /api/appointments/{id}` is called, **then** `IsCanceled` is set to `true` and the response is 204.
  - **Given** a non-existent id on any GET or PUT, **then** the response is 404.

- [ ] **Step 9: Appointment React Views**
  Build the React UI for appointment management. This is the most complex view: the create form requires selecting a stylist, a customer, a date, and one or more services. Total cost must be calculated and displayed on the detail view.

  Views:
  - Appointment list page: displays all appointments with stylist, customer, date, and canceled status
  - Appointment detail view: shows services booked and total cost (issue #6)
  - Create Appointment form: dropdowns for stylist and customer, checkboxes or multi-select for services, date picker (issues #2)
  - Cancel button: on each appointment, sends a PUT (issue #5)

  **Acceptance Criteria:**
  - **Given** the appointments page, **when** it loads, **then** all appointments are listed with stylist name, customer name, and date.
  - **Given** an appointment detail view, **when** the user opens it, **then** the services booked and total cost are displayed.
  - **Given** the create form, **when** the user selects a stylist, customer, date, and at least one service, **then** submitting creates the appointment and it appears in the list.
  - **Given** an active appointment in the list, **when** the user clicks Cancel, **then** the appointment is marked as canceled in the UI.
