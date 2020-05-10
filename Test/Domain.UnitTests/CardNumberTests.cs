using System.Linq;
using Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests
{
    public class CardNumberTests
    {
        private const string CardNumber = "1111222233334444";
        private readonly CardNumber sut;

        public CardNumberTests()
        {
            sut = new CardNumber(CardNumber);
        }

        [Fact]
        public void CardNumber_WhenConvertedToString_ShouldNotBeOriginalValue()
        {
            var converted = $"{sut}";

            converted.Should().NotBe(CardNumber);
        }

        [Fact]
        public void CardNumberMaskedValue_ShouldNotBeOriginalValue()
        {
            sut.MaskedValue.Should().NotBe(CardNumber);
        }

        [Fact]
        public void CardNumberMaskedValue_ShouldMaskFourDigits()
        {
            sut.MaskedValue.Count(chr => chr == '*').Should().Be(4);
        }

        [Fact]
        public void CardNumberMaskedValue_ShouldBeFormatted()
        {
            const int chunkSize = 5;
            var chars = Enumerable.Range(1, 3)
                .Select(i =>
                    sut.MaskedValue.Substring(i * chunkSize - 1, 1));
            
            chars.All(chr => chr == "-").Should().BeTrue();
        }

        [Fact]
        public void CardNumber_TwoInstancesWithTheSameCardNumber_ShouldEqual()
        {
            var secondCardNumber = new CardNumber(CardNumber);

            sut.Equals(secondCardNumber).Should().BeTrue();
        }

        [Fact]
        public void CardNumber_TwoInstancesWithTheSameCardNumber_ShouldEqualOnOperator()
        {
            var secondCardNumber = new CardNumber(CardNumber);

            (sut == secondCardNumber).Should().BeTrue();
        }
        
        [Fact]
        public void CardNumber_TwoInstancesWithDifferentCardNumber_ShouldNotEqualOnOperator()
        {
            var secondCardNumber = new CardNumber("4444333322221111");

            (sut != secondCardNumber).Should().BeTrue();
        }
    }
}