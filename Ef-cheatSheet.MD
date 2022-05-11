# Entity Framework Core Cheat Sheet


## Install Entity Framework

* Install Packages using the CLI

    * Install EF Tools
        * `dotnet add package Microsoft.EntityFrameworkCore.Tools`

    * Install EF Db Provider 
        * common providers
            * `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
            * `dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL`
            * `dotnet add package MongoDB.EntityFramework`
    * Install EF Desing
        * `dotnet add package Microsoft.EntityFrameworkCore.Design`

* Or Install Packages usign Nuget Gallery

        ![nugetGallery](https://user-images.githubusercontent.com/85702956/167743171-cb55338f-0cd9-45dd-88ad-d680e9f93a47.png)


## Conventions 

  * By convention, a property named `Id` or `<type name>Id` will be configured as the <strong>`primary key`</strong> of an entity.
  * Keys that are `int` or `Guid` -> *by convention, automatically configure the key as an identity column. (`indentity(1,1)` for int keys) * 

    ```csharp

    internal class Car
    {
        //Primary key
        public string Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }

    internal class Truck
    {
        //Primary key
        public string TruckId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
    ```
  
  * Avoid Nullable types WARNINGS => C# 8.0 enable nulleable types by default! 
    * avoid warnings using "?", "null!" operators on properties!!

        ```csharp
        public class Customer
            {
                public int Id { get; set; }
                // Any DataType can be declared non-nullable by assign it "null!" value. use "!" the null-forgiving operator.
                public string FirstName { get; set; } = null!;
                public string LastName { get; set; } = null!;
                // Any DataType can be declared nullable type with the help of operator "?". 
                public string? Address { get; set; }
                public string? Phone { get; set; }
                public string? Email { get; set; }
                public ICollection<Order> Orders { get; set; } = null!;
            }
        ```

    * avoid warnings by setting the nulleable context!!
        - on project.csproj set nullable disable
        /// img img 
  

<br>

* Configure your model using...
  * ...*Data Annotations*

  * Entity Framework uses `nvarchar(max)` for as default for string properties 
   - You can use DataAnnotations.Schema to change that...
    
        ```csharp
            public class Book
            {
                public int BookId { get; set; }
                // DataAnnotations
                [MaxLength(12), MinLength(5)]
                public string Title { get; set; }
            }
        ```
    - You can use DataAnnotations for REQUIRED properties 

        ```csharp
            public class Dog
            {
                public int ID { get; set; }
                // DataAnnotations
                [Required(ErrorMessage="Empty dog name")]
                [MaxLength(10,ErrorMessage="Up to 10 chars")]
                public string Name { get; set; }
            }
        ```
<br>

* (codefirst) database Tables <strong>NAMES</strong>
    * The Name of the `DbSet<Entity>` property, will be name of the table <br/>

## Create a *DbContext*

* `DbContextOptionsBuilder` in Constructor:

```csharp
//EF
using Microsoft.EntityFrameworkCore;
namespace ContosoPizza.Data; //Create this Dbcontext class on Data folder
//DbContext is like a representation of a session within the database
public class ContosoPizzaContext : DbContext 
{
    //Dbset maps to table that will be created in the datebase
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    //Override DbContext.OnConfiguring(DbContextOptionsBuilder options) and set string connection
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //its a bad practice use the string connection like this...
        options.UseSqlServer(@"Data Source=localhost; Initial Catalog=ContosoPizza; Integrated Security=true;"); 
    }

    //Run in CLI
    //1) `dotnet ef migrations add NameOfMigration`
    //2) `dotnet ef database update`
}
```


## Connection String in `appsettings.json`


* Add ConnectionString  to *appsettings.json* file:

```json
{
    "Logging" : {},
    "AllowedHost" : "*",
    "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\dev;Database=AddressBook;Trusted_Connection=True"
  }
}
```

* Read connection string in Program.cs file class:

```csharp
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.DependencyInjection;

builder.Services.AddEntityFrameworkProvider()
.AddDbContext<Context>(options => options
    .UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));
```
* Enable Dependency Injection on : `Context Class`

```csharp
//EF
using Microsoft.EntityFrameworkCore;
namespace ContosoPizza.Data; 
public class ContosoPizzaContext : DbContext 
{
    // add this instance 
    private readonly context;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    //Remove OnCofiguring override
    //And Add a this constructor!!!
    public ContosoPizza(Context context){
        this.context = context; 
    }

    //Now you can access to tables by context  
}
```
## Query Data

* [Documentation](https://docs.microsoft.com/en-us/ef/core/querying/)

|       Task        |                    Operation                    |
| ----------------- | ----------------------------------------------- |
| Load all data     | `ToListAsync`                                   |
| Load single row   | `Single`                                        |
| Filter            | `Where`                                         |
| Load related data | `Include`, `ThenInclude`, `Entry`, `Collection` |
| No-tracking query | `AsNoTracking`                                  |
| Raw SQL queries   | `FromSql`                                       |
| Sorting           | `OrderBy`, `OrderByDescending`                  |


## Saving Data

* [Documentation](https://docs.microsoft.com/en-us/ef/core/saving/)

|      Task       |     Operation      |
| --------------- | ------------------ |
| Add instance    | `Add`              |
| Delete instance | `Remove`           |
| Save            | `SaveChangesAsync` |

* Transactions:

```csharp
using (var transaction = context.Database.BeginTransaction())
{
    try
    {
        ...
        context.SaveChanges();
        ...
        context.SaveChanges();
        transaction.Commit();
    }
    catch (Exception)
    {
        // TODO: Handle failure
    }
}
```
## Manage DB Schema

* [Documentation](https://docs.microsoft.com/en-us/ef/core/managing-schemas/)

* Add Migration: `dotnet ef migrations add <MigrationName>`
* Update target database: `dotnet ef database update`
* Remove last Migration: `dotnet ef migrations remove`
* Generate SQL script from Migrations: `dotnet ef migrations script`
