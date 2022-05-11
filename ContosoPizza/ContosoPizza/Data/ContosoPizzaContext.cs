namespace ContosoPizza.Data;

using ContosoPizza.Models; 

//EF
using Microsoft.EntityFrameworkCore;
//DbContext is like a representation of a session within the database
public class ContosoPizzaContext : DbContext 
{
    //Dbset maps to table that will be created in the datebase
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetailss { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //its a bad practice use the string connection like this...
        options.UseSqlServer(@"Data Source=localhost; Initial Catalog=ContosoPizza; Integrated Security=true;"); 
    }

    //Run in cli
    //1) dotnet ef migrations add NameOfMigration
    //2) dotnet ef database update

}