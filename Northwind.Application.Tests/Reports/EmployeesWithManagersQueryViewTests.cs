using Dapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Reports.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Northwind.Persistence;
using Xunit;

namespace Northwind.Application.Tests.Reports
{
    public class EmployeesWithManagersQueryViewTests : TestBase
    {
        [Fact]
        public async Task ShouldReturnReport()
        {
            using (var context = GetDbContext(useSqlLite: true))
            {
                NorthwindInitializer.Initialize(context);

                context.Database.GetDbConnection().Execute(@"
CREATE VIEW viewEmployeesWithManagers(
        EmployeeFirstName, EmployeeLastName, EmployeeTitle,
        ManagerFirstName, ManagerLastName, ManagerTitle)
AS
SELECT e.FirstName as EmployeeFirstName, e.LastName as EmployeeLastName, e.Title as EmployeeTitle,
        m.FirstName as ManagerFirstName, m.LastName as ManagerLastName, m.Title as ManagerTitle
FROM employees AS e
JOIN employees AS m ON e.ReportsTo = m.EmployeeID
WHERE e.ReportsTo is not null");

                var query = new EmployeesWithManagersViewQuery();
                var queryHandler = new EmployeesWithManagersViewQueryHandler(context);
                var result = await queryHandler.Handle(query, CancellationToken.None);

                result.Should().NotBeEmpty();
                result.Should().HaveCount(8);
                result.Should().Contain(r => r.ManagerTitle == "Vice President, Sales");
                result.Should().NotContain(r => r.EmployeeTitle == "Vice President, Sales");
            }
        }
    }
}
