using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.Application.Tests.Infrastructure;
using Northwind.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Northwind.Application.Tests.Customers.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomerDetailQueryHandlerTests
    { 
        private readonly NorthwindDbContext _context;

        public GetCustomerDetailQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }    

        [Fact]
        public async Task GetCustomerDetail()
        {
            var sut = new GetCustomerDetailQueryHandler(_context);

            var result = await sut.Handle(new GetCustomerDetailQuery { Id = "JASON" }, CancellationToken.None);

            result.Should().BeOfType<CustomerDetailModel>();
            result.Id.Should().Be("JASON");
        }
    }
}
