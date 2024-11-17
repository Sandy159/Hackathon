namespace Hackathon
{
    public class Compition
    {
        private List<Junior> juniors = new();
        private List<TeamLead> teamLeads = new();
        private HRManager hrManager;
        private HRDirector hrDirector;

        public Compition(HRManager hrManager, HRDirector hrDirector)
        {
            this.hrManager = hrManager;
            this.hrDirector = hrDirector;
        }

        public void SetParticipants(List<Junior> juniors, List<TeamLead> teamLeads)
        {
            this.juniors = juniors;
            this.teamLeads = teamLeads;
        }

        public double Run()
        {
            var allTeamLeadIds = teamLeads.Select(t => t.Id).ToList();
            var allJuniorIds = juniors.Select(j => j.Id).ToList();
            var random = new Random();

            foreach (var junior in juniors)
            {
                junior.GenerateWishlist(allTeamLeadIds, random);
            }

            foreach (var teamLead in teamLeads)
            {
                teamLead.GenerateWishlist(allJuniorIds, random);
            }

            hrManager.SetParticipants(juniors, teamLeads);
            var pairs = hrManager.BuildPairs();
            
            return hrDirector.CountHackathon(pairs);
        }
    }
}