using System;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public int MerchantId { get; private set; }
        public string CardHolderName { get; private set; }
        public CardNumber CardNumber { get; private set; }
        public CardExpiryDate CardExpiryDate { get; private set; }
        public string Cvv { get; private set; }
        public decimal Amount { get; private set; }

        private Payment()
        {
        }
        
        public Payment(int merchantId, string cardHolderName, string cardNumber, string cardExpiryDate, string cvv, decimal amount)
        {
            Id = Guid.NewGuid();
            MerchantId = merchantId;
            CardHolderName = cardHolderName;
            CardNumber = new CardNumber(cardNumber);
            CardExpiryDate = CardExpiryDate.For(cardExpiryDate);
            Cvv = cvv;
            Amount = amount;
        }
    }
}