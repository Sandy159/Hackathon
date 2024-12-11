using System.Collections.Generic;
using System.Linq;

namespace CommonLibrary.Contracts
{
    public class SimpleTeamBuildingStrategy : ITeamBuildingStrategy
    {
        public IEnumerable<Team> BuildTeams(
            IEnumerable<Employee> teamLeads,
            IEnumerable<Employee> juniors,
            IEnumerable<Wishlist> teamLeadsWishlists,
            IEnumerable<Wishlist> juniorsWishlists)
        {
            var pairs = new List<Team>();
            var usedJuniors = new HashSet<int>();
            var usedTeamLeads = new HashSet<int>();

            // Создаем список всех возможных пар с их рейтингами счастья
            var allPossiblePairs = new List<(Employee junior, Employee teamLead, double happiness)>();

            foreach (var junior in juniors)
            {
                var juniorWishlist = juniorsWishlists.FirstOrDefault(wl => wl.EmployeeId == junior.Id);
                if (juniorWishlist == null) continue;

                foreach (var preferredTeamLeadId in juniorWishlist.Preferences)
                {
                    var preferredTeamLead = teamLeads.FirstOrDefault(tl => tl.Id == preferredTeamLeadId);
                    if (preferredTeamLead == null || usedTeamLeads.Contains(preferredTeamLeadId)) continue;

                    var teamLeadWishlist = teamLeadsWishlists.FirstOrDefault(wl => wl.EmployeeId == preferredTeamLead.Id);
                    if (teamLeadWishlist != null && teamLeadWishlist.Preferences.Contains(junior.Id))
                    {
                        // Рассчитываем счастье для пары
                        int juniorHappiness = juniors.Count() - Array.IndexOf(juniorWishlist.Preferences.ToArray(), preferredTeamLead.Id);
                        int teamLeadHappiness = teamLeads.Count() - Array.IndexOf(teamLeadWishlist.Preferences.ToArray(), junior.Id);

                        // Гармоническое среднее счастья
                        double happiness = CalculateHarmonic(juniorHappiness, teamLeadHappiness);

                        allPossiblePairs.Add((junior, preferredTeamLead, happiness));
                    }
                }
            }

            // Сортируем пары по убыванию счастья
            var sortedPairs = allPossiblePairs.OrderByDescending(p => p.happiness).ToList();

            // Выбираем пары с максимальным счастьем
            foreach (var pair in sortedPairs)
            {
                if (!usedJuniors.Contains(pair.junior.Id) && !usedTeamLeads.Contains(pair.teamLead.Id))
                {
                    pairs.Add(new Team(pair.teamLead, pair.junior));
                    usedJuniors.Add(pair.junior.Id);
                    usedTeamLeads.Add(pair.teamLead.Id);
                }
            }

            return pairs;
        }

        private double CalculateHarmonic(int juniorHappiness, int teamLeadHappiness)
        {
            return 2.0 * juniorHappiness * teamLeadHappiness / (juniorHappiness + teamLeadHappiness);
        }

    }
}
