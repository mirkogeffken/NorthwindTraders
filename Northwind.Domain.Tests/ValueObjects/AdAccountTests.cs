using System;
using System.Threading.Tasks;
using FluentAssertions;
using Northwind.Domain.Exceptions;
using Northwind.Domain.ValueObjects;
using Xunit;

namespace Northwind.Domain.Tests.ValueObjects
{
    public class AdAccountTests
    {
        [Fact]
        public void ShouldHaveCorrectDomainAndName()
        {
            var account = AdAccount.For("SSW\\Jason");

            account.Domain.Should().Be("SSW");
            account.Name.Should().Be("Jason");
        }

        [Fact]
        public void ToStringReturnsCorrectFormat()
        {
            const string value = "SSW\\Jason";

            var account = AdAccount.For(value);

            account.ToString().Should().Be(value);
        }

        [Fact]
        public void ImplicitConversionToStringResultsInCorrectString()
        {
            const string value = "SSW\\Jason";

            var account = AdAccount.For(value);

            string result = account;

            result.Should().Be(value);
        }

        [Fact]
        public void ExplicitConversionFromStringSetsDomainAndName()
        {
            var account = (AdAccount) "SSW\\Jason";

            account.Domain.Should().Be("SSW");
            account.Name.Should().Be("Jason");
        }

        [Fact]
        public void ShouldThrowAdAccountInvalidExceptionForInvalidAdAccount()
        {
            Func<AdAccount> action = () => (AdAccount) "SSWJason";
            action.Should().Throw<AdAccountInvalidException>();
        }
    }
}
