using System.Collections.Generic;

namespace Hackathon
{
    // Базовый класс для участников (джунов и тимлидов)
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Wishlist wishlist { get; set; }

        public Participant(int id, string name)
        {
            Id = id;
            Name = name;
            wishlist = new Wishlist(id, Array.Empty<int>()); //пустой wishlist
        }

        public void GenerateWishlist(List<int> ids, Random random)
        {
            var randomizedIds = ids.OrderBy(x => random.Next()).ToArray(); // Перемешиваем и преобразуем в массив
            wishlist = new Wishlist(Id, randomizedIds); 
        }
    }
}
