namespace bng
{
    internal static class BengaliCalendarConstants
    {

        internal const int LeapYearMonthIndex = 2; // Falgun
        internal static readonly string[] BanglaDigits = { "০", "১", "২", "৩", "৪", "৫", "৬", "৭", "৮", "৯" };
        internal static readonly string[] EnglishDigits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        internal static readonly string[] BengaliMonths = {
            "পৌষ", "মাঘ", "ফাল্গুন", "চৈত্র", "বৈশাখ", "জ্যৈষ্ঠ",
            "আষাঢ়", "শ্রাবণ", "ভাদ্র", "আশ্বিন", "কার্তিক", "অগ্রহায়ণ"
        };

        internal static readonly string[] BengaliWeekdays = {
            "সোমবার", "মঙ্গলবার", "বুধবার", "বৃহস্পতিবার",
            "শুক্রবার", "শনিবার", "রবিবার"
        };

        internal static readonly string[] BengaliSeasons = {
            "শীত", "বসন্ত", "গ্রীষ্ম", "বর্ষা", "শরৎ", "হেমন্ত"
        };

        internal static readonly string[] BengaliOrdinals = {
            "পহেলা", "দোসরা", "তেসরা", "চৌঠা", "পাঁচই", "ছয়ই", "সাতই", "আটই", "নয়ই", "দশই",
            "এগারোই", "বারোই", "তেরোই", "চৌদ্দই", "পনেরোই", "ষোলোই", "সতেরোই", "আঠারোই", "ঊনিশে", "বিশে",
            "একুশে", "বাইশে", "তেইশে", "চব্বিশে", "পঁচিশে", "ছাব্বিশে", "সাতাশে", "আঠাশে", "ঊনত্রিশে", "ত্রিশে",
            "একত্রিশে"
        };

        // Gregorian date equivalents to last day of Bengali months
        internal static readonly int[] GregLastDayOfBengaliMonths = {
            13, 12, 14, 13, 14, 14, 15, 15, 15, 15, 14, 14
        };

        internal static readonly int[] DaysInBengaliMonths = {
            30, 30, 30, 30, 31, 31, 31, 31, 31, 30, 30, 30
        };
    }
}