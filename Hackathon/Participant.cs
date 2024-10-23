using System.Collections.Generic;

namespace Hackathon
{
    // Базовый класс для участников (джунов и тимлидов)
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Wishlist { get; set; }

        public Participant(int id, string name, List<string> wishlist)
        {
            Id = id;
            Name = name;
            Wishlist = wishlist;
        }
    }
}
