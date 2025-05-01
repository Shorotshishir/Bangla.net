using bng;

namespace TestBangla.net
{
    public class CalendarTests
    {
        [Theory]
        [InlineData(29, 4, 2025, "১৬ (ষোলোই) বৈশাখ, ১৪৩২ বঙ্গাব্দ ; মঙ্গলবার, গ্রীষ্ম কাল ।")]
        [InlineData(13, 3, 2025, "২৯ (ঊনত্রিশে) ফাল্গুন, ১৪৩১ বঙ্গাব্দ ; বৃহস্পতিবার, বসন্ত কাল ।")]
        public void GetBengaliDate_WithValidNumericInput_ReturnsCorrectDate(int day, int month, int year, string expected)
        {
            var result = Calendar.GetBengaliDate(day, month, year).ToString();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(29, 4, 2025, "১৬ (ষোলোই) বৈশাখ, ১৪৩২ বঙ্গাব্দ ; মঙ্গলবার, গ্রীষ্ম কাল ।")]
        [InlineData(13, 3, 2025, "২৯ (ঊনত্রিশে) ফাল্গুন, ১৪৩১ বঙ্গাব্দ ; বৃহস্পতিবার, বসন্ত কাল ।")]
        public void GetBengaliDate_WithValidDateTimeInput_ReturnsCorrectDate(int day, int month, int year, string expected)
        {
            var date = new DateTime(year, month, day);
            var result = Calendar.GetBengaliDate(date).ToString();
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(0, 1, 2000, "Day must be greater than 0")]
        [InlineData(32, 1, 2000, "Invalid date")]
        [InlineData(1, 0, 2000, "Month must be between 1 and 12")]
        [InlineData(1, 13, 2000, "Month must be between 1 and 12")]
        [InlineData(1, 1, 0, "Year must be greater than 0")]
        [InlineData(31, 2, 2023, "Invalid date")] // February 31st
        [InlineData(31, 4, 2023, "Invalid date")] // April 31st
        public void GetBengaliDate_WithInvalidInput_ThrowsException(int day, int month, int year, string expectedMessage)
        {
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                Calendar.GetBengaliDate(day, month, year));
            Assert.Contains(expectedMessage, exception.Message);
        }

        [Fact]
        public void GetBengaliDate_WithCurrentDate_ReturnsValidResult()
        {
            var result = Calendar.GetBengaliDate();
            Assert.NotNull(result);
            Assert.NotEmpty(result.ToString());
        }

        [Fact]
        public void GetBengaliDate_WithDateTime_ReturnsValidResult()
        {
            var result = new DateTime(2025, 01, 5);
            Assert.NotEmpty(result.ToString());
        }

        [Fact]
        public void GetBengaliDate_WithLeapYear_HandlesFebruaryCorrectly()
        {
            // February 29th in a leap year should be valid
            var result = Calendar.GetBengaliDate(29, 2, 2024);
            Assert.NotNull(result);

            // February 29th in a non-leap year should throw
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
                Calendar.GetBengaliDate(29, 2, 2023));
            Assert.Contains("Invalid date", exception.Message);
        }
    }
}