//CustomerOrderViewModelTests.cs
namespace AutoLot.Dal.Tests.IntegrationTests;

[Collection("Integation Tests")]
public class CustomerOrderViewModelTests
: BaseTest, IClassFixture<EnsureAutoLotDatabaseTestFixture>
{
    private readonly ICustomerOrderViewModelRepo _repo;
    public CustomerOrderViewModelTests(ITestOutputHelper outputHelper)
    : base(outputHelper)
    {
        _repo = new CustomerOrderViewModelRepo(Context);
    }
    public override void Dispose()
    {
        _repo.Dispose();
        base.Dispose();
    }

    [Fact]
    public void ShouldGetAllViewModels()
    {
        var qs = Context.CustomerOrderViewModels.ToQueryString();
        OutputHelper.WriteLine($"Query: {qs}");
        List<Models.ViewModels.CustomerOrderViewModel> list =
        Context.CustomerOrderViewModels.ToList();
        Assert.NotEmpty(list);
        Assert.Equal(5, list.Count);
    }

    [Fact]
    public void ShouldGetCustomersWithLastNameW()
    {
        IQueryable<Customer> query = Context.Customers
        .Where(x => x.PersonInformation.LastName.StartsWith("W"));
        var qs = query.ToQueryString();
        OutputHelper.WriteLine($"Query: {qs}");
        List<Customer> customers = query.ToList();
        Assert.Equal(2, customers.Count);
        foreach (var customer in customers)
        {
            var pi = customer.PersonInformation;
            Assert.StartsWith("W", pi.LastName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
