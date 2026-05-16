// Services/PizzaService.cs
// El "Service" es la capa de datos: guarda la lista en memoria y
// expone metodos para leer, agregar, actualizar y eliminar pizzas.
// En una app real esto conectaria a una base de datos (SQL, etc.).

using ContosoPizza.Models;

namespace ContosoPizza.Services;

public static class PizzaService
{
    // Lista en memoria: se reinicia cada vez que reinicias dotnet run
    static List<Pizza> Pizzas { get; }

    // El proximo Id que se asignara al hacer POST
    // Empieza en 4 porque la lista inicial tiene 3 pizzas (ids 1, 2 y 3)
    static int nextId = 4;

    // Constructor estatico: se ejecuta una sola vez al arrancar la app
    static PizzaService()
    {
        Pizzas = new List<Pizza>
        {
            new Pizza { Id = 1, Name = "Classic Italian",  IsGlutenFree = false },
            new Pizza { Id = 2, Name = "Veggie",           IsGlutenFree = true  },
            // ✅ Registro adicional requerido por la tarea
            new Pizza { Id = 3, Name = "Pepperoni Blast",  IsGlutenFree = false }
        };
    }

    // Devuelve todas las pizzas
    public static List<Pizza> GetAll() => Pizzas;

    // Busca una pizza por Id; devuelve null si no existe
    public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    // Agrega una nueva pizza asignandole el siguiente Id disponible
    public static void Add(Pizza pizza)
    {
        pizza.Id = nextId++;
        Pizzas.Add(pizza);
    }

    // Elimina una pizza por Id
    public static void Delete(int id)
    {
        var pizza = Get(id);
        if (pizza is null)
            return;

        Pizzas.Remove(pizza);
    }

    // Reemplaza una pizza existente con los nuevos datos
    public static void Update(Pizza pizza)
    {
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        if (index == -1)
            return;

        Pizzas[index] = pizza;
    }
}
