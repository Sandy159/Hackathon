using Xunit;
using System.Collections.Generic;
using Hackathon;


public class WishlistTests
{
    [Fact]
    public void Wishlist_ShouldHaveCorrectSize()
    {
        // Arrange
        string juniorsFile = "Juniors20.csv";
        string teamLeadsFile = "Teamleads20.csv";
        var juniors = DataLoader.LoadEmployees(juniorsFile, (id, name) => new Junior(id, name));
        var teamLeads = DataLoader.LoadEmployees(teamLeadsFile, (id, name) => new TeamLead(id, name));

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
        string juniorsFile = "Juniors20.csv";
        string teamLeadsFile = "Teamleads20.csv";
        var juniors = DataLoader.LoadEmployees(juniorsFile, (id, name) => new Junior(id, name));
        var teamLeads = DataLoader.LoadEmployees(teamLeadsFile, (id, name) => new TeamLead(id, name));

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
