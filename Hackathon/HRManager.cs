using System;
using System.Collections.Generic;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon
{
    public class HRManager
    {
        private readonly ITeamBuildingStrategy _teamBuildingStrategy;

        public HRManager(ITeamBuildingStrategy teamBuildingStrategy)
        {
            _teamBuildingStrategy = teamBuildingStrategy;
        }

        public IEnumerable<Team> BuildTeams(
            IEnumerable<Employee> juniors, 
            IEnumerable<Employee> teamLeads, 
            IEnumerable<Wishlist> juniorsWishlists, 
            IEnumerable<Wishlist> teamLeadsWishlists)
        {
            if (juniors.Count() != teamLeads.Count())
                throw new ArgumentException("The number of team leads and juniors should be the same.");

            return _teamBuildingStrategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        }
    }
}
