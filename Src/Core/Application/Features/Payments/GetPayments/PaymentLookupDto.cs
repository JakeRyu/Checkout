using System;
using Domain.ValueObjects;

namespace Application.Features.Payments.GetPayments
{
    public class PaymentLookupDto
    {
        public Guid Id { get; set; }
        public string CardHolderName { get; set; }
        public CardNumber CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}