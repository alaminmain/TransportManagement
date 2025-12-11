# Migration Tracker

## Legend
- **[ ]** Pending
- **[-]** In Progress
- **[x]** Completed
- **[!]** Skipped/Deprecated

## 1. Core & Admin
| Feature | Status | Server (API) | Client (UI) | Notes |
| :--- | :---: | :---: | :---: | :--- |
| **Authentication** | [ ] | AuthController | Login.razor | JWT Implementation |
| User Management | [ ] | UserGateway | AddUser, CustomerInfo | |
| Role Management | [ ] | RoleGateway | Roles, AssignMenuToRole | |
| Menu System | [ ] | - | NavMenu | Dynamic menu based on roles? |

## 2. Master Data
| Feature | Status | Server (API) | Client (UI) | Notes |
| :--- | :---: | :---: | :---: | :--- |
| **Vehicles** | [-] | VehiclesController (Basic) | VehicleList.razor | **Phase 1 Foundation** |
| Drivers | [ ] | DriverDAO | DriverInfo | |
| Hired Drivers/Vehicles | [ ] | - | HiredDriver*, HiredVehicle | |
| Dealers | [ ] | DealerGateway | DealerInfo | |
| Locations/Ghats | [ ] | LocationGateway | LocationInfo, Ghat | |
| Products | [ ] | ProductGateway | ProductInfo | |
| Suppliers | [ ] | - | Supplier | |
| Chart of Accounts | [ ] | ChartOfAccountGateway | ChartofAccounts | |

## 3. Operations (Transport)
| Feature | Status | Server (API) | Client (UI) | Notes |
| :--- | :---: | :---: | :---: | :--- |
| **Trip Management** | [ ] | TripInfoGateway | TripInfo, TripInfoOther | |
| Trip Advice | [ ] | - | TripAdvInfo | |
| Trip Cancellation | [ ] | - | TripCancel | |
| Vehicle Movement | [ ] | - | VehicleMovmentEntry | |
| Fuel Management | [ ] | FuelInfoGateway | FuelSlip | |
| Administrative | [ ] | - | PaperRenewal, AdminStatus | |

## 4. Commercial
| Feature | Status | Server (API) | Client (UI) | Notes |
| :--- | :---: | :---: | :---: | :--- |
| **DO (Delivery Order)** | [ ] | - | DO, PendingDOList | |
| **TC (Transport Challan)**| [ ] | TransContactGateway | TransportContact, PendingTC | |
| Vouchers | [ ] | VoucherGateway | Voucher | |
| Bills | [ ] | - | BillReport | |

## 5. Inventory & Store
| Feature | Status | Server (API) | Client (UI) | Notes |
| :--- | :---: | :---: | :---: | :--- |
| Purchase Orders | [ ] | - | PurchaseOrder | |
| Requisitions | [ ] | OilReqGateway | InternalRequsition | |
| Stock/Issues | [ ] | - | DailyStock, PullReceive | |
| Workshop | [ ] | - | WorkshopProduct, WorkshopStatus | |

## 6. Reporting (Crystal Reports)
*See [CRYSTAL_REPORTS_PLAN.md](./CRYSTAL_REPORTS_PLAN.md) for detailed strategy.*

| Report Category | Status | Count | Strategy |
| :--- | :---: | :---: | :--- |
| **Documents (Slips, Challans)**| [ ] | ~10 | QuestPDF (Pixel-perfect) |
| **Statements (Analytical)** | [ ] | ~40 | Blazor Grid + Export |
| **Summaries** | [ ] | ~10 | Dashboard/Charts |
