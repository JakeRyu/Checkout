using Domain.Entities;
using Xunit;
using FluentAssertions;

namespace Domain.UnitTests
{
    public class PaymentTests
    {
        private readonly Payment sut;

        public PaymentTests()
        {
            sut = new Payment(1, "Jake Ryu", "1111222233334444", "10/23", "987", 99);
        }
        
        [Fact]
        public void Id_ShouldBeAutoCreated()
        {
            sut.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void CardNumber_WhenRetrieved_ShouldBeMasked()
        {
            sut.CardNumber.Should().NotBe("1111222233334444");
        }
    }
}