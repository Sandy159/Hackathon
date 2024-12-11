using System.Collections.Generic;

namespace CommonLibrary.Contracts
{
    public record Junior(int id, string name) : Employee(id, name);
}
