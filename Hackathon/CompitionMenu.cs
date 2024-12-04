using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hackathon
{
    public class CompitionMenu
    {
        private readonly IMediator _mediator;
        private readonly HackathonContext _context;

        public CompitionMenu(IMediator mediator, HackathonContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task Run()
        {
            while (true)
            {
                Console.WriteLine("Enter command:");
                Console.WriteLine("1 - Run hackathon");
                Console.WriteLine("2 - Get average score for all hackathons");
                Console.WriteLine("3 - Get hackathon by ID");
                Console.WriteLine("0 - Exit");
                var input = Console.ReadLine();
                if (input == "0") break;

                switch (input)
                {
                    case "1":
                        var harmonicMean = await _mediator.Send(new RunHackathonRequest());
                        Console.WriteLine($"Harmonic mean for current hackathon: {harmonicMean}");
                        break;

                    case "2":
                        var avgScore = await _mediator.Send(new GetAverageScoreRequest());
                        Console.WriteLine($"Average score for all hackathons: {avgScore}");
                        break;

                    case "3":
                        Console.WriteLine("Enter hackathon ID:");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            var hackathon = await _mediator.Send(new GetHackathonByIdRequest(id));
                            if (hackathon != null)
                            {
                                Console.WriteLine("----------------------------------");
                                Console.WriteLine($"Hackathon: {hackathon.Id}, score: {hackathon.Score}");

                                var employees = await _context.Employees.ToListAsync();

                                Console.WriteLine("Participants:");
                                foreach (var wishlist in hackathon.Wishlists)
                                {
                                    var employee = employees.FirstOrDefault(e => e.Id == wishlist.EmployeeId);
                                    Console.WriteLine($"- {employee.Id} {employee?.Name ?? "Unknown"}");
                                }

                                Console.WriteLine("Teams:");
                                foreach (var team in hackathon.Teams)
                                {
                                    var teamLead = employees.FirstOrDefault(e => e.Id == team.TeamLead.Id);
                                    var junior = employees.FirstOrDefault(e => e.Id == team.Junior.Id);
                                    Console.WriteLine($"- Team Lead: {teamLead?.Name ?? "Unknown"}, Junior: {junior?.Name ?? "Unknown"}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Hackathon is not found");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID format.");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid command. Try again.");
                        break;
                }
            }
        }
    }
}
