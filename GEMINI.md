# Transport Management

## Project Overview

Transport Management is a legacy ASP.NET Web Forms application designed for managing transportation systems, including fleet tracking, route optimization, and resource allocation.

**Key Technologies:**
*   **Framework:** .NET Framework 4.8 (compilation target), .NET 4.5.2 (some packages).
*   **Type:** ASP.NET Web Forms.
*   **Database:** SQL Server (accessed via ADO.NET).
*   **Reporting:** Crystal Reports.
*   **Logging:** log4net.
*   **Frontend:** ASP.NET Web Controls, AjaxControlToolkit, jQuery, DataTables.

## Architecture

The solution uses a layered architecture, split into two main projects:

1.  **`TransportManagerUI`:** The web presentation layer.
    *   Contains `.aspx` pages (Web Forms) and their code-behind files (`.aspx.cs`).
    *   Handles user interaction and reporting.
2.  **`TransportManagerLibrary`:** The business logic and data access layer.
    *   **DAL (Data Access Layer):** Follows a Gateway/DAO pattern (e.g., `DealerGateway.cs`, `DriverDAO.cs`). Uses raw `SqlConnection` and `SqlCommand` ADO.NET objects.
    *   **DAO (Data Access Objects):** Likely simpler data carriers or specific access logic.
    *   **UtilityClass:** Helpers for logging, crypto, etc.

## Setup and Configuration

1.  **Prerequisites:**
    *   Visual Studio (with "ASP.NET and web development" workload).
    *   SQL Server (LocalDB or Express).
    *   IIS Express (usually included with VS).

2.  **Database Connection:**
    *   The connection string is defined in `TransportManagerUI/Web.config`.
    *   **Current Configuration:**
        ```xml
        <add name="TransportDBConnectionString"
             connectionString="Data Source=olbin\SQLEXPRESS;Initial Catalog=MADINADB;Integrated Security=True;"
             providerName="System.Data.SqlClient" />
        ```
    *   *Note:* The code in `TransportManagerLibrary/DAL/Gateway.cs` appears to reference a connection string named `MadinaDBConnectionString`. Ensure `Web.config` matches the code or vice-versa to avoid runtime errors.

3.  **Running the Application:**
    *   Open `TransportManager.sln` or `TransportManagerUI.sln` in Visual Studio.
    *   Set `TransportManagerUI` as the Startup Project.
    *   Run (F5) to launch in IIS Express.

## Development Conventions

*   **Data Access:** Raw ADO.NET is used. Always ensure `SqlConnection` and `SqlCommand` objects are properly disposed (using `using` blocks is recommended).
*   **Configuration:** Web application settings are in `Web.config`.
*   **Logging:** `log4net` is configured. Check `UtilityClass/Logger.cs` for usage.
*   **UI Components:** Heavily relies on server-side Web Forms controls (`<asp:TextBox>`, `<asp:GridView>`, etc.) and AjaxControlToolkit.
