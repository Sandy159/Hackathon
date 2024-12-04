using System.Collections;
using System.Collections.Generic;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon
{
    public class Compition
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public IEnumerable<Wishlist>? Wishlists { get; set; }
        public IEnumerable<Team>? Teams { get; set; }

        public double RunHackathon
            (IEnumerable<Employee> juniors, IEnumerable<Employee> teamLeads, 
            HRManager hrManager, HRDirector hrDirector, 
            IEnumerable<Wishlist>? juniorsWishlists = null, IEnumerable<Wishlist>? teamLeadsWishlists = null)
        {
            juniorsWishlists ??= WishlistGenerator.GenerateWishlist(juniors, teamLeads);
            teamLeadsWishlists ??= WishlistGenerator.GenerateWishlist(teamLeads, juniors);
            this.Wishlists = juniorsWishlists.Concat(teamLeadsWishlists);

            var teams = hrManager.BuildTeams(juniors, teamLeads, juniorsWishlists, teamLeadsWishlists);
            this.Teams = teams;

            return hrDirector.CountHackathon(teams, juniorsWishlists, teamLeadsWishlists);
        }
    }
}
