using Application.Features.Payments.GetPayments;
using Application.UnitTests.Common;
using AutoMapper;
using Persistence;
using Xunit;

namespace Application.UnitTests.Payments
{
    [Collection("QueryCollection")]
    public class QueryPaymentsTests
    {
        private readonly CheckoutDbContext context;
        private readonly IMapper mapper;
        
        public QueryPaymentsTests(QueryTestFixture fixture)
        {
            context = fixture.Context;
            mapper = fixture.Mapper;
        }
        
        [Fact]
        public void QueryPayments_GivenValidCondition_ShouldFindPayments()
        {
            // Save a payment
            
            var query = new GetPaymentsQuery()
            {
                MerchantId = 1,
                CardNumber = "1111-2222-3333-4444"
            };
            var sut = new GetPaymentsQuery.Handler(context, mapper);
        }
    }
}