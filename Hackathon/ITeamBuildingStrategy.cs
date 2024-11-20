using System.Collections.Generic;

namespace Hackathon
{
    public interface ITeamBuildingStrategy
    {
        List<(Junior, TeamLead)> BuildPairs(List<Junior> juniors, List<TeamLead> teamLeads, List<Wishlist> juniorsWishlists, List<Wishlist> teamleadsWishlists);
    }
}
