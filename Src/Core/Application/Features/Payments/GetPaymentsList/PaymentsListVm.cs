using System.Collections.Generic;
using Application.Features.Payments.GetPaymentDetailQuery;

namespace Application.Features.Payments.GetPaymentsList
{
    public class PaymentsListVm
    {
        public IList<PaymentDetailDto> Payments { get; set; }
    }
}