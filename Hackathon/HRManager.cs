using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hackathon
{
    // Класс, управляющий хакатоном
    public class HRManager
    {
        public List<Junior> Juniors { get; private set; } = new List<Junior>();
        public List<TeamLead> TeamLeads { get; private set; } = new List<TeamLead>();
        private Random random;

        public HRManager(string juniorsFile, string teamLeadsFile)
        {
            random = new Random();

            LoadTeamLeads(teamLeadsFile);
            LoadJuniors(juniorsFile);
        }

        private void LoadTeamLeads(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл не найден: {filePath}");
                return; 
            }

            TeamLeads = File.ReadAllLines(filePath)
                            .Skip(1) // Пропускаем заголовок
                            .Select(line => line.Split(';'))
                            .Where(parts => parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[1]))
                            .Select(parts =>
                            {
                                //Console.WriteLine($"Загружаем тимлида: Id={parts[0]}, Name={parts[1]}");
                                return new TeamLead(int.Parse(parts[0]), parts[1], new List<string>()); // Создаем тимлида с пустым вишлистом
                            })
                            .ToList();
        }

        private void LoadJuniors(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл не найден: {filePath}");
                return; // Возвращаем пустой список, если файл не найден
            }

            Juniors = File.ReadAllLines(filePath)
                        .Skip(1) // Пропускаем заголовок
                        .Select(line => line.Split(';'))
                        .Where(parts => parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[1]))
                        .Select(parts =>
                        {
                            //Console.WriteLine($"Загружаем джуна: Id={parts[0]}, Name={parts[1]}");
                            return new Junior(int.Parse(parts[0]), parts[1], new List<string>()); // Создаем джуна с пустым вишлистом
                        })
                        .ToList();
        }

        private void GenerateWishlists()
        {
            var allTeamLeadNames = TeamLeads.Select(t => t.Name).ToList();
            var allJuniorNames = Juniors.Select(j => j.Name).ToList();

            // Генерируем вишлисты для джунов
            foreach (var junior in Juniors)
            {
                junior.Wishlist = GenerateWishlist(allTeamLeadNames);
                //Console.WriteLine($"Джун {junior.Name} имеет wishlist: {string.Join(", ", junior.Wishlist)}\n");
            }

            // Генерируем вишлисты для тимлидов
            foreach (var teamLead in TeamLeads)
            {
                teamLead.Wishlist = GenerateWishlist(allJuniorNames);
                //Console.WriteLine($"Тимлид {teamLead.Name} имеет wishlist: {string.Join(", ", teamLead.Wishlist)}\n");
            }
        }

        private List<string> GenerateWishlist(List<string> names)
        {
            // Перемешиваем имена и возвращаем список
            return names.OrderBy(x => random.Next()).ToList();
        }   
        
        private List<(Junior, TeamLead)> BuildPairs()
        {
            var pairs = new List<(Junior, TeamLead)>();

            // Пройдемся по каждому джуну и найдем для него подходящего тимлида
            foreach (var junior in Juniors)
            {
                foreach (var preferredTeamLeadName in junior.Wishlist)
                {
                    // Находим тимлида, который в приоритетах джуна
                    var preferredTeamLead = TeamLeads.FirstOrDefault(tl => tl.Name == preferredTeamLeadName);

                    if (preferredTeamLead != null && preferredTeamLead.Wishlist.Contains(junior.Name))
                    {
                        pairs.Add((junior, preferredTeamLead));
                        break; // Переходим к следующему джуну после нахождения пары
                    }
                }
            }

            return pairs;
        }


        public double ConductHackathon(List<(Junior, TeamLead)> pairs)
        {
            double totalHappiness = 0.0;

            foreach (var pair in pairs)
            {
                var junior = pair.Item1;
                var teamLead = pair.Item2;

                // Индексы счастья для джуна и тимлида
                int juniorHappiness = 20 - junior.Wishlist.IndexOf(teamLead.Name);
                int teamLeadHappiness = 20 - teamLead.Wishlist.IndexOf(junior.Name);

                // Проверяем, что значения счастья корректны
                if (juniorHappiness <= 0 || teamLeadHappiness <= 0)
                {
                    continue;
                }

                // Рассчитываем гармоническое среднее для пары
                double harmonicMean = 2.0 * juniorHappiness * teamLeadHappiness / (juniorHappiness + teamLeadHappiness);
                totalHappiness += harmonicMean;
            }
            //Console.WriteLine("\n");

            return totalHappiness / Juniors.Count;
        }

        public double ConductMultipleHackathons(int hackathonCount)
        {
            double totalHarmonicMean = 0.0;

            for (int i = 0; i < hackathonCount; i++)
            {
                // Генерируем вишлисты после полной загрузки данных
                GenerateWishlists();
                var pairs = BuildPairs();
                totalHarmonicMean += ConductHackathon(pairs);
            }

            return totalHarmonicMean / hackathonCount;
        }
    }
}
