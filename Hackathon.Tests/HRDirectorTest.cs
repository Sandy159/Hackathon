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
    public void HarmonicMean_TwoAndSix_ShouldReturnThree(int a, int b, double c)
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

    [Fact]
    public void HarmonicMean_WithPredefinedPreferences_ShouldReturnExpectedValue()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = DataLoader.LoadEmployees(juniorsFile, (id, name) => new Junior(id, name));
        var teamLeads = DataLoader.LoadEmployees(teamLeadsFile, (id, name) => new TeamLead(id, name));
        List<Wishlist> juniorsWishlists = TestDataInitializer.GetJuniorsWishlist(juniors.ToList());
        List<Wishlist> teamleadsWishlists = TestDataInitializer.GetTeamLeadsWishlist(teamLeads.ToList());
        var hrManager = new HRManager(new SimpleTeamBuildingStrategy());
        var teams = hrManager.BuildPairs(juniors, teamLeads, juniorsWishlists, teamleadsWishlists);
        var hrDirector = new HRDirector();

        // Act
        var harmonicMean = hrDirector.CountHackathon(teams, juniorsWishlists, teamleadsWishlists);

        // Assert
        Assert.Equal(179.0/45, harmonicMean, 2);
    }
}
