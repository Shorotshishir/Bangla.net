# Bangla.net

A .NET library for numeric data and date data conversion to bangla language specific terms. Inspired from the python library [`bangla`](https://github.com/arsho/bangla)

## Features

- convert roman number to bengali number
- convert georgian calendar to bangla calendar

### Number

Number is a static class which is part of the `bng` namespace. It provides 2 main functions with 1 more override for each. You can see the example to understand the usage.

```cs
using bng;

var banglaWord = Number.ToBanglaWord(10011111002548976);
// output : এক শত কোটি এগারো লক্ষ এগারো হাজার এক শত কোটি পঁচিশ লক্ষ আটচল্লিশ হাজার নয় শত ছিয়াত্তর

var banglaWord = Number.ToBanglaWord("10011111002548976");
// output : এক শত কোটি এগারো লক্ষ এগারো হাজার এক শত কোটি পঁচিশ লক্ষ আটচল্লিশ হাজার নয় শত ছিয়াত্তর

var banglaWord = Number.ToBanglaWord("1234.56");, 
// output : "এক হাজার দুই শত চৌত্রিশ দশমিক পাঁচ ছয়"

var banglaWord = Number.ToBanglaWord(1234.56);, 
// output : "এক হাজার দুই শত চৌত্রিশ দশমিক পাঁচ ছয়"

var banglaNum = Number.ToBanglaNumber(10011111002548976);
// output : ১০০১১১১১০০২৫৪৮৯৭৬

var banglaNum = Number.ToBanglaNumber("10011111002548976");
// output : ১০০১১১১১০০২৫৪৮৯৭৬

var banglaNum = Number.ToBanglaNumber(10011111002548976);
// output : ১০০১১১১১০০২৫৪৮৯৭৬

var banglaNum = Number.ToBanglaNumber("10011111002548976");
// output : ১০০১১১১১০০২৫৪৮৯৭৬

"1234.88", "১২৩৪.৮৮"

var banglaNum = Number.ToBanglaNumber(10011111002548976);
// output : ১০০১১১১১০০২৫৪৮৯৭৬

var banglaNum = Number.ToBanglaNumber("10011111002548976");
// output : ১০০১১১১১০০২৫৪৮৯৭৬
```

### Calendar

Calendar is a static class which is part of the `bng` namespace.
provides 1 main function with 2 more overrides. You can see the example to understand the usage. `ToString()` is overriden to provide a cleaner output. 

```cs
using System;
using bng;
 
var today = Calendar.GetBengaliDate();
Console.WriteLine(today.ToString());
// output : ১৬ (ষোলোই) বৈশাখ, ১৪৩২ বঙ্গাব্দ ; মঙ্গলবার, গ্রীষ্ম কাল ।

var thisDay = Calendar.GetBengaliDate(15, 8, 1995);
// output : ৩১ (একত্রিশে) শ্রাবণ, ১৪০২ বঙ্গাব্দ ; মঙ্গলবার, বর্ষা কাল ।

var maxDate = Calendar.GetBengaliDate(DateTime.MaxValue);
// output : ১৭ (সতেরোই) পৌষ, ৯৪০৬ বঙ্গাব্দ ; শুক্রবার, শীত কাল ।
```

Calendar Functions return a `BengaliDateInfo` object. Currently this is a object to return the data in a organized way. No functions take the data as input. Perhaps a future implementation

```json
// this is the structure of the object
{
    "Date":"১৬",
    "Month":"বৈশাখ",
    "Year":"১৪৩২",
    "Season":"গ্রীষ্ম",
    "Weekday":"মঙ্গলবার",
    "Ordinal":"ষোলোই"
}
```

### tips

you may need to configure the json serializer to get correct bangla words output. or else it may print out as Unicode escaped characters

```cs
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

var option = new JsonSerializerOptions
{
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
};
var today = Calendar.GetBengaliDate();
JsonSerializer.Serialize(today, option)
```
