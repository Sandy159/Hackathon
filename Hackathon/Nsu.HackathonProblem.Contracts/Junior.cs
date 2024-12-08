using System.Collections.Generic;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon
{
    public record Junior(int id, string name) : Employee(id, name);
}
