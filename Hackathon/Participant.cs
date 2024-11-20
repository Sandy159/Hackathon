using System.Collections.Generic;

namespace Hackathon
{
    // Базовый класс для участников (джунов и тимлидов)
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Participant(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
