using Xunit;
using Hackathon;

public class HackathonTests
{
    [Fact]
    public void Hackathon_WithPredefinedParticipants_ShouldReturnExpectedHarmonicity()
    {
        // Arrange
        
        string juniorsFile = "Juniors5.csv";
        string teamLeadsFile = "Teamleads5.csv";
        var juniors = DataLoader.LoadEmployees(juniorsFile, (id, name) => new Junior(id, name));
        var teamLeads = DataLoader.LoadEmployees(teamLeadsFile, (id, name) => new TeamLead(id, name));
        List<Wishlist> juniorsWishlists = TestDataInitializer.GetJuniorsWishlist(juniors.ToList());
        List<Wishlist> teamleadsWishlists = TestDataInitializer.GetTeamLeadsWishlist(teamLeads.ToList());
        var hrManager = new HRManager(new SimpleTeamBuildingStrategy());
        var pairs = hrManager.BuildPairs(juniors, teamLeads, juniorsWishlists, teamleadsWishlists);
        var hrDirector = new HRDirector();
        var hackathon = new Compition(hrManager, hrDirector);
        hackathon.SetParticipants(juniors, teamLeads);

        // Act
        var harmonicity = hackathon.RunHackathon(pairs, juniorsWishlists, teamleadsWishlists);

        // Assert
        Assert.Equal(179.0/45, harmonicity, 2); 
    }
}
