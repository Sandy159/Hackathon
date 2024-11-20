using System.Collections.Generic;

namespace Hackathon
{
    public class HRManager
    {
        private ITeamBuildingStrategy _teamBuildingStrategy;

        public HRManager(ITeamBuildingStrategy teamBuildingStrategy)
        {
            _teamBuildingStrategy = teamBuildingStrategy;
        }
        
        public List<(Junior, TeamLead)> BuildPairs(List<Junior> juniors, List<TeamLead> teamLeads, List<Wishlist> juniorsWishlists, List<Wishlist> teamleadsWishlists)
        {
            return _teamBuildingStrategy.BuildPairs(juniors, teamLeads, juniorsWishlists, teamleadsWishlists);
        }
    }
}
