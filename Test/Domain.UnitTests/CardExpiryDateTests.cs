using System;
using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests
{
    /// <summary>
    /// Date is formatted in UK culture
    /// </summary>
    public class CardExpiryDateTests
    {
        [Theory]
        [InlineData("12/20", "31/12/2020")]
        [InlineData("11/20", "30/11/2020")]
        [InlineData("02/20", "29/02/2020")]
        public void CardExpiryDate_GivenExpiryYearMonthString_ShouldCreateExpiryDate(string expiryYearMonthString, string expiryDate)
        {
            var sut = CardExpiryDate.For(expiryYearMonthString);

            // Date property should be the end of month
            sut.Value.ToString("dd/MM/yyyy").Should().Be(expiryDate);
            
            // ToString is overriden to return original input
            sut.ToString().Should().Be(expiryYearMonthString);
        }

        [Theory]
        [InlineData("13/20")]
        [InlineData("1l/20")]
        [InlineData("10/2O")]
        public void CardExpiryDate_GivenWrongInput_ShouldRaiseCardExpiryDateInvalidException(string cardExpiryYearMonthString)
        {
            Action act = () => CardExpiryDate.For(cardExpiryYearMonthString);
            act.Should().Throw<CardExpiryDateInvalidException>();
        }

        [Fact]
        public void CardExpiryDate_ShouldConvertToStringImplicitly()
        {
            var expiryYearMonthString = "01/20";
            var sut = CardExpiryDate.For(expiryYearMonthString);

            string converted = sut;
            
            sut.Value.ToString("dd/MM/yyyy").Should().Be("31/01/2020");
            converted.Should().Be(expiryYearMonthString);
        }

        [Fact]
        public void CardExpiryDate_GivenSameValue_ShouldBeEqual()
        {
            var expiryYearMonthString = "01/20";
            var sut1 = CardExpiryDate.For(expiryYearMonthString);   
            var sut2 = CardExpiryDate.For(expiryYearMonthString);  
            
            sut1.Should().Be(sut2);
            sut1.Equals(sut2).Should().BeTrue();
            (sut1 == sut2).Should().BeTrue();
        }

        [Fact]
        public void CardExpiryDate_GivenDifferentValue_ShouldNotEqualOnOperator()
        {
            var sut1 = CardExpiryDate.For("01/20");   
            var sut2 = CardExpiryDate.For("02/22");

            sut1.Should().NotBe(sut2);
            sut1.Equals(sut2).Should().BeFalse();
            (sut1 != sut2).Should().BeTrue();
        }
    }
}