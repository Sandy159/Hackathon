using Xunit;
using Moq;
using Hackathon;
using Nsu.HackathonProblem.Contracts;


public class HRManagerTests
{
    [Fact]
    public void HRManager_ShouldCreateCorrectNumberOfTeams()
    {
        // Arrange
        var juniors = new List<Employee>
        {
            new Employee(1, "Junior1"),
            new Employee(2, "Junior2"),
            new Employee(3, "Junior3"),
            new Employee(4, "Junior4"),
            new Employee(5, "Junior5")
        };

        var teamLeads = new List<Employee>
        {
            new Employee(1, "TeamLead1"),
            new Employee(2, "TeamLead2"),
            new Employee(3, "TeamLead3"),
            new Employee(4, "TeamLead4"),
            new Employee(5, "TeamLead5")
        };

        var juniorsWishlists = new List<Wishlist>
        {
            new Wishlist(1, new int[] { 1, 2, 3, 4, 5 }),
            new Wishlist(2, new int[] { 2, 3, 1, 5, 4 }),
            new Wishlist(3, new int[] { 4, 5, 2, 1, 3 }),
            new Wishlist(4, new int[] { 5, 4, 3, 2, 1 }),
            new Wishlist(5, new int[] { 3, 1, 2, 5, 4 })
        };

        var teamleadsWishlists = new List<Wishlist>
        {
            new Wishlist(1, new int[] { 1, 2, 3, 4, 5 }),
            new Wishlist(2, new int[] { 2, 3, 4, 1, 5 }),
            new Wishlist(3, new int[] { 3, 4, 1, 2, 5 }),
            new Wishlist(4, new int[] { 4, 5, 3, 2, 1 }),
            new Wishlist(5, new int[] { 5, 1, 2, 3, 4 })
        };

        var hrManager = new HRManager(new SimpleTeamBuildingStrategy());

        // Act
        var teams = hrManager.BuildTeams(juniors, teamLeads, juniorsWishlists, teamleadsWishlists);

        // Assert
        Assert.Equal(juniors.Count, teams.Count());
        Assert.Equal(teamLeads.Count, teams.Count());
    }

    [Fact]
    public void HRManager_ShouldUseStrategyAndReturnExpectedDistribution()
    {
        // Arrange
        var juniors = new List<Employee>
        {
            new Employee(1, "Junior1"),
            new Employee(2, "Junior2"),
            new Employee(3, "Junior3"),
            new Employee(4, "Junior4"),
            new Employee(5, "Junior5")
        };

        var teamLeads = new List<Employee>
        {
            new Employee(1, "TeamLead1"),
            new Employee(2, "TeamLead2"),
            new Employee(3, "TeamLead3"),
            new Employee(4, "TeamLead4"),
            new Employee(5, "TeamLead5")
        };

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
                
        var hrManager = new HRManager(new SimpleTeamBuildingStrategy());

        // Act
        var teams = hrManager.BuildTeams(juniors, teamLeads, juniorsWishlists, teamleadsWishlists);

        // Assert
        List<Team> teamsArray = teams.ToList();
        Assert.Equal(1, teamsArray[0].TeamLead.Id);
        Assert.Equal(2, teamsArray[0].Junior.Id);
        Assert.Equal(2, teamsArray[1].TeamLead.Id);
        Assert.Equal(4, teamsArray[1].Junior.Id);
        Assert.Equal(4, teamsArray[2].TeamLead.Id);
        Assert.Equal(3, teamsArray[2].Junior.Id);
        Assert.Equal(3, teamsArray[3].TeamLead.Id);
        Assert.Equal(1, teamsArray[3].Junior.Id);
        Assert.Equal(5, teamsArray[4].TeamLead.Id);
        Assert.Equal(5, teamsArray[4].Junior.Id);
    }

    [Fact]
    public void HRManager_ShouldCallStrategyExactlyOnce()
    {
        // Arrange
        IEnumerable<Employee> juniors = new List<Employee>
        {
            new Employee(1, "Junior1"),
            new Employee(2, "Junior2"),
            new Employee(3, "Junior3"),
            new Employee(4, "Junior4"),
            new Employee(5, "Junior5")
        };

        IEnumerable<Employee> teamLeads = new List<Employee>
        {
            new Employee(1, "TeamLead1"),
            new Employee(2, "TeamLead2"),
            new Employee(3, "TeamLead3"),
            new Employee(4, "TeamLead4"),
            new Employee(5, "TeamLead5")
        };

        IEnumerable<Wishlist> juniorsWishlists = new List<Wishlist>
        {
            new Wishlist(1, new int[] { 1, 2, 3, 4, 5 }),
            new Wishlist(2, new int[] { 2, 3, 1, 5, 4 }),
            new Wishlist(3, new int[] { 4, 5, 2, 1, 3 }),
            new Wishlist(4, new int[] { 5, 4, 3, 2, 1 }),
            new Wishlist(5, new int[] { 3, 1, 2, 5, 4 })
        };

        IEnumerable<Wishlist> teamleadsWishlists = new List<Wishlist>
        {
            new Wishlist(1, new int[] { 1, 2, 3, 4, 5 }),
            new Wishlist(2, new int[] { 2, 3, 4, 1, 5 }),
            new Wishlist(3, new int[] { 3, 4, 1, 2, 5 }),
            new Wishlist(4, new int[] { 4, 5, 3, 2, 1 }),
            new Wishlist(5, new int[] { 5, 1, 2, 3, 4 })
        };
        var mockStrategy = new Mock<ITeamBuildingStrategy>();
        var hrManager = new HRManager(mockStrategy.Object);

        // Act
        var teams = hrManager.BuildTeams(juniors, teamLeads, juniorsWishlists, teamleadsWishlists);

        // Assert
        mockStrategy.Verify(s => s.BuildTeams(teamLeads, juniors, teamleadsWishlists, juniorsWishlists), Times.Once);
    }

    [Fact]
    public void HRManager_DifferentNumbersOfEmployees_ThrowsArgumentException()
    {
        // Arrange
        var juniors = new List<Employee>
        {
            new Employee(1, "Junior1"),
            new Employee(2, "Junior2"),
            new Employee(3, "Junior3"),
            new Employee(4, "Junior4"),
            new Employee(5, "Junior5")
        };

        var teamLeads = new List<Employee>
        {
            new Employee(1, "TeamLead1"),
            new Employee(2, "TeamLead2"),
            new Employee(3, "TeamLead3"),
            new Employee(4, "TeamLead4")
        };

        var juniorsWishlists = new List<Wishlist>
        {
            new Wishlist(1, new int[] { 1, 2, 3, 4, 5 }),
            new Wishlist(2, new int[] { 2, 3, 1, 5, 4 }),
            new Wishlist(3, new int[] { 4, 5, 2, 1, 3 }),
            new Wishlist(4, new int[] { 5, 4, 3, 2, 1 }),
            new Wishlist(5, new int[] { 3, 1, 2, 5, 4 })
        };

        var teamleadsWishlists = new List<Wishlist>
        {
            new Wishlist(1, new int[] { 1, 2, 3, 4, 5 }),
            new Wishlist(2, new int[] { 2, 3, 4, 1, 5 }),
            new Wishlist(3, new int[] { 3, 4, 1, 2, 5 }),
            new Wishlist(4, new int[] { 4, 5, 3, 2, 1 })
        };

        var hrManager = new HRManager(new SimpleTeamBuildingStrategy());
        Action throwingAction = () => { hrManager.BuildTeams(juniors, teamLeads, juniorsWishlists, teamleadsWishlists); };
    
        // Assert
        Assert.Throws<ArgumentException>(throwingAction);
    }
}