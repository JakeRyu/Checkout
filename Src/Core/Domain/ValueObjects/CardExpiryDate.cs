using System;
using System.Collections.Generic;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class CardExpiryDate : ValueObject
    {
        public DateTime Value { get; private set; }
        

        /// <summary>
        /// Produce a card expiry date based on month and year
        /// </summary>
        /// <param name="expiryMonthYearString">mm/yy format</param>
        /// <returns></returns>
        /// <exception cref="CardExpiryDateInvalidException"></exception>
        public static CardExpiryDate For(string expiryMonthYearString)
        {
            var cardExpiryDate = new CardExpiryDate();
            
            try
            {
                var index = expiryMonthYearString.IndexOf("/", StringComparison.Ordinal);
                var monthString = expiryMonthYearString.Substring(0, index);
                var twoDigitYearString = expiryMonthYearString.Substring(index + 1);

                var month = int.Parse(monthString);
                var twoDigitYear = int.Parse(twoDigitYearString);
                var fourDigitYear = twoDigitYear >= 50 ? 1900 + twoDigitYear : 2000 + twoDigitYear;

                var firstDayOfMonth = new DateTime(fourDigitYear, month, 1);
                cardExpiryDate.Value = firstDayOfMonth.AddMonths(1).AddDays(-1);
            }
            catch(Exception ex)
            {
                throw new CardExpiryDateInvalidException(expiryMonthYearString, ex);
            }
            
            return cardExpiryDate;
        }

        public static implicit operator string(CardExpiryDate cardExpiryDate)
        {
            return cardExpiryDate.ToString();
        }

        public override string ToString()
        {
            var year = Value.Year.ToString().Substring(2);
            var month = Value.Month >= 10 ? Value.Month.ToString() : "0" + Value.Month;

            return $"{month}/{year}";
        }

        public static bool operator ==(CardExpiryDate date1, CardExpiryDate date2)
        {
            return EqualOperator(date1, date2);
        }
        
        public static bool operator !=(CardExpiryDate date1, CardExpiryDate date2)
        {
            return NotEqualOperator(date1, date2);
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}