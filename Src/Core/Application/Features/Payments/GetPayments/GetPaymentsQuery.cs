using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Payments.GetPayments
{
    public class GetPaymentsQuery : IRequest<PaymentsListVm>
    {
        public int MerchantId { get; set; }
        public string CardNumber { get; set; }

        public class Handler : IRequestHandler<GetPaymentsQuery, PaymentsListVm>
        {
            private readonly ICheckoutDbContext context;
            private readonly IMapper mapper;

            public Handler(ICheckoutDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            
            public Task<PaymentsListVm> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}