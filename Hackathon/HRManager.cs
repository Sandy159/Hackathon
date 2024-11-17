using System.Collections.Generic;

namespace Hackathon
{
    public class HRManager
    {
        public List<Junior> juniors = new();
        public List<TeamLead> teamLeads = new();
        private ITeamBuildingStrategy _teamBuildingStrategy;

        public HRManager()
        {
            _teamBuildingStrategy = new SimpleTeamBuildingStrategy();
        }

        public void SetParticipants(List<Junior> juniors, List<TeamLead> teamLeads)
        {
            this.juniors = juniors;
            this.teamLeads = teamLeads;
        }
        
        public List<(Junior, TeamLead)> BuildPairs()
        {
            return _teamBuildingStrategy.BuildPairs(juniors, teamLeads);
        }
    }
}
