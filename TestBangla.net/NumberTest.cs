using bng;

public class NumberTests
{
    [Theory]
    [InlineData("123", "এক শত তেইশ")]
    [InlineData("1000", "এক হাজার")]
    [InlineData("1234", "এক হাজার দুই শত চৌত্রিশ")]
    [InlineData("0", "শূন্য")]
    public void ToBanglaWord_String_ReturnsCorrectWord(string input, string expected)
    {
        var result = Number.ToBanglaWord(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(123, "এক শত তেইশ")]
    [InlineData(1000, "এক হাজার")]
    [InlineData(1234.56, "এক হাজার দুই শত চৌত্রিশ দশমিক পাঁচ ছয়")]
    [InlineData(0, "শূন্য")]
    public void ToBanglaWord_Double_ReturnsCorrectWord(double input, string expected)
    {
        var result = Number.ToBanglaWord(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123", "১২৩")]
    [InlineData("1000", "১০০০")]
    [InlineData("0", "০")]
    public void ToBanglaNumber_String_ReturnsCorrectNumerals(string input, string expected)
    {
        var result = Number.ToBanglaNumber(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(123, "১২৩")]
    [InlineData(1000, "১০০০")]
    [InlineData(1234.56, "১২৩৪.৫৬")]
    [InlineData(0, "০")]
    public void ToBanglaNumber_Double_ReturnsCorrectNumerals(double input, string expected)
    {
        var result = Number.ToBanglaNumber(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("12.34.56")]
    [InlineData("")]
    public void ToBanglaWord_WithInvalidInput_ThrowsFormatException(string input)
    {
        Assert.Throws<FormatException>(() => Number.ToBanglaWord(input));
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("12.34.56")]
    [InlineData("")]
    public void ToBanglaNumber_WithInvalidInput_ThrowsFormatException(string input)
    {
        Assert.Throws<FormatException>(() => Number.ToBanglaNumber(input));
    }
}