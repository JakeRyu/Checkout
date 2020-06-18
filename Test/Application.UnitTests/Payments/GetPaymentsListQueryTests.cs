using System.Threading;
using System.Threading.Tasks;
using Application.Features.Payments.GetPaymentsList;
using Application.UnitTests.Common;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Persistence;
using Xunit;

namespace Application.UnitTests.Payments
{
    [Collection("QueryCollection")]
    public class GetPaymentsListQueryTests
    {
        private readonly CheckoutDbContext context;
        private readonly IMapper mapper;
        
        public GetPaymentsListQueryTests(QueryTestFixture fixture)
        {
            context = fixture.Context;
            mapper = fixture.Mapper;
        }
        
        [Fact]
        public async Task Handler_GivenValidCondition_ShouldFindPayments()
        {
            // Save payments
            var payments = new[]
            {
                new Payment(1, "cardholder1", "1111-2222-3333-4444", "12/22", "111", 1000),
                new Payment(1, "cardholder1", "1111-2222-3333-4444", "12/22", "111", 2000),
                new Payment(1, "cardholder1", "1111-2222-3333-4444", "12/22", "111", 3000)
            };

            await context.Payments.AddRangeAsync(payments);
            await context.CommitAsync(CancellationToken.None);
            
            var query = new GetPaymentsListQuery
            {
                MerchantId = 1,
                CardNumber = "1111-2222-3333-4444"
            };
            var sut = new GetPaymentsListQuery.Handler(context, mapper);
            var result = await sut.Handle(query, CancellationToken.None);

            result.Should().BeOfType<PaymentsListVm>();
            result.Payments.Count.Should().Be(3);
        }
    }
}