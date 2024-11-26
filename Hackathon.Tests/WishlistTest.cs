using Xunit;
using System.Collections.Generic;
using Hackathon;


public class WishlistTests
{
    [Fact]
    public void Wishlist_ShouldHaveCorrectSize()
    {
        // Arrange
        var juniors = new List<Junior>
        {
            new Junior(1, "Junior1"),
            new Junior(2, "Junior2"),
            new Junior(3, "Junior3"),
            new Junior(4, "Junior4"),
            new Junior(5, "Junior5")
        };

        var teamLeads = new List<TeamLead>
        {
            new TeamLead(1, "TeamLead1"),
            new TeamLead(2, "TeamLead2"),
            new TeamLead(3, "TeamLead3"),
            new TeamLead(4, "TeamLead4"),
            new TeamLead(5, "TeamLead5")
        };

        // Act
        var juniorsWishlists = WishlistGenerator.GenerateWishlist(juniors, teamLeads);
        var teamleadsWishlists = WishlistGenerator.GenerateWishlist(teamLeads, juniors);

        // Assert
        Assert.All(juniorsWishlists, juniorsWishlist => Assert.Equal(teamLeads.Count, juniorsWishlist.DesiredEmployees.Length));
        Assert.All(teamleadsWishlists, teamleadsWishlist => Assert.Equal(juniors.Count, teamleadsWishlist.DesiredEmployees.Length));
    }

    [Fact]
    public void Wishlist_ShouldContainSpecificEmployee()
    {
        // Arrange
        var juniors = new List<Junior>
        {
            new Junior(1, "Junior1"),
            new Junior(2, "Junior2"),
            new Junior(3, "Junior3"),
            new Junior(4, "Junior4"),
            new Junior(5, "Junior5")
        };

        var teamLeads = new List<TeamLead>
        {
            new TeamLead(1, "TeamLead1"),
            new TeamLead(2, "TeamLead2"),
            new TeamLead(3, "TeamLead3"),
            new TeamLead(4, "TeamLead4"),
            new TeamLead(5, "TeamLead5")
        };

        // Act
        var juniorsWishlists = WishlistGenerator.GenerateWishlist(juniors, teamLeads);
        var teamleadsWishlists = WishlistGenerator.GenerateWishlist(teamLeads, juniors);

        // Assert
        foreach (var junior in juniors)
        {
            var wishlist = juniorsWishlists.FirstOrDefault(wl => wl.EmployeeId == junior.Id);
            Assert.NotNull(wishlist);
            Assert.True(teamLeads.All(tl => wishlist.DesiredEmployees.Contains(tl.Id)));
        }

        foreach (var teamLead in teamLeads)
        {
            var wishlist = teamleadsWishlists.FirstOrDefault(wl => wl.EmployeeId == teamLead.Id);
            Assert.NotNull(wishlist);
            Assert.True(juniors.All(j => wishlist.DesiredEmployees.Contains(j.Id)));
        }
    }
}
