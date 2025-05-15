using System.Text;

namespace bng
{
    internal static class NumberConverter
    {   
        internal static string ConvertToWords(string numberStr)
         {
            if (string.IsNullOrEmpty(numberStr))
            {
                throw new FormatException(nameof(numberStr));
            }

            if (numberStr.Count(c=> c == '.') > 1)
            {
                throw new FormatException(nameof(numberStr));
            }
            // Handle negative numbers
            if (numberStr.StartsWith("-"))
                return "ঋণাত্মক " + ConvertToWords(numberStr[1..]);

            var splitOnDecimal = numberStr.Split('.');
            numberStr = splitOnDecimal[0];
            var decBuilder = new StringBuilder();
            if (splitOnDecimal.Length > 1)
            {
                var dec = splitOnDecimal[1];
                
                decBuilder.Append(" দশমিক");
                Console.WriteLine(dec);
                for (int i = 0; i < dec.Length; i++)
                {
                    var index = dec[i] - 48;
                    decBuilder.Append(" ");
                    decBuilder.Append(SharedConstant.BengaliDigits[index]);
                }
            }

            // Clean the number string (remove commas and other non-numeric chars)
            // numberStr = new string(numberStr.Where(c => char.IsDigit(c)).ToArray());

            // Handle zero
            if (numberStr == "0")
                return SharedConstant.BengaliDigits[0];

            
            
            // Group the digits into 7-digit segments (crore-lakh-thousand-hundred pattern)
            var segments = new List<string>();
            int length = numberStr.Length;

            // Handle the leftmost segment (could be 1 or 2 digits for crores)
            int firstSegmentLength = length % 7;
            if (firstSegmentLength > 0)
            {
                segments.Add(numberStr.Substring(0, firstSegmentLength));
            }

            // Handle remaining 7-digit segments
            for (int i = firstSegmentLength; i < length; i += 7)
            {
                int segmentLength = Math.Min(7, length - i);
                segments.Add(numberStr.Substring(i, segmentLength));
            }

            StringBuilder result = new StringBuilder();

            // Process each segment
            for (int i = 0; i < segments.Count; i++)
            {
                string segmentValue = ConvertSegment(segments[i]);

                if (!string.IsNullOrEmpty(segmentValue))
                {
                    if (result.Length > 0)
                        result.Append(" ");

                    // Add crore for all segments except the last one
                    if (i < segments.Count - 1)
                    {
                        result.Append(segmentValue + " " + NumberConstants.Crore);
                    }
                    else
                    {
                        result.Append(segmentValue);
                    }
                }
            }
            result.Append(decBuilder);
            return result.ToString();
        }

        private static string ConvertSegment(string segment)
        {
            long value = long.Parse(segment);

            if (value == 0)
                return "";

            StringBuilder result = new StringBuilder();

            // Handle crores
            if (value >= 10000000)
            {
                long crores = value / 10000000;
                result.Append(ConvertSmallNumber(crores) + " " + NumberConstants.Crore);
                value %= 10000000;

                if (value > 0)
                    result.Append(" ");
            }

            // Handle lakhs
            if (value >= 100000)
            {
                long lakhs = value / 100000;
                result.Append(ConvertSmallNumber(lakhs) + " " + NumberConstants.Lakh);
                value %= 100000;

                if (value > 0)
                    result.Append(" ");
            }

            // Handle thousands
            if (value >= 1000)
            {
                long thousands = value / 1000;
                result.Append(ConvertSmallNumber(thousands) + " " + NumberConstants.Thousand);
                value %= 1000;

                if (value > 0)
                    result.Append(" ");
            }

            // Handle hundreds
            if (value >= 100)
            {
                long hundreds = value / 100;
                result.Append(SharedConstant.BengaliDigits[(int)hundreds] + " " + NumberConstants.Hundred);
                value %= 100;

                if (value > 0)
                    result.Append(" ");
            }

            // Handle tens and units
            if (value > 0)
            {
                result.Append(ConvertSmallNumber(value));
            }

            return result.ToString();
        }

        private static string ConvertSmallNumber(long number)
        {
            if (number < 10)
                return SharedConstant.BengaliDigits[(int)number];
            else if (number < 20)
                return SharedConstant.BengaliTeens[(int)number - 10];
            else if (number < 30)
                return SharedConstant.BengaliTwenties[(int)number - 20];
            else if (number < 40)
                return SharedConstant.BengaliThirties[(int)number - 30];
            else if (number < 50)
                return SharedConstant.BengaliFourties[(int)number - 40];
            else if (number < 60)
                return SharedConstant.BengaliFifties[(int)number - 50];
            else if (number < 70)
                return SharedConstant.BengaliSixties[(int)number - 60];
            else if (number < 80)
                return SharedConstant.BengaliSeventies[(int)number - 70];
            else if (number < 90)
                return SharedConstant.BengaliEighties[(int)number - 80];
            else if (number < 100)
                return SharedConstant.BengaliNineties[(int)number - 90];

            return ""; // Should never reach here
        }

        internal static string ConvertToMillBill(string bengaliNumberText)
        {
            // Constants for conversion
            const decimal CRORE = 10000000M; // 10 million
            const decimal LAKH = 100000M;    // 0.1 million
            const decimal THOUSAND = 1000M;   // 0.001 million

            decimal totalValue = 0;
            string[] parts = bengaliNumberText.Split(' ');
            decimal currentValue = 0;

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];

                switch (part)
                {
                    case "কোটি":
                        totalValue += currentValue * CRORE;
                        currentValue = 0;
                        break;
                    case "লক্ষ":
                        totalValue += currentValue * LAKH;
                        currentValue = 0;
                        break;
                    case "হাজার":
                        totalValue += currentValue * THOUSAND;
                        currentValue = 0;
                        break;
                    case "শত":
                        currentValue *= 100;
                        break;
                    default:
                        // Convert Bengali number word to numeric value
                        decimal number = GetNumericValue(part);
                        if (number >= 0)
                            currentValue = number;
                        break;
                }
            }

            totalValue += currentValue; // Add any remaining value

            // Convert to millions format
            if (totalValue >= 1000000000) // Billion
            {
                decimal billions = Math.Floor(totalValue / 1000000000);
                decimal millions = (totalValue % 1000000000) / 1000000;
                return $"{billions:##.##} billion {millions:##.##} million";
            }
            else if (totalValue >= 1000000) // Million
            {
                decimal millions = totalValue / 1000000;
                return $"{millions:##.##} million";
            }
            else // Less than a million
            {
                return $"{totalValue:##.##}";
            }
        }

        private static decimal GetNumericValue(string bengaliWord)
        {
            // Match Bengali word to numeric value
            if (Array.IndexOf(SharedConstant.BengaliDigits, bengaliWord) is var digitIndex and >= 0)
                return digitIndex;
            if (Array.IndexOf(SharedConstant.BengaliTeens, bengaliWord) is var teenIndex and >= 0)
                return teenIndex + 10;
            if (Array.IndexOf(SharedConstant.BengaliTwenties, bengaliWord) is var twentyIndex and >= 0)
                return twentyIndex + 20;

            return -1; // Invalid word
        }

        internal static string ConvertToBanglaNumerics(string text)
        {
            return ToBanglaNum(text, SharedConstant.EnglishNum, SharedConstant.BengaliNum);
        }

        private static string ToBanglaNum(string text, string [] sourceDigits, string[] targetDigits)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new FormatException(nameof(text));
            }

            if (text.Count(c => c == '.') > 1)
            {
                throw new FormatException(nameof(text));
            }

            var result = new StringBuilder(text.Length);

            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    if (c == '.')
                    {
                        result.Append(c);
                    }
                    else
                    {
                        throw new FormatException(nameof(text));
                    }
                }
                else
                {
                    string charStr = c.ToString();
                    int index = Array.IndexOf(sourceDigits, charStr);

                    if (index >= 0)
                        result.Append(targetDigits[index]);
                    else
                        result.Append(c);
                }
                
            }

            return result.ToString();
        }
    }
}