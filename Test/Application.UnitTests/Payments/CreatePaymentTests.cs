using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Payments;
using Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.UnitTests.Payments
{
    public class CreatePaymentTests : CommandTestBase
    {
        [Fact]
        public async Task CreatePayment_GivenValidInput_ShouldSaveAPayment()
        {
            var command = new CreatePaymentCommand
            {
                MerchantId = 1,
                CardHolderName = "Jake Ryu",
                CardNumber = "1111-2222-3333-4444",
                CardExpiryDate = "05/24",
                Cvv = "978",
                Amount = 150
            };

            var sut = new CreatePaymentCommand.Handler(Context);
            await sut.Handle(command, CancellationToken.None);

            var payment = await Context.Payments.FirstAsync();
            payment.MerchantId.Should().Be(1);
            payment.CardHolderName.Should().Be("Jake Ryu");
            payment.CardNumber.OriginalValue.Should().Be("1111-2222-3333-4444");
            payment.CardExpiryDate.Value.Should().Be(new DateTime(2024, 5, 31));
            payment.Cvv.Should().Be("978");
            payment.Amount.Should().Be(150);
        }
    }
}