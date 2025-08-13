using System;

public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }

    public Product(string id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class StockSystem
{
    private Product[] products;
    private int count;
    private int capacity;

    public StockSystem(int capacity)
    {
        this.capacity = capacity;
        products = new Product[capacity];
        count = 0;
    }

    public bool AddProduct(string id, string name)
    {
        // Validar si ya existe un producto con el mismo ID o nombre
        for (int i = 0; i < count; i++)
        {
            if (products[i].Id == id)
            {
                Console.WriteLine("Error: Ya existe un producto con el mismo ID.");
                return false;
            }
            if (products[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Error: Ya existe un producto con el mismo nombre.");
                return false;
            }
        }

        if (count >= capacity)
            return false;

        products[count++] = new Product(id, name);
        return true;
    }

    public bool RemoveProduct(string id)
    {
        for (int i = 0; i < count; i++)
        {
            if (products[i].Id == id)
            {
                for (int j = i; j < count - 1; j++)
                    products[j] = products[j + 1];
                products[count - 1] = null!;
                count--;
                return true;
            }
        }
        return false;
    }

    public bool EditProduct(string id, string newName)
    {
        for (int i = 0; i < count; i++)
        {
            if (products[i].Id == id)
            {
                // Validar que el nuevo nombre no esté en otro producto
                for (int j = 0; j < count; j++)
                {
                    if (j != i && products[j].Name.Equals(newName, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Error: Ya existe un producto con ese nombre.");
                        return false;
                    }
                }

                products[i].Name = newName;
                return true;
            }
        }
        return false;
    }

    public void ListProducts()
    {
        if (count == 0)
        {
            Console.WriteLine("No hay productos en stock.");
            return;
        }

        Console.WriteLine("Productos en stock:");
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"- ID: {products[i].Id}, Nombre: {products[i].Name}");
        }
    }
}

partial class Program
{
    static void Main()
    {
        StockSystem stock = new StockSystem(10);
        while (true)
        {
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1. Agregar producto");
            Console.WriteLine("2. Eliminar producto");
            Console.WriteLine("3. Editar producto");
            Console.WriteLine("4. Listar productos");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");
            string? opcion = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(opcion))
            {
                Console.WriteLine("Opción inválida.");
                continue;
            }

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el nombre del producto a agregar: ");
                    string? prodName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(prodName))
                    {
                        Console.WriteLine("Nombre de producto inválido.");
                        break;
                    }
                    Console.Write("Ingrese el ID del producto: ");
                    string? prodId = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(prodId))
                    {
                        Console.WriteLine("ID de producto inválido.");
                        break;
                    }
                    if (stock.AddProduct(prodId, prodName))
                        Console.WriteLine("Producto agregado.");
                    else
                        Console.WriteLine("No se pudo agregar el producto.");
                    break;

                case "2":
                    Console.Write("Ingrese el ID del producto a eliminar: ");
                    string? prodRemoveId = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(prodRemoveId))
                    {
                        Console.WriteLine("ID de producto inválido.");
                        break;
                    }
                    if (stock.RemoveProduct(prodRemoveId))
                        Console.WriteLine("Producto eliminado.");
                    else
                        Console.WriteLine("Producto no encontrado.");
                    break;

                case "3":
                    Console.Write("Ingrese el ID del producto a editar: ");
                    string? prodEditId = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(prodEditId))
                    {
                        Console.WriteLine("ID de producto inválido.");
                        break;
                    }
                    Console.Write("Ingrese el nuevo nombre: ");
                    string? prodNewName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(prodNewName))
                    {
                        Console.WriteLine("Nuevo nombre inválido.");
                        break;
                    }
                    if (stock.EditProduct(prodEditId, prodNewName))
                        Console.WriteLine("Producto editado.");
                    else
                        Console.WriteLine("No se pudo editar el producto.");
                    break;

                case "4":
                    stock.ListProducts();
                    break;

                case "5":
                    MostrarEncuestaSatisfaccion();
                    return;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }

    static void MostrarEncuestaSatisfaccion()
    {
        Console.WriteLine("\n--- Encuesta de Satisfacción ---");
        Console.Write("¿Cómo calificaria el sistema? (1=Muy mala, 5=Excelente): ");
        string? calificacion = Console.ReadLine();
        int valor;
        if (!int.TryParse(calificacion, out valor) || valor < 1 || valor > 5)
        {
            Console.WriteLine("Calificación inválida. Se registrará como 'Sin respuesta'.");
        }
        else
        {
            Console.WriteLine($"Gracias por su calificación: {valor}");
        }
        Console.Write("¿Algún comentario? (opcional): ");
        string? comentario = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(comentario))
        {
            Console.WriteLine("Gracias por su comentario.");
        }
        Console.WriteLine("¡Gracias por usar el sistema!");
    }
}
