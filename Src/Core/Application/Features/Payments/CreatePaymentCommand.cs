using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Payments
{
    public class CreatePaymentCommand : IRequest
    {
        public int MerchantId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiryDate { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }
        
        public class Handler : IRequestHandler<CreatePaymentCommand>
        {
            private readonly ICheckoutDbContext context;

            public Handler(ICheckoutDbContext context)
            {
                this.context = context;
            }
            
            public async Task<Unit> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
            {
                var payment = new Payment(request.MerchantId, request.CardHolderName, request.CardNumber,
                    request.CardExpiryDate, request.Cvv, request.Amount);

                await context.Payments.AddAsync(payment, cancellationToken);

                await context.CommitAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}