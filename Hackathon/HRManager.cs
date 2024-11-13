using System.Collections.Generic;

namespace Hackathon
{
    public class HRManager
    {
        public List<Junior> Juniors { get; private set; }
        public List<TeamLead> TeamLeads { get; private set; }
        private ITeamBuildingStrategy _teamBuildingStrategy;

        public HRManager(List<Junior> juniors, List<TeamLead> teamLeads)
        {
            Juniors = juniors;
            TeamLeads = teamLeads;
            _teamBuildingStrategy = new SimpleTeamBuildingStrategy();
        }
        
        public List<(Junior, TeamLead)> BuildPairs()
        {
            return _teamBuildingStrategy.BuildPairs(Juniors, TeamLeads);
        }
    }
}
