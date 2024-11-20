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

        public double RunHackathon(List<(Junior, TeamLead)> pairs, List<Wishlist> juniorsWishlists, List<Wishlist> teamleadsWishlists)
        {
            return hrDirector.CountHackathon(pairs, juniorsWishlists, teamleadsWishlists);
        }

        public double Run()
        {
            var juniorsWishlists = WishlistGenerator.GenerateWishlist(juniors, teamLeads);
            var teamleadsWishlists = WishlistGenerator.GenerateWishlist(teamLeads, juniors);

            var pairs = hrManager.BuildPairs(juniors, teamLeads, juniorsWishlists, teamleadsWishlists);
            
            return RunHackathon(pairs, juniorsWishlists, teamleadsWishlists);
        }
    }
}