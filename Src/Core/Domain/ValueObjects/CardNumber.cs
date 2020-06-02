using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.ValueObjects
{
#pragma warning disable 660,661
    public class CardNumber : ValueObject
#pragma warning restore 660,661
    {
        public string OriginalValue { get; private set; }
        
        public string MaskedValue
        {
            get
            {
                var rnd = new Random();
                var resultBuilder = new StringBuilder();
                var stringLength = OriginalValue.Length;

                var randomIndices = Enumerable.Range(0, stringLength)
                    .OrderBy(x => rnd.Next())
                    .Take(stringLength / 4)
                    .ToList();

                for (var i = 0; i < stringLength; i++)
                {
                    resultBuilder.Append(randomIndices.Contains(i) ? '*' : OriginalValue[i]);
                }

                const int chunkSize = 4;
                var masked = resultBuilder.ToString();
                
                var formatted = string.Join('-',
                    Enumerable.Range(0, masked.Length / chunkSize)
                        .Select(i => masked.Substring(i * chunkSize, chunkSize)));

                return formatted;
            }
        }
        
        public CardNumber(string originalValue)
        {
            OriginalValue = originalValue;
        }

        public override string ToString()
        {
            return MaskedValue;
        }

        public static bool operator ==(CardNumber cardNumber1, CardNumber cardNumber2)
        {
            return EqualOperator(cardNumber1, cardNumber2);
        }
        
        public static bool operator !=(CardNumber cardNumber1, CardNumber cardNumber2)
        {
            return NotEqualOperator(cardNumber1, cardNumber2);
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return OriginalValue;
        }
    }
}