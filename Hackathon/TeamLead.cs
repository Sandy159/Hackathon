using System.Collections.Generic;

namespace Hackathon
{
    // Класс для тимлидов
    public class TeamLead : Participant
    {
        public TeamLead(int id, string name, List<string> wishlist) : base(id, name, wishlist) { }
    }
}
