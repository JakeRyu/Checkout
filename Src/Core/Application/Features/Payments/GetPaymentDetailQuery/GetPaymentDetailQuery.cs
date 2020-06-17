using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Payments.GetPaymentDetailQuery
{
    public class GetPaymentDetailQuery : IRequest<PaymentDetailDto>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetPaymentDetailQuery, PaymentDetailDto>
        {
            private readonly ICheckoutDbContext context;
            private readonly IMapper mapper;

            public Handler(ICheckoutDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<PaymentDetailDto> Handle(GetPaymentDetailQuery request,
                CancellationToken cancellationToken)
            {
                var payment = await context.Payments
                    .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

                if (payment == null)
                {
                    throw new EntityNotFoundException(nameof(Payment), request.Id);
                }

                return mapper.Map<PaymentDetailDto>(payment);
            }
        }
    }
}