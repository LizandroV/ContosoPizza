# CSE 325 — Week 01: Build .NET Applications with C#

## Repository Structure

```
cse325-week01/
├── ContosoPizza/          ← Part 1: Web API (ASP.NET Core)
│   ├── Controllers/
│   │   └── PizzaController.cs
│   ├── Models/
│   │   └── Pizza.cs
│   ├── Services/
│   │   └── PizzaService.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── ContosoPizza.csproj
│   ├── Program.cs
│   └── thunder-collection_ContosoPizza.json  ← Thunder Client test collection
│
├── TailwindTraders/       ← Part 2: Files & Directories
│   ├── stores/
│   │   ├── 201/sales.json
│   │   ├── 202/sales.json
│   │   └── 203/sales.json
│   ├── salesTotalDir/     ← generated on first run
│   ├── TailwindTraders.csproj
│   └── Program.cs
│
├── notes.md               ← Assignment submission evidence
└── README.md
```

---

## Part 1 — ContosoPizza Web API

### Prerequisites

- .NET 8 SDK → `dotnet --version` should show `8.x.x`
- Visual Studio Code
- [Thunder Client extension](https://marketplace.visualstudio.com/items?itemName=rangav.vscode-thunder-client)

### Run the API

```bash
cd ContosoPizza
dotnet run
```

The API starts at **http://localhost:5100**

### Test with Thunder Client

1. Open VS Code
2. Click the **Thunder Client** icon in the left sidebar (lightning bolt)
3. Click **Collections → Import**
4. Select the file `ContosoPizza/thunder-collection_ContosoPizza.json`
5. The collection **"ContosoPizza API"** will appear with 6 pre-built requests

#### Correct testing order:

| #   | Request Name            | Method | URL       | Expected Status    |
| --- | ----------------------- | ------ | --------- | ------------------ |
| 1   | GET all pizzas          | GET    | /pizza    | **200 OK**         |
| 2   | GET pizza by id (id=3)  | GET    | /pizza/3  | **200 OK**         |
| 3   | GET pizza not found     | GET    | /pizza/99 | **404 Not Found**  |
| 4   | POST create pizza       | POST   | /pizza    | **201 Created**    |
| 5   | PUT update pizza (id=4) | PUT    | /pizza/4  | **204 No Content** |
| 6   | DELETE pizza (id=4)     | DELETE | /pizza/4  | **204 No Content** |

> ⚠️ Run requests in this order: GET → POST → PUT → DELETE
> After POST creates pizza with id=4, then run PUT and DELETE on id=4.

### Endpoints Summary

| Verb   | URL         | Body required                                     | Success code   |
| ------ | ----------- | ------------------------------------------------- | -------------- |
| GET    | /pizza      | —                                                 | 200 OK         |
| GET    | /pizza/{id} | —                                                 | 200 OK         |
| POST   | /pizza      | `{"name":"...","isGlutenFree":true/false}`        | 201 Created    |
| PUT    | /pizza/{id} | `{"id":N,"name":"...","isGlutenFree":true/false}` | 204 No Content |
| DELETE | /pizza/{id} | —                                                 | 204 No Content |

---

## Part 2 — TailwindTraders (Files & Directories)

### Run the project

```bash
cd TailwindTraders
dotnet run
```

This will:

1. Find all `sales.json` files under `stores/`
2. Write a `salesTotalDir/totals.txt` with raw numbers (module exercise)
3. Write a `salesTotalDir/summary.txt` with the formatted sales summary report (assignment task)
4. Print the summary to the console

### Expected console output

```
Found 3 sales file(s).
Totals written to: .../salesTotalDir/totals.txt

══════════════════════════════════════
Sales Summary
----------------------------
 Total Sales: $106,406.25

 Details:
  201/sales.json: $22,385.32
  202/sales.json: $45,120.75
  203/sales.json: $38,900.18

══════════════════════════════════════
Summary saved to: .../salesTotalDir/summary.txt
```
