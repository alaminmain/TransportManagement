# Crystal Reports Migration Plan

## Overview
The legacy application heavily relies on Crystal Reports (`.rpt` files) for both **operational documents** (Receipts, Challans, Slips) and **analytical reports** (Monthly Statements, Summaries). Since Crystal Reports is not supported in .NET Core/.NET 9, we must rewrite them.

## Strategy by Category

### 1. Operational Documents (High Fidelity)
**Examples:** `TripChallan.rpt`, `Bill.rpt`, `FuelSlip.rpt`, `VehicleWorkSlip.rpt`
**Requirement:** These documents often need to be printed on specific paper sizes (A4, Slip) or emailed as PDFs. They require precise layout control.
**Solution: [QuestPDF](https://www.questpdf.com/)**
*   **Why:** Code-first PDF generation, strictly typed, high performance, no external designer needed.
*   **Implementation:**
    *   Create a `Reports` service in the Server API.
    *   Define layouts using C# fluent API.
    *   Expose an endpoint: `GET /api/reports/trip-challan/{id}/pdf`.
    *   Client opens this URL in a new tab or downloads the file.

### 2. Analytical Reports (Statements & Lists)
**Examples:** `TripStatement.rpt`, `VehicleMovement.rpt`, `DailyStock.rpt`, `AccidentalStatement.rpt`
**Requirement:** Large datasets, tabular data, grouping, totals.
**Solution: Interactive Blazor Pages + Export**
*   **Why:** Static PDFs are "dead" data. Modern users prefer interactive tables.
*   **Implementation:**
    *   **UI:** Use a Data Grid component (e.g., QuickGrid or MudBlazor Table) with filtering, sorting, and pagination.
    *   **Export:** Add an "Export to Excel" or "Export to PDF" button.
    *   **Tools:** `ClosedXML` for Excel export, or simple HTML-to-PDF if a snapshot is needed.

### 3. Summaries & Dashboards
**Examples:** `VoucherSummery.rpt`, `FuelSummery.rpt`
**Requirement:** Aggregated data, visual trends.
**Solution: Dashboard Widgets**
*   **Implementation:** Render charts (using a library like ApexCharts or Chart.js) directly on the Blazor dashboard.

## Migration Process (Per Report)

1.  **Analyze:** Open the `.rpt` file (requires Crystal Reports Viewer or VS Extension) OR run the legacy app to see the output.
    *   *Alternative:* If `.rpt` cannot be opened, inspect the SQL Query in the legacy `Gateway` or `DAO` that feeds the report.
2.  **Data Retrieval:** Create a Dapper query in the new API to fetch the same dataset.
3.  **Design:**
    *   If **Document**: Build QuestPDF layout.
    *   If **Statement**: Build Blazor Grid.
4.  **Verify:** Compare the new output with a PDF generated from the legacy system.

## Priority List

| Priority | Report Name | Category | Proposed Solution |
| :--- | :--- | :--- | :--- |
| **High** | `TripChallan.rpt` | Document | QuestPDF |
| **High** | `Bill.rpt` | Document | QuestPDF |
| **High** | `FuelSlip.rpt` | Document | QuestPDF |
| **Med** | `DailyStock.rpt` | Statement | Blazor Grid |
| **Med** | `TripStatement.rpt` | Statement | Blazor Grid |
