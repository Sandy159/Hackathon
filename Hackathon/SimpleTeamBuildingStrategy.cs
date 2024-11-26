using System.Collections.Generic;
using System.Linq;

namespace Hackathon
{
    public class SimpleTeamBuildingStrategy : ITeamBuildingStrategy
    {
        public List<(Junior, TeamLead)> BuildPairs(
            List<Junior> juniors, List<TeamLead> teamLeads, List<Wishlist> juniorsWishlists, List<Wishlist> teamleadsWishlists)
        {
            var pairs = new List<(Junior, TeamLead)>();
            var usedJuniors = new HashSet<int>(); // Храним ID занятых джунов
            var usedTeamLeads = new HashSet<int>(); // Храним ID занятых тимлидов

            // Создадим список всех возможных пар с их рейтингами счастья
            var allPossiblePairs = new List<(Junior junior, TeamLead teamLead, double happiness)>();

            // Пройдемся по каждому джуну и каждому тимлиду и посчитаем совместное счастье
            foreach (var junior in juniors)
            {
                var juniorWishlist = juniorsWishlists.FirstOrDefault(wl => wl.EmployeeId == junior.Id);
                if (juniorWishlist == null) continue;

                foreach (var preferredTeamLeadId in juniorWishlist.DesiredEmployees)
                {
                    var preferredTeamLead = teamLeads.FirstOrDefault(tl => tl.Id == preferredTeamLeadId);
                    if (preferredTeamLead == null || usedTeamLeads.Contains(preferredTeamLeadId)) continue;

                    var teamLeadWishlist = teamleadsWishlists.FirstOrDefault(wl => wl.EmployeeId == preferredTeamLead.Id);
                    if (teamLeadWishlist != null && teamLeadWishlist.DesiredEmployees.Contains(junior.Id))
                    {
                        // Рассчитываем счастье для этой пары
                        int juniorHappiness = juniors.Count - Array.IndexOf(juniorWishlist.DesiredEmployees, preferredTeamLead.Id);;
                        int teamLeadHappiness = teamLeads.Count - Array.IndexOf(teamLeadWishlist.DesiredEmployees, junior.Id);

                        // Вычисляем гармоническое среднее счастья для пары
                        double happiness = CalculateHarmonic(juniorHappiness, teamLeadHappiness);

                        allPossiblePairs.Add((junior, preferredTeamLead, happiness));
                    }
                }
            }

            // Сортируем пары по убыванию счастья
            var sortedPairs = allPossiblePairs.OrderByDescending(p => p.happiness).ToList();

            // Выбираем пары с наибольшим счастьем
            foreach (var pair in sortedPairs)
            {
                if (!usedJuniors.Contains(pair.junior.Id) && !usedTeamLeads.Contains(pair.teamLead.Id))
                {
                    pairs.Add((pair.junior, pair.teamLead));
                    usedJuniors.Add(pair.junior.Id);
                    usedTeamLeads.Add(pair.teamLead.Id);
                }
            }

            return pairs;
        }

        // Рассчитываем гармоническое среднее счастья
        private double CalculateHarmonic(int juniorHappiness, int teamLeadHappiness)
        {
            return 2.0 * juniorHappiness * teamLeadHappiness / (juniorHappiness + teamLeadHappiness);
        }
    }
}
