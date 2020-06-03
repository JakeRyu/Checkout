using System.Collections.Generic;

namespace Application.Features.Payments.GetPayments
{
    public class PaymentsListVm
    {
        public IList<PaymentLookupDto> Payments { get; set; }
    }
}