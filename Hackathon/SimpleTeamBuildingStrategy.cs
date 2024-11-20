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

            // Пройдемся по каждому джуну и найдем для него подходящего тимлида
            foreach (var junior in juniors)
            {
                if (usedJuniors.Contains(junior.Id)) 
                    continue; // Пропускаем, если джун уже в паре

                var juniorWishlist = juniorsWishlists.FirstOrDefault(wl => wl.EmployeeId == junior.Id);
                if (juniorWishlist == null) continue;

                foreach (var preferredTeamLeadId in juniorWishlist.DesiredEmployees)
                {
                    // Находим тимлида по ID, который в приоритетах джуна
                    if (usedTeamLeads.Contains(preferredTeamLeadId)) 
                        continue; // Пропускаем, если тимлид уже в паре

                    var preferredTeamLead = teamLeads.FirstOrDefault(tl => tl.Id == preferredTeamLeadId);

                    // Проверяем, что предпочитаемый тимлид не равен null и имеет в пожеланиях текущего джуна
                    if (preferredTeamLead != null)
                    {
                        var teamLeadWishlist = teamleadsWishlists.FirstOrDefault(wl => wl.EmployeeId == preferredTeamLead.Id);

                        if (teamLeadWishlist != null && teamLeadWishlist.DesiredEmployees.Contains(junior.Id))
                        {
                            // Добавляем пару
                            pairs.Add((junior, preferredTeamLead));
                            // Отмечаем джуна и тимлида как занятых
                            usedJuniors.Add(junior.Id);
                            usedTeamLeads.Add(preferredTeamLead.Id);
                            break; // Переходим к следующему джуну после нахождения пары
                        }
                    }
                }
            } 

            return pairs;
        }
    }
}
