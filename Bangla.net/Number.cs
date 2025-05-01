
namespace bng
{
    public class Number
    {
        public static string ToBanglaWord(string numberStr)
        {
            return BengaliNumberConverter.ConvertToWords(numberStr);
        }

        public static string ToBanglaNumber(string numberString)
        {
            return BengaliCalendarConverters.ConvertToBengaliNumerals(numberString);
        }

        public static string ToBanglaWord(double number)
        {
            return ToBanglaWord(number.ToString("F0"));
        }
        public static string ToBanglaNumber(double number)
        {
            return ToBanglaNumber(number.ToString("F0"));
        }

        public static string ConvertToMillion(string numTextString)
        {
            return BengaliNumberConverter.ConvertToMillBill(numTextString);
        }
    }
}

