

// "******************************8* More Fun with Entity Framework Core *************************************

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;

ClearSampleData();
AddRecords();
ClearSampleData();
LoadMakeAndCarData();
QueryData();

void AddRecords()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    // var newMake = new Make { Name = "BMW" };
    // context.Makes.Add(newMake);
    // Console.WriteLine($"State of the {newMake.Name} is {context.Entry(newMake).State}");
    // context.SaveChanges();
    // Console.WriteLine($"State of the {newMake.Name} is {context.Entry(newMake).State}");

    // var newCar = new Car()
    // {
    //     Color = "Blue",
    //     DateBuilt = new DateTime(2016, 12, 01),
    //     IsDrivable = true,
    //     PetName = "Bluesmobile",
    //     MakeId = newMake.Id
    // };

    // Console.WriteLine($"State of the {newCar.PetName} is {context.Entry(newCar).State}");
    // context.Cars.Attach(newCar);
    // Console.WriteLine($"State of the {newCar.PetName} is {context.Entry(newCar).State}");
    // context.SaveChanges();
    // Console.WriteLine($"State of the {newCar.PetName} is {context.Entry(newCar).State}");

    //     var cars = new List<Car>
    //  {
    //   new() {Color = "Yellow",MakeId = newMake.Id, PetName = "Herbie"},
    //   new() {Color = "White",MakeId = newMake.Id, PetName = "March 5"},
    //   new() {Color = "Pink",MakeId = newMake.Id, PetName = "Avon"},
    //   new() {Color = "Blue",MakeId = newMake.Id, PetName = "Bluenerry"},
    //  };
    //     context.Cars.AddRange(cars);
    //     context.SaveChanges();

    // IEntityType metadata = context.Model.FindEntityType(typeof(Car).FullName);
    // var schema = metadata.GetSchema();
    // var tableName = metadata.GetTableName();

    // var strategy = context.Database.CreateExecutionStrategy();
    // strategy.Execute(() =>
    // {
    //     using var trans = context.Database.BeginTransaction();
    //     try
    //     {
    //         context.Database.ExecuteSqlRaw(
    //      $"SET IDENTITY_INSERT {schema}.{tableName} ON"
    //         );
    //         var anotherNewCar = new Car()
    //         {
    //             Id = 27,
    //             Color = "Blue",
    //             DateBuilt = new DateTime(2016, 12, 01),
    //             IsDrivable = true,
    //             PetName = "Bluesmobile",
    //             MakeId = newMake.Id
    //         };
    //         context.Cars.Add(anotherNewCar);
    //         context.SaveChanges();
    //         trans.Commit();
    //         Console.WriteLine($"Insert succeeded");
    //     }
    //     catch (Exception ex)
    //     {
    //         trans.Rollback();
    //         Console.WriteLine($"Insert failed: {ex.Message}");
    //     }
    //     finally
    //     {
    //         context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT  {schema}.{tableName} OFF");
    //     }
    // });

    // var anotherMake = new Make { Name = "Honda" };
    // var car = new Car { Color = "Yellow", PetName = "Herbie" };
    // // Cast the Cars property to List<Car> from IEnumerable<Car>
    // ((List<Car>)anotherMake.Cars).Add(car);
    // context.Makes.Add(anotherMake);
    // context.SaveChanges();


    //M2M
    //     var drivers = new List<Driver>
    // {
    // new() { PersonInfo = new Person { FirstName = "Fred", LastName = "Flinstone" } },
    // new() { PersonInfo = new Person { FirstName = "Wilma", LastName = "Flinstone" } },
    // new() { PersonInfo = new Person { FirstName = "BamBam", LastName = "Flinstone" } },
    // new() { PersonInfo = new Person { FirstName = "Barney", LastName = "Rubble" } },
    // new() { PersonInfo = new Person { FirstName = "Betty", LastName = "Rubble" } },
    // new() { PersonInfo = new Person { FirstName = "Pebbles", LastName = "Rubble" } },
    // };
    //     var carsForM2M = context.Cars.Take(2).ToList();
    //     //Cast the IEnumerable to a List to access the Add method
    //     //Range support works with LINQ to Objects, but is not translatable to SQL calls
    //     ((List<Driver>)carsForM2M[0].Drivers).AddRange(drivers.Take(..3));
    //     ((List<Driver>)carsForM2M[1].Drivers).AddRange(drivers.Take(3..));
    //     context.SaveChanges();



}


//Add Sample Records
static void LoadMakeAndCarData()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    List<Make> makes = new()
{
new() { Name = "VW" },
new() { Name = "Ford" },
new() { Name = "Saab" },
new() { Name = "Yugo" },
new() { Name = "BMW" },
new() { Name = "Pinto" },
};
    context.Makes.AddRange(makes);
    context.SaveChanges();

    List<Car> inventory = new()
        {
            new() { MakeId = 1, Color = "Black", PetName = "Zippy"
            },
            new() { MakeId = 2, Color = "Rust", PetName = "Rusty"
            },
            new() { MakeId = 3, Color = "Black", PetName = "Mel"
            },
            new() { MakeId = 4, Color = "Yellow", PetName = "Clunker"
            },
            new() { MakeId = 5, Color = "Black", PetName = "Bimmer"
            },
            new() { MakeId = 5, Color = "Green", PetName = "Hank"
            },
            new() { MakeId = 5, Color = "Pink", PetName = "Pinky"
            },
new() { MakeId = 6, Color = "Black", PetName = "Pete" },
new() { MakeId = 4, Color = "Brown", PetName = "Brownie" },
new() { MakeId = 1, Color = "Rust", PetName = "Lemon", IsDrivable = false },
};
    context.Cars.AddRange(inventory);
    context.SaveChanges();
}

static void ClearSampleData()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var entities = new[]
    {
typeof(Driver).FullName,
typeof(Car).FullName,
typeof(Make).FullName,
};
    foreach (var entityName in entities)
    {
        var entity = context.Model.FindEntityType(entityName);
        var tableName = entity.GetTableName();
        var schemaName = entity.GetSchema();
        context.Database.ExecuteSqlRaw($"DELETE FROM {schemaName}.{tableName}");
        context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT (\"{schemaName}.{tableName}\",RESEED, 0); ");

    }
}


static void QueryData()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    //Return all of the cars
    IQueryable<Car> cars = context.Cars;
    foreach (Car c in cars)
    {
        Console.WriteLine($"{c.PetName} is {c.Color}");
    }
    //Clean up the context
    context.ChangeTracker.Clear();
    List<Car> cars2 = context.Cars.ToList();
    foreach (Car c in cars2)
    {
        Console.WriteLine($"{c.PetName} is {c.Color}");
    }
}


static void FilterData()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    //Return all yellow cars
    IQueryable<Car> cars = context.Cars.Where(c => c.Color == "Yellow");
    Console.WriteLine("Yellow cars");
    foreach (Car c in cars)
    {
        Console.WriteLine($"{c.PetName} is {c.Color}");
    }
    context.ChangeTracker.Clear();

    //Return all yellow cars with a petname of Clunker
    IQueryable<Car> cars2 = context.Cars.Where(c => c.Color == "Yellow" && c.PetName == "Clunker");
    Console.WriteLine("Yellow cars and Clunkers");
    foreach (Car c in cars2)
    {
        Console.WriteLine($"{c.PetName} is {c.Color}");
    }
    context.ChangeTracker.Clear();

    // Return all yellow cars with a petname of Clunker
    IQueryable<Car> cars3 = context.Cars.Where(c => c.Color == "Yellow").Where(c => c.PetName == "Clunker");
    Console.WriteLine("Yellow cars and Clunkers");
    foreach (Car c in cars3)
    {
        Console.WriteLine($"{c.PetName} is {c.Color}");
    }
    context.ChangeTracker.Clear();

    //Return all yellow cars or cars with PetName of Clunker
    IQueryable<Car> cars4 = context.Cars.Where(c => c.Color == "Yellow" || c.PetName == "Clunker");
    foreach (Car c in cars4)
    {
        Console.WriteLine($"{c.PetName} is {c.Color}");
    }
    context.ChangeTracker.Clear();

    IQueryable<Car> cars5 = context.Cars.Where(c => !string.IsNullOrWhiteSpace(c.Color));
    foreach (Car c in cars5)
    {
        Console.WriteLine($"{c.PetName} is {c.Color}");
    }
    context.ChangeTracker.Clear();

}

static void SortData()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    //Return all cars ordered by color
    IOrderedQueryable<Car> cars = context.Cars.OrderBy(c => c.Color);
    Console.WriteLine("Cars ordered by Color");
    foreach (Car c in cars)
    {
        Console.WriteLine($"{c.PetName} is {c.Color}");
    }
    context.ChangeTracker.Clear();
    //Return all cars ordered by color then Petname
    IOrderedQueryable<Car> cars1 = context.Cars.OrderBy(c => c.Color).ThenBy(c => c.PetName);
    Console.WriteLine("Cars ordered by Color then PetName");
    foreach (Car c in cars1)
    {
        Console.WriteLine($"{c.PetName} is {c.Color}");
    }
    context.ChangeTracker.Clear();
    //Return all cars ordered by color Descending
    IOrderedQueryable<Car> cars2 = context.Cars.OrderByDescending(c => c.Color);
    Console.WriteLine("Cars ordered by Color descending");
    foreach (Car c in cars2)
    {
        Console.WriteLine($"{c.PetName} is {c.Color}");
    }
    context.ChangeTracker.Clear();

    //Return all cars ordered by Color then by PetName descending
    IOrderedQueryable<Car> cars3 = context.Cars.OrderBy(c => c.Color).ThenByDescending(c => c.PetName);
    Console.WriteLine("Cars ordered by Color then by PetName descending");
    foreach (Car c in cars3)
    {
        Console.WriteLine($"{c.PetName} is {c.Color}");
    }
    context.ChangeTracker.Clear();
}

static void Paging()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    Console.WriteLine("Paging");
    //Skip the first two records
    var cars = context.Cars.Skip(2).ToList();
    context.ChangeTracker.Clear();

    // Take the first two records
    cars = context.Cars.Take(2).ToList();

    //Skip the first two records and take the next two records
    cars = context.Cars.Skip(2).Take(2).ToList();

    //Retrieve a Single Record
    context.Cars.Where(c => c.Id < 5).First();
    context.Cars.First(c => c.Id < 5);
}

static void SingleRecordQueries()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    Console.WriteLine("Single Record with database Sort");
    var firstCar = context.Cars.First();
    Console.WriteLine($"{firstCar.PetName} is {firstCar.Color}");
    context.ChangeTracker.Clear();

    Console.WriteLine("Single Record with OrderBy sort");
    var firstCarByColor = context.Cars.OrderBy(c => c.Color).First();
    Console.WriteLine($"{firstCarByColor.PetName} is {firstCarByColor.Color}");
    context.ChangeTracker.Clear();

    Console.WriteLine("Single Record with Where clause");
    var firstCarIdThree = context.Cars.Where(c => c.Id == 3).First();
    Console.WriteLine($"{firstCarIdThree.PetName} is {firstCarIdThree.Color}");
    context.ChangeTracker.Clear();

    Console.WriteLine("Single Record Using First as Where clause");
    var firstCarIdThree1 = context.Cars.First(c => c.Id == 3);
    Console.WriteLine($"{firstCarIdThree1.PetName} is {firstCarIdThree1.Color}");
    context.ChangeTracker.Clear();

    // Console.WriteLine("Exception when no record is found");
    // try
    // {
    //     var firstCarNotFound = context.Cars.First(c => c.Id == 27);
    // }
    // catch (InvalidOperationException ex)
    // {
    //     Console.WriteLine(ex.Message);
    // }
    // context.ChangeTracker.Clear();

    Console.WriteLine("Return Default (null) when no record is found");
    var firstCarNotFound = context.Cars.FirstOrDefault(c => c.Id == 27);
    Console.WriteLine(firstCarNotFound == null);
    context.ChangeTracker.Clear();

    Console.WriteLine("Exception with Last and no order by");

    try
    {
        context.Cars.Last();

    }
    catch (InvalidOperationException ex)
    {

        Console.WriteLine(ex.Message);
    }

    Console.WriteLine("Get last record sorted by Color");
    var lastCar = context.Cars.OrderBy(c => c.Color).Last();
    Console.WriteLine(firstCarNotFound == null);
    context.ChangeTracker.Clear();

    Console.WriteLine("Exception when more than one record is found");
    try
    {
        context.Cars.Single(c => c.Id > 1);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
    }

    context.ChangeTracker.Clear();
    Console.WriteLine("Exception when no records are found");
    try
    {
        context.Cars.Single(c => c.Id == 27);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
    }
    context.ChangeTracker.Clear();

    Console.WriteLine("Exception when more than one record is found");
    try
    {
        context.Cars.SingleOrDefault(c => c.Id > 1);
    }
    catch (InvalidOperationException ex)
    {

        Console.WriteLine(ex.Message);
    }
    context.ChangeTracker.Clear();

    var defaultWhenSingleNotFoundCar = context.Cars.SingleOrDefault(c => c.Id == 27);
    context.ChangeTracker.Clear();

}

static void Aggregation()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var count = context.Cars.Count();

    var countByMake = context.Cars.Count(x => x.MakeId == 1);
    Console.WriteLine($"Count: {countByMake}");
    var countByMake2 = context.Cars.Where(x => x.MakeId == 1).Count();
    Console.WriteLine($"Count: {countByMake2}");

    var max = context.Cars.Max(x => x.Id);
    var min = context.Cars.Min(x => x.Id);
    var avg = context.Cars.Average(x => x.Id);
    Console.WriteLine($"Max ID: {max} Min ID: {min} Ave ID: {avg}");
}

static void AnyAndAll()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var resultAny = context.Cars.Any(x => x.MakeId == 1);
    //This executes the same query as the preceding line
    var resultAnyWithWhere = context.Cars.IgnoreQueryFilters().Where(x => x.MakeId == 1).Any();

    Console.WriteLine($"Exist? {resultAny}");
    Console.WriteLine($"Exist? {resultAnyWithWhere}");

    var resultAll = context.Cars.All(x => x.MakeId == 1);
    Console.WriteLine($"All? {resultAll}");
}

static string GetPetName(ApplicationDbContext context, int id)
{
    var parameterId = new SqlParameter
    {
        ParameterName = "@carId",
        SqlDbType = System.Data.SqlDbType.Int,
        Value = id,
    };
    var parameterName = new SqlParameter
    {
        ParameterName = "@petName",
        SqlDbType = System.Data.SqlDbType.NVarChar,
        Size = 50,
        Direction = ParameterDirection.Output
    };
    var result = context.Database.ExecuteSqlRaw("EXEC [dbo].[GetPetName] @carId, @petName OUTPUT", parameterId,
parameterName);
    return (string)parameterName.Value;
}

static void GetDataFromStoredProc()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var cars = context.Cars.IgnoreQueryFilters().ToList();
    foreach (var c in cars)
    {
        Console.WriteLine($"PetName: {GetPetName(context, c.Id)}");
    }
}


static void RelatedData()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var carsWithMakes = context.Cars.Include(c => c.MakeNavigation).ToList();
    context.ChangeTracker.Clear();

    var makesWithCarsAndDrivers = context.Makes.Include(c => c.Cars).ThenInclude(d => d.Drivers).ToList();

    //Add to keep the demos clean
    context.ChangeTracker.Clear();
    var makesWithYellowCars = context.Makes.Include(x => x.Cars.Where(x => x.Color == "Yellow")).ToList();


    //Add to keep the demos clean
    context.ChangeTracker.Clear();
    var splitMakes = context.Makes.AsSplitQuery()
    .Include(x => x.Cars.Where(x => x.Color == "Yellow")).ToList();

    var carsAndDrivers = context.Cars.Include(x => x.Drivers).Where(x => x.Drivers.Any());

    //Get the Car record
    var car = context.Cars.First(x => x.Id == 1);
    // Get the Make information
    context.Entry(car).Reference(c => c.MakeNavigation).Load();
    //Get the Drivers the Car is related to
    context.Entry(car).Collection(c => c.Drivers).Query().Load();

}


static void LazyLoadCar()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(new string[] { "lazy" });

    var query = context.Cars.AsQueryable();
    var cars = query.ToList();
    var make = cars[0].MakeNavigation;
    Console.WriteLine(make.Name);
}

static void UpdateRecords()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var car = context.Cars.First();
    car.Color = "Green";
    context.SaveChanges();

    var carToUpdate = context.Cars.AsNoTracking().First(x => x.Id == 1);
    carToUpdate.Color = "Orange";
    context.Cars.Update(carToUpdate);
    context.SaveChanges();

    context.ChangeTracker.Clear();
    var carToUpdate2 = context.Cars.AsNoTracking().First(x => x.Id == 1);
    carToUpdate2.Color = "Orange";
    context.Entry(carToUpdate2).State = EntityState.Modified;
    context.SaveChanges();
}

static void DeleteRecords()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    ClearSampleData();
    LoadMakeAndCarData();
    var car = context.Cars.First(x => x.Color != "Green");
    context.Cars.Remove(car);
    context.SaveChanges();

    Console.WriteLine($"{car.PetName}'s state is {context.Entry(car).State}");

    context.ChangeTracker.Clear();
    var carToDelete = context.Cars.AsNoTracking().First(x => x.Color != "Green");
    context.Cars.Remove(carToDelete);
    context.SaveChanges();
    context.ChangeTracker.Clear();
    var carToDelete2 = context.Cars.AsNoTracking().First(x => x.Color != "Green");
    context.Entry(carToDelete2).State = EntityState.Deleted;
    context.SaveChanges();


    context.ChangeTracker.Clear();
    var make = context.Makes.First();
    context.Makes.Remove(make);
    try
    {
        context.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void QueryFilters()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var cars = context.Cars.ToList();
    Console.WriteLine($"Total number of drivable cars: {cars.Count}");

    // var allCars = context.Cars.IgnoreQueryFilters().ToList();
    // Console.WriteLine($"Total number of cars: {allCars.Count}");
    // var radios = context.Radios.ToList();
    // var allRadios = context.Radios.IgnoreQueryFilters().ToList();

    var radios = context.Radios.ToList();

    var allRadios = context.Radios.IgnoreQueryFilters().ToList();
}

static void RelatedDataWithQueryFilters()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var make = context.Makes.First(x => x.Id == 1);
    //Get the Cars collection
    context.Entry(make).Collection(c => c.Cars).Load();
    context.ChangeTracker.Clear();

    //Get the Cars collection
    context.Entry(make).Collection(c => c.Cars).Query().IgnoreQueryFilters().Load();
}

static void UsingFromSql()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    IEntityType metadata = context.Model.FindEntityType(typeof(Car).FullName);
    Console.WriteLine(metadata.GetSchema());
    Console.WriteLine(metadata.GetTableName());

    int carId = 1;
    var car = context.Cars
    .FromSqlInterpolated($"Select * from dbo.Inventory where Id = {carId}")
    .Include(x => x.MakeNavigation)
    .First();
}

static void Projections()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    List<int> ids = context.Cars.Select(x => x.Id).ToList();

    var vms = context.Cars.Select(x => new CarMakeViewModel
    {
        CarId = x.Id,
        Color = x.Color,
        DateBuilt = x.DateBuilt.GetValueOrDefault(new DateTime(2020, 01, 01)),
        Display = x.Display,
        IsDrivable = x.IsDrivable,
        Make = x.MakeNavigation.Name,
        MakeId = x.MakeId,
        PetName = x.PetName
    });
    foreach (CarMakeViewModel c in vms)
    {
        Console.WriteLine($"{c.PetName} is a {c.Make}");
    }
}
static void AddACar()
{
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var car = new Car
    {
        Color = "Yellow",
        MakeId = 1,
        PetName = "Herbie"
    };
    context.Cars.Add(car);
    context.SaveChanges();
}
static void AddACarWithDefaultsSet()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var car = new Car
    {
        Color = "Yellow",
        MakeId = 1,
        PetName = "Herbie",
        IsDrivable = true,
        DateBuilt = new DateTime(2021, 01, 01)
    };
    context.Cars.Add(car);
    context.SaveChanges();
}

static void UpdateACar()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var car = context.Cars.First(c => c.Id == 1);
    car.Color = "White";
    context.SaveChanges();
}

static void ThrowConcurrencyException()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    try
    {
        //Get a car record (doesn't matter which one)
        var car = context.Cars.First();
        //Update the database outside of the context
        context.Database.ExecuteSqlInterpolated($"Update dbo.Inventory set Color='Pink' where Id= {car.Id}");
        //update the car record in the change tracker and then try and save changes
        car.Color = "Yellow";
        context.SaveChanges();
    }
    catch (DbUpdateConcurrencyException ex)
    {
        //Get the entity that failed to update
        var entry = ex.Entries[0];
        //Get the original values (when the entity was loaded)
        PropertyValues originalProps = entry.OriginalValues;
        //Get the current values (updated by this code path)
        PropertyValues currentProps = entry.CurrentValues;
        //get the current values from the data store –
        //Note: This needs another database call
        PropertyValues databaseProps = entry.GetDatabaseValues();
    }

    try
    {
        context.SaveChanges();
    }
    catch (RetryLimitExceededException ex)
    {
        //A retry limit error occurred
        //Should handle intelligently
        Console.WriteLine($"Retry limit exceeded! {ex.Message}");
    }
}

static void TransactionWithExecutionStrategies()

{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var strategy = context.Database.CreateExecutionStrategy();
    strategy.Execute(() =>
    {
        using var trans = context.Database.BeginTransaction();
        try
        {
            //actionToExecute();
            trans.Commit();
        }
        catch (Exception ex)
        {
            trans.Rollback();
        }
    });
}

static void UseEFFunctions()
{
    Console.WriteLine("Using Like");
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);

    //Same as contains
    var cars = context.Cars.IgnoreQueryFilters().Where(x => EF.Functions.Like(x.PetName, "%Clunk%")).ToList();
    foreach (var c in cars)
    {
        Console.WriteLine($"{c.PetName} was found");
    }

    //Same as Starts with
    cars = context.Cars.IgnoreQueryFilters().Where(x => EF.Functions.Like(x.PetName, "Clun%")).ToList();
    foreach (var c in cars)
    {
        Console.WriteLine($"{c.PetName} was found");
    }

    //Same as Ends with
    cars = context.Cars.IgnoreQueryFilters().Where(x => EF.Functions.Like(x.PetName, "%er")).ToList();
    foreach (var c in cars)
    {
        Console.WriteLine($"{c.PetName} was found");
    }
}

static void Batching()
{
    //The factory is not meant to be used like this, but it's demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var cars = new List<Car>
    {
        new Car { Color = "Yellow", MakeId = 1, PetName = "Herbie" },
        new Car { Color = "White", MakeId = 2, PetName = "Mach 5" },
        new Car { Color = "Pink", MakeId = 3, PetName = "Avon" },
        new Car { Color = "Blue", MakeId = 4, PetName = "Blueberry" },
    };
    context.Cars.AddRange(cars);
    context.SaveChanges();
}
// "****************************** Building blocks of Entity Framework Core *************************************

// SampleSaveChanges();

// static void SampleSaveChanges()
// {
//  //The factory is not meant to be used like this, but it’s demo code :-)
//  var context = new ApplicationDbContextFactory().CreateDbContext(null);
//  // make some changes
//  context.SaveChanges();
// }

// static void TransactedSaveChanges()
// {
//  //The factory is not meant to be used like this, but it’s demo code :-)
//  var context = new ApplicationDbContextFactory().CreateDbContext(null);
//  using var trans = context.Database.BeginTransaction();

//  try
//  {
//   //Create, change, delete stuff
//   context.SaveChanges();
//   trans.Commit();
//  }
//  catch (Exception ex)
//  {

//   trans.Rollback();
//  }
// }

// static void UsingSavePoints()
// {
//  //The factory is not meant to be used like this, but it’s demo code :-)
//  var context = new ApplicationDbContextFactory().CreateDbContext(null);
//  using var trans = context.Database.BeginTransaction();
//  try
//  {
//   //Create, change, delete stuff
//   trans.CreateSavepoint("check point 1");
//   context.SaveChanges();
//   trans.Commit();
//  }
//  catch (Exception ex)
//  {
//   trans.RollbackToSavepoint("check point 1");
//  }
// }

// static void TransactionWithExecutionStrategies()
// {
//  //The factory is not meant to be used like this, but it’s demo code :-)
//  var context = new ApplicationDbContextFactory().CreateDbContext(null);
//  var strategy = context.Database.CreateExecutionStrategy();
//  strategy.Execute(() =>
//  {
//   using var trans = context.Database.BeginTransaction();
//   try
//   {
//    //actionToExecute();
//    trans.Commit();
//    Console.WriteLine("Insert succeeded");
//   }
//   catch (Exception ex)
//   {
//    trans.Rollback();
//    Console.WriteLine($"Insert failed: {ex.Message}");
//   }
//  });
// }

// static void DbConstraint()
// {
//  var context = new ApplicationDbContextFactory().CreateDbContext(null);
//  context.Makes.Add(new Make { Name = "Lemon" });
//  context.SaveChanges();
// }