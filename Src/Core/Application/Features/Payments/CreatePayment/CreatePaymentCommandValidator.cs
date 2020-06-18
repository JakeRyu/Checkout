using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Payments.CreatePayment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.MerchantId).NotEqual(0);
            RuleFor(x => x.CardHolderName).NotEmpty().MaximumLength(60);
            RuleFor(x => x.CardNumber).NotEmpty().CreditCard();
            RuleFor(x => x.CardExpiryDate).NotEmpty().Must(str =>
            {
                if (string.IsNullOrEmpty(str)) return false;
                
                var regex = new Regex(@"\b[0-1][0-9]/[0-9]{2}\b");
                if (!regex.IsMatch(str)) return false;
                
                var month = int.Parse(str.Substring(0, 2));
                return month >= 1 && month <= 12;
            }).WithMessage("Card expiration date should be mm/yy format");
            RuleFor(x => x.Cvv).NotEmpty().Matches(@"\b[0-9]{3}\b");;
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}