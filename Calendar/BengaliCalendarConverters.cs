namespace bng
{
    internal class BengaliCalendarConverters
    {

        /// <summary>
        /// Converts English numerals to Bengali numerals
        /// </summary>
        /// <param name="text">The text containing English numerals</param>
        /// <returns>Text with English numerals converted to Bengali numerals</returns>
        public static string ConvertToBengaliNumerals(string text)
        {
            return ConvertDigits(text, BengaliCalendarConstants.EnglishDigits, BengaliCalendarConstants.BanglaDigits);
        }

        /// <summary>
        /// Converts Bengali numerals to English numerals
        /// </summary>
        /// <param name="text">The text containing Bengali numerals</param>
        /// <returns>Text with Bengali numerals converted to English numerals</returns>
        public static string ConvertToEnglishNumerals(string text)
        {
            return ConvertDigits(text, BengaliCalendarConstants.BanglaDigits, BengaliCalendarConstants.EnglishDigits);
        }

        /// <summary>
        /// Converts digits from one numeric system to another
        /// </summary>
        private static string ConvertDigits(string text, string[] sourceDigits, string[] targetDigits)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var result = new System.Text.StringBuilder(text.Length);

            foreach (char c in text)
            {
                string charStr = c.ToString();
                int index = Array.IndexOf(sourceDigits, charStr);

                if (index >= 0)
                    result.Append(targetDigits[index]);
                else
                    result.Append(c);
            }

            return result.ToString();
        }

        public static string ConvertToOrdinal(string text)
        {
            if (int.TryParse(ConvertToEnglishNumerals(text), out var numericDay) &&
                numericDay > 0 && numericDay <= BengaliCalendarConstants.BengaliOrdinals.Length)
            {
                return BengaliCalendarConstants.BengaliOrdinals[numericDay - 1];
            }
            return string.Empty;
        }
    }
}
