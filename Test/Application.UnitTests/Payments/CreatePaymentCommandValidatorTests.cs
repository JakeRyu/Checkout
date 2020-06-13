using Application.Features.Payments.CreatePayment;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.UnitTests.Payments
{
    public class CreatePaymentCommandValidatorTests
    {
        private readonly CreatePaymentCommandValidator validator = new CreatePaymentCommandValidator();

        [Fact]
        public void MerchantId_ShouldBeNumber()
        {
            validator.ShouldHaveValidationErrorFor(x => x.MerchantId, 0);
            
            validator.ShouldNotHaveValidationErrorFor(x => x.MerchantId, 1);
        }
        
        [Fact]
        public void CardHolderName_ShouldNotBeEmpty()
        {
            validator.ShouldHaveValidationErrorFor(x => x.CardHolderName, "");

            validator.ShouldNotHaveValidationErrorFor(x => x.CardHolderName, "John Smith");
        }
        
        [Fact]
        public void CardHolderName_LengthShouldBeLessThanOrEqualTo60()
        {
            validator.ShouldHaveValidationErrorFor(x => x.CardHolderName, new string('A', 61));
            
            validator.ShouldNotHaveValidationErrorFor(x => x.CardHolderName, new string('A', 60));
        }

        [Fact]
        public void CardNumber_ShouldBeInRightFormats()
        {
            validator.ShouldHaveValidationErrorFor(x => x.CardNumber, "AAAA-BBBB");
            validator.ShouldHaveValidationErrorFor(x => x.CardNumber, "AAAA-BBBB-1111-2222");
            validator.ShouldHaveValidationErrorFor(x => x.CardNumber, "11112222333344");
            validator.ShouldHaveValidationErrorFor(x => x.CardNumber, "1111-2222-3333-44");
            
            validator.ShouldNotHaveValidationErrorFor(x => x.CardNumber, "1111222233334444");
            validator.ShouldNotHaveValidationErrorFor(x => x.CardNumber, "1111-2222-3333-4444");
        }

        [Fact]
        public void CardExpiryDate_ShouldBeInRightFormats()
        {
            validator.ShouldHaveValidationErrorFor(x => x.CardExpiryDate, "lo/lo");
            validator.ShouldHaveValidationErrorFor(x => x.CardExpiryDate, "13/00");
            validator.ShouldHaveValidationErrorFor(x => x.CardExpiryDate, "1220");
            validator.ShouldHaveValidationErrorFor(x => x.CardExpiryDate, "00/00");
            
            validator.ShouldNotHaveValidationErrorFor(x => x.CardExpiryDate, "01/00");
            validator.ShouldNotHaveValidationErrorFor(x => x.CardExpiryDate, "12/99");
            validator.ShouldNotHaveValidationErrorFor(x => x.CardExpiryDate, "05/20");
        }

        [Fact]
        public void Cvv_ShouldBeThreeDigitNumber()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Cvv, "abc");
            validator.ShouldHaveValidationErrorFor(x => x.Cvv, "1bc");
            validator.ShouldHaveValidationErrorFor(x => x.Cvv, "12");
            validator.ShouldHaveValidationErrorFor(x => x.Cvv, "1234");
            validator.ShouldHaveValidationErrorFor(x => x.Cvv, "");

            validator.ShouldNotHaveValidationErrorFor(x => x.Cvv, "123");
        }

        [Fact]
        public void Amount_ShouldBeGreaterThanZero()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Amount, 0);
            validator.ShouldHaveValidationErrorFor(x => x.Amount, -1);
            
            validator.ShouldNotHaveValidationErrorFor(x => x.Amount, 1);
        }
    }
}