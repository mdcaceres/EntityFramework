using System.Diagnostics;
using ContosoPizza.Models;
using ContosoPizza.Data;

class program {
    public static void Main(string[] args){
        /*what is doing using???
        usind declarations: tells the compiler that eventually call dispose() on context variable 
        nd gets rid of the unmaneged resources... 
        this ensure that ContosoPizzaContext object disposed off properly when we´re done using it.*/ 
        using ContosoPizzaContext context = new ContosoPizzaContext();
        
        //Create Products in Data Base}

        // Product veggieSpecial = new Product()
        // {
        //     Name = "Veggie Special Pizza",
        //     Price = 9.99M
        // }; 
        // context.Products.Add(veggieSpecial); 

        // Product deluxMeat = new Product()
        // {
        //     Name = "Deluxe Meat Pizza",
        //     Price = 12.99M
        // };
        // context.Products.Add(deluxMeat); 
        // context.SaveChanges();

        //use Fluent API or LINQ to get data from data base

        //DELETE BY ID
        
        // var veggieSpecial = context.Products
        // .Where(x => x.Id == 4)
        // .FirstOrDefault();

        // System.Console.WriteLine("veggieSpecial is Product??"); 

        // System.Console.WriteLine(veggieSpecial is Product); 

        // System.Console.WriteLine("veggieSpecial.props"); 
        // System.Console.WriteLine(veggieSpecial.Id);
        // System.Console.WriteLine(veggieSpecial.Name);
        // System.Console.WriteLine(veggieSpecial.Price);

        // System.Console.WriteLine("context.Products.Remove(veggieSpecial)");
        // System.Console.WriteLine(context.Remove(veggieSpecial));
        // System.Console.WriteLine("se borro");

        // context.SaveChanges(); 

    }
}