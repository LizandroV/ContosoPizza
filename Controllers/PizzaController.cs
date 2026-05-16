// Controllers/PizzaController.cs
// El "Controller" es el que recibe las peticiones HTTP y las conecta con el Service.
// Cada metodo publico aqui = un endpoint de la API.
//
// [ApiController]  → le dice a ASP.NET que esta clase es un controlador de API
// [Route("[controller]")] → la ruta base sera /pizza (nombre de la clase sin "Controller")

using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController() { }

    // ─────────────────────────────────────────────────────────────────────────
    // GET /pizza
    // Devuelve la lista completa de pizzas
    // Respuesta exitosa: 200 OK + array JSON
    // ─────────────────────────────────────────────────────────────────────────
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
        PizzaService.GetAll();

    // ─────────────────────────────────────────────────────────────────────────
    // GET /pizza/{id}       ejemplo: GET /pizza/3
    // Devuelve una sola pizza que coincida con el Id
    // Respuesta exitosa:  200 OK + objeto JSON
    // Si no existe:       404 Not Found
    // ─────────────────────────────────────────────────────────────────────────
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza == null)
            return NotFound(); // 404

        return pizza; // 200
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST /pizza
    // Crea una nueva pizza con los datos del body JSON
    // Respuesta exitosa: 201 Created + objeto JSON con el Id asignado
    // Body esperado:  { "name": "...", "isGlutenFree": true/false }
    // ─────────────────────────────────────────────────────────────────────────
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        // CreatedAtAction genera el header "Location" apuntando al GET del nuevo recurso
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza); // 201
    }

    // ─────────────────────────────────────────────────────────────────────────
    // PUT /pizza/{id}       ejemplo: PUT /pizza/4
    // Actualiza una pizza existente con los datos del body JSON
    // Respuesta exitosa:  204 No Content (sin body)
    // Si el id del URL no coincide con el del body: 400 Bad Request
    // Si la pizza no existe: 404 Not Found
    // Body esperado:  { "id": 4, "name": "...", "isGlutenFree": true/false }
    // ─────────────────────────────────────────────────────────────────────────
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest(); // 400 - los ids no coinciden

        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
            return NotFound(); // 404

        PizzaService.Update(pizza);
        return NoContent(); // 204
    }

    // ─────────────────────────────────────────────────────────────────────────
    // DELETE /pizza/{id}    ejemplo: DELETE /pizza/4
    // Elimina la pizza con ese Id
    // Respuesta exitosa:  204 No Content
    // Si no existe:       404 Not Found
    // ─────────────────────────────────────────────────────────────────────────
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null)
            return NotFound(); // 404

        PizzaService.Delete(id);
        return NoContent(); // 204
    }
}
