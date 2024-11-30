using System.Collections.Generic;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon
{
    public record TeamLead(int id, string name) : Employee(id, name);
}
