using System;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Payments.GetPaymentsList
{
    public class PaymentDetailDto : DtoBase<Payment>
    {
        public Guid Id { get; set; }
        public string CardHolderName { get; set; }
        public string MaskedCardNumber { get; set; }
        public decimal Amount { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<Payment, PaymentDetailDto>()
                .ForMember(d => d.MaskedCardNumber, opt => 
                    opt.MapFrom(src => src.CardNumber.MaskedValue));
        }
    }
}