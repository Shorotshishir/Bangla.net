namespace bng
{
    public class BengaliDateInfo
    {
        /// <summary>Gets the day of the month in Bengali numerals</summary>
        public required string Date { get; set; }

        /// <summary>Gets the month name</summary>
        public required string Month { get; set; }

        /// <summary>Gets the year in Bengali numerals</summary>
        public required string Year { get; set; }

        /// <summary>Gets the current season name</summary>
        public required string Season { get; set; }

        /// <summary>Gets the weekday name</summary>
        public required string Weekday { get; set; }

        /// <summary>Gets the ordinal representation of the day (optional)</summary>
        public string? Ordinal { get; set; }

        public override string ToString()
        {
            return $"{Date} ({Ordinal}) {Month}, {Year} বঙ্গাব্দ ; {Weekday}, {Season} কাল ।";
        }
    }
}

