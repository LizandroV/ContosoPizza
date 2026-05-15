# CSE 325 — Week 01 Assignment Notes

---

## Part 1: Web API with ASP.NET Core Controllers

### Pizza list — showing all records including additional entry

```
GET http://localhost:5100/pizza
→ 200 OK

[
  { "id": 1, "name": "Classic Italian",  "isGlutenFree": false },
  { "id": 2, "name": "Veggie",           "isGlutenFree": true  },
  { "id": 3, "name": "Pepperoni Blast",  "isGlutenFree": false }   ← added record
]
```

### CRUD Operations — Request & Response Evidence

#### GET all pizzas

```
Request:  GET http://localhost:5100/pizza
Response: 200 OK
Body:     [ array of all 3 pizzas ]
```

#### GET single pizza

```
Request:  GET http://localhost:5100/pizza/3
Response: 200 OK
Body:     { "id": 3, "name": "Pepperoni Blast", "isGlutenFree": false }
```

#### GET non-existent pizza

```
Request:  GET http://localhost:5100/pizza/99
Response: 404 Not Found
```

#### POST — create new pizza

```
Request:  POST http://localhost:5100/pizza
Body:     { "name": "BBQ Chicken", "isGlutenFree": false }
Response: 201 Created
Body:     { "id": 4, "name": "BBQ Chicken", "isGlutenFree": false }
Header:   Location: http://localhost:5100/Pizza/4
```

#### PUT — update existing pizza

```
Request:  PUT http://localhost:5100/pizza/4
Body:     { "id": 4, "name": "BBQ Chicken Deluxe", "isGlutenFree": true }
Response: 204 No Content
```

#### DELETE — remove pizza

```
Request:  DELETE http://localhost:5100/pizza/4
Response: 204 No Content
```

---

## Part 2: Sales Summary Function (Work with Files & Directories)

```csharp
void GenerateSalesSummary(string[] files, string baseDirectory, string outputPath)
{
    double grandTotal = 0;
    var details = new List<(string RelativePath, double Total)>();

    // Read each file and accumulate totals
    foreach (var filePath in files)
    {
        var json      = File.ReadAllText(filePath);
        var data      = JsonConvert.DeserializeObject<SalesTotal>(json);
        var fileTotal = data?.Total ?? 0;

        grandTotal += fileTotal;

        // Use relative path so the report shows "201/sales.json" not the full disk path
        var relativePath = Path.GetRelativePath(baseDirectory, filePath);
        details.Add((relativePath, fileTotal));
    }

    // Build the report with StringBuilder
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

    // Write to file (overwrites if already exists)
    File.WriteAllText(outputPath, report.ToString());

    Console.WriteLine();
    Console.WriteLine("══════════════════════════════════════");
    Console.Write(report.ToString());
    Console.WriteLine("══════════════════════════════════════");
    Console.WriteLine($"Summary saved to: {outputPath}");
}
```

### Example output (summary.txt)

```
Sales Summary
----------------------------
 Total Sales: $106,406.25

 Details:
  201/sales.json: $22,385.32
  202/sales.json: $45,120.75
  203/sales.json: $38,900.18
```
