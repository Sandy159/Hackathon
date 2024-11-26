using Xunit;
using Hackathon;

namespace Hackathon.Tests;

public class HRDirectorTests
{
    [Fact]
    public void HarmonicMean_IdenticalNumbers_ShouldReturnSameValue()
    {
        // Arrange
        var hrDirector = new HRDirector();
        List<int[]> values = [];
        values.Add([3, 3]);
        values.Add([3, 3]);
        values.Add([3, 3]);

        // Act
        var harmonicMean = hrDirector.CalculateHarmonic(values);

        // Assert
        Assert.Equal(3, harmonicMean);
    }

    [Theory]
    [InlineData(2, 6, 3.0)]
    [InlineData(2, 8, 3.2)]
    [InlineData(20, 180, 36)]
    public void HarmonicMean_CountHarmonicMeanOfTwoFixNumbers_ShouldReturnFixNumber(int a, int b, double c)
    {
        // Arrange
        var hrDirector = new HRDirector();
        List<int[]> values = [];
        values.Add([a, b]);

        // Act
        var harmonicMean = hrDirector.CalculateHarmonic(values);

        // Assert
        Assert.Equal(c, harmonicMean);
    }

    [Theory]
    [InlineData(10)]
    public void CountHarmonicMean_CountHarmonicMeanOfZeros_ReturnException(int n)
    {
        // Arrange
        var hrDirector = new HRDirector();
        List<int[]> values = [];
        for (int i = 0; i < n; ++i)
        {
            values.Add([0, 0]);
        }

        // Act
        Action throwingAction = () => { hrDirector.CalculateHarmonic(values); };
    
        // Assert
        Assert.Throws<ArgumentException>(throwingAction);
    }

    [Fact]
    public void HarmonicMean_WithPredefinedPreferences_ShouldReturnExpectedValue()
    {
        // Arrange
        var juniorsWishlists = new List<Wishlist>
        {
            new Wishlist(1, new int[] { 1, 2, 3, 4, 5 }),
            new Wishlist(2, new int[] { 1, 3, 5, 4, 2 }),
            new Wishlist(3, new int[] { 4, 5, 2, 1, 3 }),
            new Wishlist(4, new int[] { 2, 3, 1, 4, 5 }),
            new Wishlist(5, new int[] { 1, 2, 4, 5, 3 })
        };

        var teamleadsWishlists = new List<Wishlist>
        {
            new Wishlist(1, new int[] { 2, 1, 4, 3, 5 }),
            new Wishlist(2, new int[] { 4, 3, 5, 1, 2 }),
            new Wishlist(3, new int[] { 4, 2, 5, 1, 3 }),
            new Wishlist(4, new int[] { 2, 3, 1, 5, 4 }),
            new Wishlist(5, new int[] { 4, 1, 2, 5, 3 })
        };
        var teams = new List<(Junior, TeamLead)>
        {
            (new Junior(2, "Junior2"), new TeamLead(1, "TeamLead1")),
            (new Junior(4, "Junior4"), new TeamLead(2, "TeamLead2")),
            (new Junior(3, "Junior3"), new TeamLead(4, "TeamLead4")),
            (new Junior(1, "Junior1"), new TeamLead(3, "TeamLead3")),
            (new Junior(5, "Junior5"), new TeamLead(5, "TeamLead5"))
        };
        var hrDirector = new HRDirector();

        // Act
        var harmonicMean = hrDirector.CountHackathon(teams, juniorsWishlists, teamleadsWishlists);

        // Assert
        Assert.Equal(848.0/225, harmonicMean, 2);
    }
}
