namespace AutoLot.Samples.Models;

[Table("Inventory", Schema = "dbo")]
[Index(nameof(MakeId), Name = "IX_Inventory_MakeId")]
[EntityTypeConfiguration(typeof(CarConfiguration))]
public class Car : BaseEntity
{
    // public string Color { get; set; }
    private string _color;
    [Required, StringLength(50)]
    public DateTime? DateBuilt { get; set; }
    public string Color
    {
        get => _color;
        set => _color = value;
    }
    [Required, StringLength(50)]
    public string PetName { get; set; }
    public int MakeId { get; set; }
    [ForeignKey(nameof(MakeId))]
    public virtual Make MakeNavigation { get; set; }
    public virtual Radio RadioNavigation { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string Display { get; set; }
    [InverseProperty(nameof(Driver.Cars))]
    public virtual IEnumerable<Driver> Drivers { get; set; } = new List<Driver>();
    private bool? _isDrivable;
    public bool IsDrivable
    {
        get => _isDrivable ?? true;
        set => _isDrivable = value;
    }

    [InverseProperty(nameof(CarDriver.CarNavigation))]
    public virtual IEnumerable<CarDriver> CarDrivers { get; set; } = new List<CarDriver>();
}