using System.Collections.Generic;

namespace Application.Features.Payments.GetPaymentsList
{
    public class PaymentsListVm
    {
        public IList<PaymentDetailDto> Payments { get; set; }
    }
}