using System;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;
using Application.Common.Exceptions;
using Application.Features.Payments.GetPaymentDetailQuery;
using Application.UnitTests.Common;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Persistence;
using Xunit;

namespace Application.UnitTests.Payments
{
    [Collection("QueryCollection")]
    public class GetPaymentDetailQueryTests
    {
        private readonly CheckoutDbContext context;
        private readonly IMapper mapper;

        public GetPaymentDetailQueryTests(QueryTestFixture fixture)
        {
            context = fixture.Context;
            mapper = fixture.Mapper;
        }
        
        [Fact]
        public async Task Handler_GivenNotExistingId_ShouldThrowException()
        {
            var payment = new Payment(1, "cardholder1", "1111-2222-3333-4444", "12/22", "111", 1000);
            
            await context.Payments.AddAsync(payment);
            await context.CommitAsync(CancellationToken.None);

            var randomId = new Guid("0f7d4d5e-74d2-49b3-823e-f41dace79557");
            var query = new GetPaymentDetailQuery
            {
                Id = randomId
            };
            var sut = new GetPaymentDetailQuery.Handler(context, mapper);

            await sut.Invoking(async h => await h.Handle(query, CancellationToken.None))
                .Should().ThrowAsync<EntityNotFoundException>()
                .WithMessage($"Entity \"Payment\" ({randomId}) was not found.");
        }

        [Fact]
        public async Task Handler_GivenExistingId_ShouldReturnAPaymentRecord()
        {
            var payment = new Payment(1, "cardholder1", "1111-2222-3333-4444", "12/22", "111", 1000);
            
            await context.Payments.AddAsync(payment);
            await context.CommitAsync(CancellationToken.None);
            
            var query = new GetPaymentDetailQuery
            {
                Id = payment.Id
            };
            var sut = new GetPaymentDetailQuery.Handler(context, mapper);

            var result = await sut.Handle(query, CancellationToken.None);
            result.Should().BeOfType<PaymentDetailDto>();
            result.CardHolderName.Should().Be("cardholder1");
        }
    }
}