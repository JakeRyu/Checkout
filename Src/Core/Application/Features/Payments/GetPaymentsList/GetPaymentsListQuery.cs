using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Payments.GetPaymentDetailQuery;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Payments.GetPaymentsList
{
    public class GetPaymentsListQuery : IRequest<PaymentsListVm>
    {
        public int MerchantId { get; set; }
        public string CardNumber { get; set; }

        public class Handler : IRequestHandler<GetPaymentsListQuery, PaymentsListVm>
        {
            private readonly ICheckoutDbContext context;
            private readonly IMapper mapper;

            public Handler(ICheckoutDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            
            public async Task<PaymentsListVm> Handle(GetPaymentsListQuery request, CancellationToken cancellationToken)
            {
                var payments = await context.Payments
                    .AsNoTracking()
                    .Where(p => p.MerchantId == request.MerchantId &&
                        p.CardNumber.OriginalValue == request.CardNumber)
                    .ProjectTo<PaymentDetailDto>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                
                return new PaymentsListVm
                {
                    Payments = payments
                };
            }
        }
    }
}