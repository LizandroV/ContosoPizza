# CSE 325 — Week 01: Build .NET Applications with C#

## Estructura del repositorio

```
week01/
├── ContosoPizza/                  ← Parte 1: Web API
│   ├── Controllers/
│   │   └── PizzaController.cs    ← Endpoints GET/POST/PUT/DELETE
│   ├── Models/
│   │   └── Pizza.cs              ← Forma de los datos
│   ├── Services/
│   │   └── PizzaService.cs       ← Lista en memoria + logica CRUD
│   ├── Properties/
│   │   └── launchSettings.json   ← Puerto fijo: 5100
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── ContosoPizza.csproj
│   └── Program.cs                ← Arranca la app web
│
├── TailwindTraders/               ← Parte 2: Archivos
│   ├── stores/
│   │   ├── 201/sales.json
│   │   ├── 202/sales.json
│   │   └── 203/sales.json
│   ├── TailwindTraders.csproj
│   └── Program.cs                ← Lee archivos + GenerateSalesSummary
│
├── notes.md                       ← Evidencia para la entrega
└── README.md
```

---

## Parte 1 — Correr la Web API

```bash
cd ContosoPizza
dotnet run
```

La API queda escuchando en **http://localhost:5100**

---

## Parte 1 — Probar con Postman (6 requests en orden)

### 1. GET all — ver todas las pizzas
- Method: **GET**
- URL: `http://localhost:5100/pizza`
- Body: ninguno
- Resultado esperado: **200 OK** con array de 3 pizzas

### 2. GET one — buscar por id
- Method: **GET**
- URL: `http://localhost:5100/pizza/3`
- Body: ninguno
- Resultado esperado: **200 OK** con "Pepperoni Blast"

### 3. GET not found — id que no existe
- Method: **GET**
- URL: `http://localhost:5100/pizza/99`
- Body: ninguno
- Resultado esperado: **404 Not Found**

### 4. POST — crear nueva pizza
- Method: **POST**
- URL: `http://localhost:5100/pizza`
- Body → raw → JSON:
```json
{
  "name": "BBQ Chicken",
  "isGlutenFree": false
}
```
- Resultado esperado: **201 Created** con id=4

### 5. PUT — actualizar pizza id=4
- Method: **PUT**
- URL: `http://localhost:5100/pizza/4`
- Body → raw → JSON:
```json
{
  "id": 4,
  "name": "BBQ Chicken Deluxe",
  "isGlutenFree": true
}
```
- Resultado esperado: **204 No Content**

### 6. DELETE — eliminar pizza id=4
- Method: **DELETE**
- URL: `http://localhost:5100/pizza/4`
- Body: ninguno
- Resultado esperado: **204 No Content**

---

## Parte 2 — Correr TailwindTraders

```bash
cd TailwindTraders
dotnet run
```

Genera dos archivos en `salesTotalDir/`:
- `totals.txt` → numeros crudos (del modulo)
- `summary.txt` → reporte formateado (tarea del assignment)
