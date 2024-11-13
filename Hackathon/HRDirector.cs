using System;
using System.Collections.Generic;

namespace Hackathon
{
    public class HRDirector
    {
        public double CountHackathon(List<(Junior, TeamLead)> pairs)
        {
            double totalHappiness = 0.0;

            foreach (var (junior, teamLead) in pairs)
            {
                int juniorHappiness = 20 - Array.IndexOf(junior.wishlist.DesiredEmployees, teamLead.Id);
                int teamLeadHappiness = 20 - Array.IndexOf(teamLead.wishlist.DesiredEmployees, junior.Id);

                if (juniorHappiness <= 0 || teamLeadHappiness <= 0) continue;

                double harmonicMean = 2.0 * juniorHappiness * teamLeadHappiness / (juniorHappiness + teamLeadHappiness);
                totalHappiness += harmonicMean;
            }

            return totalHappiness / pairs.Count;
        }
    }
}
