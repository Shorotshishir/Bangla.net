namespace bng
{
    public static class Calendar
    {
        private static bool IsLeapYear(int year) => DateTime.IsLeapYear(year);

        private static bool IsLeapYearMonth(int month, int year) => month == CalendarConstants.LeapYearMonthIndex && IsLeapYear(year);

        private static int GetBengaliYear(int day, int month, int year)
        {
            if (month > 3 || month == 3 && day > 13)
                return year - 593;

            return year - 594;
        }

        private static string GetBengaliWeekday(DateTime date)
        {
            // Convert DayOfWeek (Sunday = 0) to Monday-based index
            int dayIndex = ((int)date.DayOfWeek + 6) % 7;
            return CalendarConstants.BengaliWeekdays[dayIndex];
        }


        private static BengaliDateInfo CalculateBengaliDate(DateTime gregorianDate)
        {
            var (day, month, year) = (gregorianDate.Day, gregorianDate.Month - 1, gregorianDate.Year);
            var (bengaliDay, bengaliMonth) = CalculateBengaliDayMonth(day, month, year);

            return new BengaliDateInfo
            {
                Date = CalendarConverters.ConvertToBengaliNumerals(bengaliDay.ToString()),
                Month = CalendarConstants.BengaliMonths[bengaliMonth],
                Year = CalendarConverters.ConvertToBengaliNumerals(GetBengaliYear(day, month + 1, year).ToString()),
                Season = CalendarConstants.BengaliSeasons[bengaliMonth / 2],
                Weekday = GetBengaliWeekday(gregorianDate),
                Ordinal = CalendarConverters.ConvertToOrdinal(CalendarConverters.ConvertToBengaliNumerals(bengaliDay.ToString()))
            };
        }

        private static (int day, int month) CalculateBengaliDayMonth(int gregorianDay, int month, int year)
        {
            var lastDay = CalendarConstants.GregLastDayOfBengaliMonths[month];
            var daysInMonth = CalendarConstants.DaysInBengaliMonths[month] + (IsLeapYearMonth(month, year) ? 1 : 0);

            if (gregorianDay <= lastDay)
            {
                return (daysInMonth + gregorianDay - lastDay, month);
            }

            return (gregorianDay - lastDay, (month + 1) % 12);
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
            if (year < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(year), "Year must be greater than 0");
            }
            if (month is < 1 or > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12");
            }
            if (day is < 1 or > 32)
            {
                throw new ArgumentOutOfRangeException(nameof(day), "Day must be greater than 0");
            }

            DateTime date;
            try
            {
                date = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentException($"Invalid date combination: {day}/{month}/{year}. {ex.Message}", ex);
            }
            return GetBengaliDate(date);
        }
    }
}

