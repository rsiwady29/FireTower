using System.Collections.Generic;

namespace FireTower.Presentation.Responses
{
    public class PaymentListResponse
    {
        public List<OneTimePaymentResponse> OneTimePayments { get; set; }

        public List<RecurringPaymentResponse> RecurringPayments { get; set; }
    }
}