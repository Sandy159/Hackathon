using Xunit;
using Hackathon;
using Nsu.HackathonProblem.Contracts;

public class HackathonTests
{
    [Fact]
    public void Hackathon_WithPredefinedParticipants_ShouldReturnExpectedHarmonicity()
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
        var teams = new List<Team>
        {
            new Team(new Employee(1, "TeamLead1"), new Employee(2, "Junior2")),
            new Team(new Employee(2, "TeamLead2"), new Employee(4, "Junior4")),
            new Team(new Employee(4, "TeamLead4"), new Employee(3, "Junior3")),
            new Team(new Employee(3, "TeamLead3"), new Employee(1, "Junior1")),
            new Team(new Employee(5, "TeamLead5"), new Employee(5, "Junior5"))
        };
        var hrManager = new HRManager(new SimpleTeamBuildingStrategy());
        var hrDirector = new HRDirector();
        var hackathon = new Compition(hrManager, hrDirector);

        // Act
        var harmonicity = hackathon.RunHackathon(teams, juniorsWishlists, teamleadsWishlists);

        // Assert
        Assert.Equal(848.0/225, harmonicity, 2); 
    }
}
