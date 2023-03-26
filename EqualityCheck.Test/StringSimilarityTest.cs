namespace EqualityCheck.Test;

public class StringSimilarityTest
{
    [Theory]
    [InlineData(null, "abcdefgh")]
    [InlineData("", "abcdefgh")]
    [InlineData(" ", "abcdefgh")]
    [InlineData("abcdefgh", null)]
    [InlineData("abcdefgh", "")]
    [InlineData("abcdefgh", " ")]
    public void StringSimilarity_CheckSimilarityShouldThrowException_WhenInputsAreNullOrWhiteSpace(string source, string target)
    {
        var exception = Assert.Throws<Exception>(() => StringSimilarity.ComputeSimilarity(source, target));
        Assert.Equal("The inputs could not be null or white space.", exception.Message);
    }

    [Theory]
    [InlineData(null, "abcdefgh")]
    [InlineData("", "abcdefgh")]
    [InlineData(" ", "abcdefgh")]
    [InlineData("abcdefgh", null)]
    [InlineData("abcdefgh", "")]
    [InlineData("abcdefgh", " ")]
    public void StringSimilarity_CheckSimilarityPercentageShouldThrowException_WhenInputsAreNullOrWhiteSpace(string source, string target)
    {
        var exception = Assert.Throws<Exception>(() => StringSimilarity.ComputeSimilarityPercentage(source, target));
        Assert.Equal("The inputs could not be null or white space.", exception.Message);
    }

    [Theory]
    [InlineData("abcdefgh", "ijklmnop", true, 0)]
    [InlineData("abcdefgh", "ABCDEFGH", true, 0)]
    [InlineData("abcdefgh", "abijklmn", true, 0.25)]
    [InlineData("abcdefgh", "abcdijkl", true, 0.5)]
    [InlineData("abcdefgh", "abcdefij", true, 0.75)]
    [InlineData("abcdefgh", "abcdefgh", true, 1)]
    [InlineData("abcdefgh", "ABCDEFGH", false, 1)]
    public void StringSimilarity_CheckSimilarityByDifferentInputs(string source, string target, bool caseSensitive, float expectedResult)
    {
        var percentage = StringSimilarity.ComputeSimilarity(source, target, caseSensitive);
        Assert.Equal(expectedResult, percentage);
    }

    [Theory]
    [InlineData("abcdefgh", "ijklmnop", true, 0)]
    [InlineData("abcdefgh", "ABCDEFGH", true, 0)]
    [InlineData("abcdefgh", "abijklmn", true, 25.00)]
    [InlineData("abcdefgh", "abcdijkl", true, 50.00)]
    [InlineData("abcdefgh", "abcdefij", true, 75.00)]
    [InlineData("abcdefgh", "abcdefgh", true, 100.00)]
    [InlineData("abcdefgh", "ABCDEFGH", false, 100.00)]
    public void StringSimilarity_CheckSimilarityPercentagesByDifferentInputs(string source, string target, bool caseSensitive, float expectedResult)
    {
        var percentage = StringSimilarity.ComputeSimilarityPercentage(source, target, caseSensitive);
        Assert.Equal(expectedResult, percentage);
    }
}
