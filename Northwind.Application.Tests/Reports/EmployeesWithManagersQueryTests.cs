using Northwind.Application.Reports.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Northwind.Persistence;
using Xunit;

namespace Northwind.Application.Tests.Reports
{
    public class EmployeesWithManagersQueryTests : TestBase
    {
        [Fact]
        public async Task ShouldReturnReport()
        {
            using (var context = GetDbContext(useSqlLite: true))
            {
                NorthwindInitializer.Initialize(context);

                var query = new EmployeesWithManagersQuery();
                var queryHandler = new EmployeesWithManagersQueryHandler(context);
                var result = await queryHandler.Handle(query, CancellationToken.None);

                result.Should().NotBeEmpty();
                result.Should().HaveCount(8);
                result.Should().Contain(r => r.ManagerTitle == "Vice President, Sales");
                result.Should().NotContain(r => r.EmployeeTitle == "Vice President, Sales");
            }
        }
    }
}
