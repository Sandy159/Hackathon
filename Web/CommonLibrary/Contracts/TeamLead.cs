using System.Collections.Generic;

namespace CommonLibrary.Contracts
{
    public record TeamLead(int id, string name) : Employee(id, name);
}
