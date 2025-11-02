namespace AutoLot.Samples.Models;

[EntityTypeConfiguration(typeof(DriverConfiguration))]
public class Driver : BaseEntity
{
 // public string FirstName { get; set; }
 // public string LastName { get; set; }

 public Person PersonInfo { get; set; } = new Person();
 public virtual IEnumerable<Car> Cars { get; set; } = new List<Car>();

 [InverseProperty(nameof(CarDriver.DriverNavigation))]
 public virtual IEnumerable<CarDriver> CarDrivers { get; set; } = new List<CarDriver>();

}