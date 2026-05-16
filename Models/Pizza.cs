// Models/Pizza.cs
// Un "model" es simplemente una clase que describe la forma de los datos.
// Cada propiedad aqui se convierte en un campo JSON cuando la API responde.

namespace ContosoPizza.Models;

public class Pizza
{
    public int Id { get; set; }           // Identificador unico: 1, 2, 3...
    public string? Name { get; set; }      // Nombre: "Classic Italian", etc.
    public bool IsGlutenFree { get; set; } // true o false
}
