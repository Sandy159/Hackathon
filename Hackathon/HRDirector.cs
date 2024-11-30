using System;
using System.Collections.Generic;
using System.Linq;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon
{
    public class HRDirector
    {
        public double CountHackathon(
            IEnumerable<Team> teams, 
            IEnumerable<Wishlist> juniorsWishlists, 
            IEnumerable<Wishlist> teamLeadsWishlists)
        {
            if (teams == null || juniorsWishlists == null || teamLeadsWishlists == null)
            {
                throw new ArgumentNullException("Списки команд или пожеланий не должны быть null.");
            }

            var happinessIndexes = new List<int[]>();

            foreach (var team in teams)
            {
                var juniorWishlist = juniorsWishlists.FirstOrDefault(wl => wl.EmployeeId == team.Junior.Id);
                var teamLeadWishlist = teamLeadsWishlists.FirstOrDefault(wl => wl.EmployeeId == team.TeamLead.Id);

                if (juniorWishlist == null || teamLeadWishlist == null)
                {
                    continue; // Если у кого-то из участников нет пожеланий, пропускаем.
                }

                int juniorHappiness = teams.Count() - Array.IndexOf(juniorWishlist.DesiredEmployees, team.TeamLead.Id);
                int teamLeadHappiness = teams.Count() - Array.IndexOf(teamLeadWishlist.DesiredEmployees, team.Junior.Id);

                if (juniorHappiness > 0 && teamLeadHappiness > 0)
                {
                    happinessIndexes.Add(new[] { juniorHappiness, teamLeadHappiness });
                }
            }

            return CalculateHarmonic(happinessIndexes);
        }

        public double CalculateHarmonic(List<int[]> happinessIndexes)
        {
            if (happinessIndexes == null || !happinessIndexes.Any())
            {
                throw new ArgumentException("Список индексов счастья пуст. Невозможно рассчитать гармоничность.");
            }

            double totalHappiness = 0.0;

            foreach (var happiness in happinessIndexes)
            {
                int juniorHappiness = happiness[0];
                int teamLeadHappiness = happiness[1];

                if (juniorHappiness == 0 || teamLeadHappiness == 0)
                {
                    throw new ArgumentException("Индексы счастья не могут быть равны нулю.");
                }

                // Рассчитываем гармоническое среднее для текущей пары.
                double harmonicMean = 2.0 * juniorHappiness * teamLeadHappiness / (juniorHappiness + teamLeadHappiness);
                totalHappiness += harmonicMean;
            }

            return totalHappiness / happinessIndexes.Count;
        }
    }
}
