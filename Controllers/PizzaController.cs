using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
 
namespace ContosoPizza.Controllers;
 
[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController() { }
 
    // ─── GET /pizza ────────────────────────────────────────────────────────────
    // Returns all pizzas → 200 OK
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
        PizzaService.GetAll();
 
    // ─── GET /pizza/{id} ───────────────────────────────────────────────────────
    // Returns one pizza by id → 200 OK  |  404 Not Found
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);
 
        if (pizza == null)
            return NotFound();
 
        return pizza;
    }
 
    // ─── POST /pizza ───────────────────────────────────────────────────────────
    // Creates a new pizza → 201 Created
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }
 
    // ─── PUT /pizza/{id} ───────────────────────────────────────────────────────
    // Updates an existing pizza → 204 No Content  |  400 Bad Request  |  404 Not Found
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();
 
        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();
 
        PizzaService.Update(pizza);
 
        return NoContent();
    }
 
    // ─── DELETE /pizza/{id} ────────────────────────────────────────────────────
    // Deletes a pizza → 204 No Content  |  404 Not Found
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);
 
        if (pizza is null)
            return NotFound();
 
        PizzaService.Delete(id);
 
        return NoContent();
    }
}