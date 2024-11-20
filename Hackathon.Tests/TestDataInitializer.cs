using Hackathon;

public static class TestDataInitializer
{
    public static List<Wishlist> GetJuniorsWishlist(List<Junior> juniors)
    {
        List<Wishlist> juniorsDesiredEmployees = new List<Wishlist>();
        juniorsDesiredEmployees.Add(new Wishlist(juniors[0].Id, new int[] { 1, 2, 3, 4, 5 }));
        juniorsDesiredEmployees.Add(new Wishlist(juniors[1].Id, new int[] { 1, 3, 5, 4, 2 }));
        juniorsDesiredEmployees.Add(new Wishlist(juniors[2].Id, new int[] { 4, 5, 2, 1, 3 }));
        juniorsDesiredEmployees.Add(new Wishlist(juniors[3].Id, new int[] { 2, 3, 1, 4, 5 }));
        juniorsDesiredEmployees.Add(new Wishlist(juniors[4].Id, new int[] { 1, 2, 4, 5, 3 }));
        return juniorsDesiredEmployees;
    }

    public static List<Wishlist> GetTeamLeadsWishlist(List<TeamLead> teamLeads)
    {
        List<Wishlist> teamLeadsDesiredEmployees = new List<Wishlist>();
        teamLeadsDesiredEmployees.Add(new Wishlist(teamLeads[0].Id, new int[] { 2, 1, 4, 3, 5 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(teamLeads[1].Id, new int[] { 4, 3, 5, 1, 2 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(teamLeads[2].Id, new int[] { 4, 2, 5, 1, 3 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(teamLeads[3].Id, new int[] { 2, 3, 1, 5, 4 }));
        teamLeadsDesiredEmployees.Add(new Wishlist(teamLeads[4].Id, new int[] { 4, 1, 2, 5, 3 }));
        return teamLeadsDesiredEmployees;
    }
}