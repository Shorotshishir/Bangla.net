
namespace bng
{
    public class Number
    {
        public static string ToBanglaWord(string numberStr)
        {
            return NumberConverter.ConvertToWords(numberStr);
        }

        public static string ToBanglaNumber(string numberString)
        {
            return NumberConverter.ConvertToBanglaNumerics(numberString);
        }

        public static string ToBanglaWord(double number)
        {
            return ToBanglaWord(number.ToString());
        }
        public static string ToBanglaNumber(double number)
        {
            return ToBanglaNumber(number.ToString());
        }
    }
}

