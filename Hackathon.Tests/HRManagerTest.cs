using Xunit;
using Moq;
using Hackathon;


public class HRManagerTests
{
    [Fact]
    public void HRManager_ShouldCreateCorrectNumberOfTeams()
    {
        // Arrange
        string juniorsFile = "Juniors20.csv";
        string teamLeadsFile = "Teamleads20.csv";
        var juniors = DataLoader.LoadEmployees(juniorsFile, (id, name) => new Junior(id, name));
        var teamLeads = DataLoader.LoadEmployees(teamLeadsFile, (id, name) => new TeamLead(id, name));
        var juniorsWishlists = WishlistGenerator.GenerateWishlist(juniors, teamLeads);
        var teamleadsWishlists = WishlistGenerator.GenerateWishlist(teamLeads, juniors);
        var hrManager = new HRManager(new SimpleTeamBuildingStrategy());

        // Act
        var teams = hrManager.BuildPairs(juniors, teamLeads, juniorsWishlists, teamleadsWishlists);

        // Assert
        Assert.Equal(juniors.Count, teams.Count);
        Assert.Equal(teamLeads.Count, teams.Count);
    }

    [Fact]
    public void HRManager_ShouldUseStrategyAndReturnExpectedDistribution()
    {
        // Arrange
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = DataLoader.LoadEmployees(juniorsFile, (id, name) => new Junior(id, name));
        var teamLeads = DataLoader.LoadEmployees(teamLeadsFile, (id, name) => new TeamLead(id, name));
        List<Wishlist> juniorsWishlists = TestDataInitializer.GetJuniorsWishlist(juniors.ToList());
        List<Wishlist> teamleadsWishlists = TestDataInitializer.GetTeamLeadsWishlist(teamLeads.ToList());
        var hrManager = new HRManager(new SimpleTeamBuildingStrategy());

        // Act
        var teams = hrManager.BuildPairs(juniors, teamLeads, juniorsWishlists, teamleadsWishlists);

        // Assert
        List<(Junior, TeamLead)> teamsArray = teams.ToList();
        Assert.Equal(1, teamsArray[0].Item1.Id);
        Assert.Equal(1, teamsArray[0].Item2.Id);
        Assert.Equal(2, teamsArray[1].Item1.Id);
        Assert.Equal(3, teamsArray[1].Item2.Id);
        Assert.Equal(3, teamsArray[2].Item1.Id);
        Assert.Equal(4, teamsArray[2].Item2.Id);
        Assert.Equal(4, teamsArray[3].Item1.Id);
        Assert.Equal(2, teamsArray[3].Item2.Id);
        Assert.Equal(5, teamsArray[4].Item1.Id);
        Assert.Equal(5, teamsArray[4].Item2.Id);
    }

    [Fact]
    public void HRManager_ShouldCallStrategyExactlyOnce()
    {
        // Arrange
        string juniorsFile = "Juniors20.csv";
        string teamLeadsFile = "Teamleads20.csv";
        var juniors = DataLoader.LoadEmployees(juniorsFile, (id, name) => new Junior(id, name));
        var teamLeads = DataLoader.LoadEmployees(teamLeadsFile, (id, name) => new TeamLead(id, name));
        var juniorsWishlists = WishlistGenerator.GenerateWishlist(juniors, teamLeads);
        var teamleadsWishlists = WishlistGenerator.GenerateWishlist(teamLeads, juniors);
        var mockStrategy = new Mock<ITeamBuildingStrategy>();
        var hrManager = new HRManager(mockStrategy.Object);

        // Act
        hrManager.BuildPairs(juniors, teamLeads, juniorsWishlists, teamleadsWishlists);

        // Assert
        mockStrategy.Verify(s => s.BuildPairs(juniors, teamLeads, juniorsWishlists, teamleadsWishlists), Times.Once);
    }
}
