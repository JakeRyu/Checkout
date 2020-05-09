using System;
using System.Text;

namespace Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public int MerchantId { get; private set; }
        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiryDate { get; private set; }
        public string Cvv { get; private set; }
        public decimal Amount { get; private set; }

        public Payment(int merchantId, string cardHolderName, string cardNumber, string cardExpiryDate, string cvv, decimal amount)
        {
            Id = Guid.NewGuid();
            MerchantId = merchantId;
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            CardExpiryDate = cardExpiryDate;
            Cvv = cvv;
            Amount = amount;
        }
    }
}