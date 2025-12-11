# Transport Management System - Blazor Migration Plan

This document outlines the strategy for migrating the legacy ASP.NET Web Forms application (`TransportManagerUI` & `TransportManagerLibrary`) to a modern .NET 9 Blazor WebAssembly architecture.

## 1. Architecture Overview

We will move from a Monolithic Web Forms architecture to a **Hosted Blazor WebAssembly** solution.

*   **TransportManagerModern.Client (Blazor WASM):**
    *   **Role:** The user interface (Frontend).
    *   **Tech:** C#, Razor components, runs in the user's browser.
    *   **Replaces:** `.aspx` pages, jQuery, AjaxControlToolkit.
*   **TransportManagerModern.Server (ASP.NET Core Web API):**
    *   **Role:** The backend API and security layer.
    *   **Tech:** .NET 9, Controllers.
    *   **Replaces:** `TransportManagerLibrary` (Gateways/DAOs), `Web.config` connection strings.
*   **TransportManagerModern.Shared:**
    *   **Role:** Data contracts (DTOs) used by both Client and Server.
    *   **Tech:** .NET Standard/Core Class Library.

## 2. Migration Strategy: "The Strangler Fig"

Instead of rewriting the entire system at once (which is high risk), we will build the new system alongside the old one. We will migrate features vertically (Database -> API -> UI) one by one.

### Phase 1: Foundation (The "Walking Skeleton")
**Goal:** Prove the data flow from the existing SQL database to a Blazor page.

1.  **Initialize Solution:**
    *   Create the 3-project structure (`Client`, `Server`, `Shared`).
2.  **Database Connection:**
    *   Install **Dapper** (Micro-ORM) in the Server project.
    *   Configure the connection string in `appsettings.json` (replacing `Web.config`).
    *   *Why Dapper?* It allows us to copy-paste the raw SQL queries from your existing `Gateway` classes with minimal changes, preserving your complex business logic.
3.  **Vertical Slice:** "Vehicle List"
    *   **Shared:** Create `VehicleDto` class.
    *   **Server:** Port `VehicleInfoGateway.cs` logic to a `VehiclesController`.
    *   **Client:** Create a `VehicleList.razor` page to fetch and display data.

### Phase 2: Core Infrastructure

4.  **Authentication:**
    *   Implement JWT (JSON Web Token) authentication.
    *   Create an `AuthController` that validates credentials against your existing `User` table.
5.  **Reporting (The Crystal Reports Replacement):**
    *   *Challenge:* Crystal Reports (`.rpt` files) are incompatible with .NET Core.
    *   *Solution:*
        *   **Option A (Simple):** HTML-based print layouts for simple reports.
        *   **Option B (Complex):** Use **QuestPDF** or **SSRS** for complex, paginated reports.

### Phase 3: Feature Migration Loop

Repeat this process for each module (Fuel, Trips, Dealers, etc.):
1.  **Analyze:** Open the old `Gateway.cs` and the `.aspx` page.
2.  **Port SQL:** Move the SQL queries to the new API Controller using Dapper.
3.  **Build UI:** Create the corresponding Blazor Page.
4.  **Verify:** Test against the live database.

## 3. Immediate Next Steps
1.  Initialize the `TransportManagerModern` solution.
2.  Configure the SQL connection.
3.  Port the `Vehicle` module as a proof of concept.
