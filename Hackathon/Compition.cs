namespace Hackathon
{
    public class Compition
    {
        private List<Junior> juniors;
        private List<TeamLead> teamLeads;
        private HRManager hrManager;
        private HRDirector hrDirector;

        public Compition(List<Junior> juniors, List<TeamLead> teamLeads, HRManager hrManager, HRDirector hrDirector)
        {
            this.juniors = juniors;
            this.teamLeads = teamLeads;
            this.hrManager = hrManager;
            this.hrDirector = hrDirector;
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

                var pairs = hrManager.BuildPairs();
                
                return hrDirector.CountHackathon(pairs);
            }
    }
}