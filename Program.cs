// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Net.Http.Headers;
using System.Xml;

List<Product> products = new List<Product>()
{
    new Product()
    {
        Name = "Football",
        Price = 15.00M,
        Sold = false,
        StockDate = new DateTime(2022, 10, 20),
        ManufactureYear = 2010,
        Condition = 4.2
    },
    new Product()
    {
        Name = "Hockey Stick",
        Price = 12.00M,
        Sold = false,
        StockDate = new DateTime(2022, 9, 28),
        ManufactureYear = 2012,
        Condition = 4.5
    },
    new Product()
    {
        Name = "Surfboard",
        Price = 50.00M,
        Sold = false,
        StockDate = new DateTime(2021, 2, 14),
        ManufactureYear = 2012,
        Condition = 4
    },
    new Product()
    {
        Name = "Tennis Racket",
        Price = 15.00M,
        Sold = false,
        StockDate = new DateTime(2021, 3, 7),
        ManufactureYear = 2012,
        Condition = 3.5
    },
    new Product()
    {
        Name = "Soccer Ball",
        Price = 10.00M,
        Sold = true,
        StockDate = new DateTime(2023, 12, 7),
        ManufactureYear = 2020,
        Condition = 4
    },
    new Product()
    {
        Name = "Volley Ball",
        Price = 10.00M,
        Sold = false,
        StockDate = new DateTime(2023, 12, 7),
        ManufactureYear = 2022,
        Condition = 5
    },
    new Product()
    {
        Name = "Bike Helmet",
        Price = 15.00M,
        Sold = false,
        StockDate = new DateTime(2023, 11, 17),
        ManufactureYear = 2020,
        Condition = 4
    }
};


string greeting = @"Welcome to Thrown For a Loop!
Your one-stop shop for used sporting equipment";

Console.WriteLine(greeting);


string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"Choose an option:
        0. Exit
        1. View All Products
        2. View Product Details
        3. View Latest Products");

    choice = Console.ReadLine();
    if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
    }
    else if (choice == "1")
    {
        ListProducts();
    }
    else if (choice == "2")
    {
        ViewProductDetails();
    }
    else if (choice == "3")
    {
        ViewLatestProducts();
    }
}


void ViewProductDetails()
{
    Console.WriteLine("Products:");
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {products[i].Name}");
    }

    Product chosenProduct = null;

    while (chosenProduct == null)
    {
        Console.WriteLine("Please enter a product number: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosenProduct = products[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do better!");
        }
    }

    DateTime now = DateTime.Now;

    TimeSpan timeInStock = now - chosenProduct.StockDate;

    Console.WriteLine(@$"You chose:
    {chosenProduct.Name}, which costs {chosenProduct.Price} dollars.
    It is {now.Year - chosenProduct.ManufactureYear} years old and its condition is rated {chosenProduct.Condition} out of 5.
    It {(chosenProduct.Sold ? "is not available" : $"has been in stock for {timeInStock.Days} days.")}");

}

void ListProducts()
{
    decimal totalValue = 0.0M;
    foreach (Product product in products)
    {
        if (!product.Sold)
        {
            totalValue += product.Price;
        }
    }
    Console.WriteLine($"Total inventory value: ${totalValue}");
    
}

void ViewLatestProducts()
{
    List<Product> latestProducts = new List<Product>();

    DateTime threeMonthsAgo = DateTime.Now - TimeSpan.FromDays(90);

    foreach (Product product in products)
    {
        if (product.StockDate > threeMonthsAgo && !product.Sold)
        {
            latestProducts.Add(product);
        }
    }

    for (int i = 0; i < latestProducts.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {latestProducts[i].Name}");
    }
}