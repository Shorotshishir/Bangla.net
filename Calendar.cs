namespace bng
{
    public static class Calendar
    {
        private static bool IsLeapYear(int year) => DateTime.IsLeapYear(year);

        private static bool IsLeapYearMonth(int month, int year) => month == BengaliCalendarConstants.LeapYearMonthIndex && IsLeapYear(year);

        private static int GetBengaliYear(int day, int month, int year)
        {
            if (month > 3)
                return year - 593;
            if (month == 3 && day > 13)
                return year - 593;

            return year - 594;
        }

        private static string GetBengaliWeekday(DateTime date)
        {
            // Convert DayOfWeek (Sunday = 0) to Monday-based index
            int dayIndex = ((int)date.DayOfWeek + 6) % 7;
            return BengaliCalendarConstants.BengaliWeekdays[dayIndex];
        }


        private static BengaliDateInfo CalculateBengaliDate(DateTime gregorianDate)
        {
            var (day, month, year) = (gregorianDate.Day, gregorianDate.Month - 1, gregorianDate.Year);
            var (bengaliDay, bengaliMonth) = CalculateBengaliDayMonth(day, month, year);

            return new BengaliDateInfo
            {
                Date = BengaliCalendarConverters.ConvertToBengaliNumerals(bengaliDay.ToString()),
                Month = BengaliCalendarConstants.BengaliMonths[bengaliMonth],
                Year = BengaliCalendarConverters.ConvertToBengaliNumerals(GetBengaliYear(day, month + 1, year).ToString()),
                Season = BengaliCalendarConstants.BengaliSeasons[bengaliMonth / 2],
                Weekday = GetBengaliWeekday(gregorianDate),
                Ordinal = BengaliCalendarConverters.ConvertToOrdinal(BengaliCalendarConverters.ConvertToBengaliNumerals(bengaliDay.ToString()))
            };
        }

        private static (int day, int month) CalculateBengaliDayMonth(int gregorianDay, int month, int year)
        {
            var lastDay = BengaliCalendarConstants.GregLastDayOfBengaliMonths[month];
            var daysInMonth = BengaliCalendarConstants.DaysInBengaliMonths[month] + (IsLeapYearMonth(month, year) ? 1 : 0);

            if (gregorianDay <= lastDay)
            {
                return (daysInMonth + gregorianDay - lastDay, month);
            }
            else
            {
                return (gregorianDay - lastDay, (month + 1) % 12);
            }
        }

        /// <summary>
        /// Gets the Bengali date information for a given Gregorian date
        /// </summary>
        /// <returns>Bengali date information</returns>
        public static BengaliDateInfo GetBengaliDate() =>
            CalculateBengaliDate(DateTime.Today);

        /// <summary>
        /// Gets the Bengali date information for a given Gregorian date
        /// </summary>
        /// <param name="date">The Gregorian date to convert</param>
        /// <returns>Bengali date information</returns>
        public static BengaliDateInfo GetBengaliDate(DateTime date) =>
            CalculateBengaliDate(date);


        /// <summary>
        /// Gets the Bengali date information for a specified Gregorian date
        /// </summary>
        /// <param name="day">The day of the month</param>
        /// <param name="month">The month (1-12)</param>
        /// <param name="year">The year</param>
        /// <returns>Bengali date information</returns>
        public static BengaliDateInfo GetBengaliDate(int day, int month, int year)
        {
            return GetBengaliDate(new DateTime(year, month, day));
        }
    }
}

