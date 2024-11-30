using System.Collections.Generic;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon
{
    public class Compition
    {
        private List<Employee> juniors = new();
        private List<Employee> teamLeads = new();
        private readonly HRManager _hrManager;
        private readonly HRDirector _hrDirector;

        public Compition(HRManager hrManager, HRDirector hrDirector)
        {
            _hrManager = hrManager;
            _hrDirector = hrDirector;
        }

        public void SetParticipants(List<Employee> juniors, List<Employee> teamLeads)
        {
            this.juniors = juniors;
            this.teamLeads = teamLeads;
        }

        public double RunHackathon(IEnumerable<Team> teams, IEnumerable<Wishlist> juniorsWishlists, IEnumerable<Wishlist> teamLeadsWishlists)
        {
            return _hrDirector.CountHackathon(teams, juniorsWishlists, teamLeadsWishlists);
        }

        public double Run()
        {
            var juniorsWishlists = WishlistGenerator.GenerateWishlist(juniors, teamLeads);
            var teamLeadsWishlists = WishlistGenerator.GenerateWishlist(teamLeads, juniors);

            var teams = _hrManager.BuildTeams(juniors, teamLeads, juniorsWishlists, teamLeadsWishlists);

            return RunHackathon(teams, juniorsWishlists, teamLeadsWishlists);
        }
    }
}
