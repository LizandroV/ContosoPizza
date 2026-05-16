# CSE 325 — Week 01 — Notes de entrega

---

## Parte 1: Web API con ASP.NET Core Controllers

### Lista de pizzas (incluyendo el registro adicional)

```
GET http://localhost:5100/pizza
→ 200 OK

[
  { "id": 1, "name": "Classic Italian",  "isGlutenFree": false },
  { "id": 2, "name": "Veggie",           "isGlutenFree": true  },
  { "id": 3, "name": "Pepperoni Blast",  "isGlutenFree": false }  ← registro adicional
]
```

### Verificacion de operaciones CRUD

| Operacion | Request                              | Status Code     |
|-----------|--------------------------------------|-----------------|
| GET all   | GET /pizza                           | 200 OK          |
| GET one   | GET /pizza/3                         | 200 OK          |
| GET miss  | GET /pizza/99                        | 404 Not Found   |
| POST      | POST /pizza  body: BBQ Chicken       | 201 Created     |
| PUT       | PUT /pizza/4  body: BBQ Chicken Deluxe | 204 No Content |
| DELETE    | DELETE /pizza/4                      | 204 No Content  |

### Evidencia GET all (con registro adicional visible)

```json
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8

[
  { "id": 1, "name": "Classic Italian",  "isGlutenFree": false },
  { "id": 2, "name": "Veggie",           "isGlutenFree": true  },
  { "id": 3, "name": "Pepperoni Blast",  "isGlutenFree": false }
]
```

### Evidencia POST

```json
HTTP/1.1 201 Created
Content-Type: application/json; charset=utf-8
Location: http://localhost:5100/Pizza/4

{ "id": 4, "name": "BBQ Chicken", "isGlutenFree": false }
```

### Evidencia PUT

```
HTTP/1.1 204 No Content
```

### Evidencia DELETE

```
HTTP/1.1 204 No Content
```

---

## Parte 2: Funcion GenerateSalesSummary

```csharp
void GenerateSalesSummary(string[] files, string baseDirectory, string outputPath)
{
    double grandTotal = 0;
    var details = new List<(string RelativePath, double Total)>();

    foreach (var filePath in files)
    {
        var json      = File.ReadAllText(filePath);
        var data      = JsonConvert.DeserializeObject<SalesTotal>(json);
        var fileTotal = data?.Total ?? 0;

        grandTotal += fileTotal;

        var relativePath = Path.GetRelativePath(baseDirectory, filePath);
        details.Add((relativePath, fileTotal));
    }

    var report = new StringBuilder();
    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($" Total Sales: {grandTotal.ToString("C")}");
    report.AppendLine();
    report.AppendLine(" Details:");

    foreach (var (relativePath, total) in details)
    {
        report.AppendLine($"  {relativePath}: {total.ToString("C")}");
    }

    File.WriteAllText(outputPath, report.ToString());

    Console.WriteLine("\n══════════════════════════════════════");
    Console.Write(report.ToString());
    Console.WriteLine("══════════════════════════════════════");
    Console.WriteLine($"summary.txt guardado en: {outputPath}");
}
```

### Salida esperada en summary.txt

```
Sales Summary
----------------------------
 Total Sales: $106,406.25

 Details:
  201/sales.json: $22,385.32
  202/sales.json: $45,120.75
  203/sales.json: $38,900.18
```
