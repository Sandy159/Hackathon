using System;
using System.Collections.Generic;

namespace Hackathon
{
    public class HRDirector
    {
        public double CountHackathon(List<(Junior, TeamLead)> pairs, List<Wishlist> juniorsWishlists, List<Wishlist> teamleadsWishlists)
        {
            if (pairs == null || juniorsWishlists == null || teamleadsWishlists == null)
            {
                throw new ArgumentNullException("Списки не должны быть null.");
            }

            List<int[]> happinessIndexes = [];

            foreach (var (junior, teamLead) in pairs)
            {
                var juniorWishlist = juniorsWishlists.FirstOrDefault(wl => wl.EmployeeId == junior.Id);
                var teamleadWishlist = teamleadsWishlists.FirstOrDefault(wl => wl.EmployeeId == teamLead.Id);

                if (juniorWishlist == null || teamleadWishlist == null)
                {
                    continue; 
                }

                int juniorHappiness = pairs.Count - Array.IndexOf(juniorWishlist.DesiredEmployees, teamLead.Id);
                int teamLeadHappiness = pairs.Count - Array.IndexOf(teamleadWishlist.DesiredEmployees, junior.Id);

                if (juniorHappiness > 0 && teamLeadHappiness > 0)
                {
                    happinessIndexes.Add([juniorHappiness, teamLeadHappiness]);
                }
            }
            double harmonicity = CalculateHarmonic(happinessIndexes);

            return harmonicity;
        }

        public double CalculateHarmonic(List<int[]> happinessIndexes)
        {
            double totalHappiness = 0.0;

            foreach (var happiness in happinessIndexes)
            {
                int juniorHappiness = happiness[0];
                int teamLeadHappiness = happiness[1];

                double harmonicMean = 2.0 * juniorHappiness * teamLeadHappiness / (juniorHappiness + teamLeadHappiness);
                totalHappiness += harmonicMean;
            }
            
            return totalHappiness / happinessIndexes.Count;
        }
    }
}
