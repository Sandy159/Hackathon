using System.Collections.Generic;
using System.Linq;

namespace Hackathon
{
    public class SimpleTeamBuildingStrategy : ITeamBuildingStrategy
    {
        public List<(Junior, TeamLead)> BuildPairs(List<Junior> juniors, List<TeamLead> teamLeads)
        {
            var pairs = new List<(Junior, TeamLead)>();

            // Пройдемся по каждому джуну и найдем для него подходящего тимлида
            foreach (var junior in juniors)
            {
                foreach (var preferredTeamLeadId in junior.wishlist.DesiredEmployees)
                {
                    // Находим тимлида по ID, который в приоритетах джуна
                    var preferredTeamLead = teamLeads.FirstOrDefault(tl => tl.Id == preferredTeamLeadId);

                    // Проверяем, что предпочитаемый тимлид не равен null и имеет в пожеланиях текущего джуна
                    if (preferredTeamLead != null && preferredTeamLead.wishlist.DesiredEmployees.Contains(junior.Id))
                    {
                        pairs.Add((junior, preferredTeamLead));
                        break; // Переходим к следующему джуну после нахождения пары
                    }
                }
            }

            return pairs;
        }
    }
}
