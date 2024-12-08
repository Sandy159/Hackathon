using Hackathon;
using MediatR;
using Nsu.HackathonProblem.Contracts;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hackathon
{
    public class RunHackathonHandler : IRequestHandler<RunHackathonRequest, double>
    {
        private readonly HackathonContext _context;
        private readonly HRManager _hrManager;
        private readonly HRDirector _hrDirector;
        private readonly IOptions<ConstantOptions> _constants;

        public RunHackathonHandler(HackathonContext context, HRManager hrManager, HRDirector hrDirector, IOptions<ConstantOptions> constants)
        {
            _context = context;
            _hrManager = hrManager;
            _hrDirector = hrDirector;
            _constants = constants;
        }

        public async Task<double> Handle(RunHackathonRequest request, CancellationToken cancellationToken)
        {
            var constants = _constants.Value;

            var juniors = DataLoader.LoadEmployees(constants.JuniorsFilePath);
            var teamleads = DataLoader.LoadEmployees(constants.TeamLeadsFilePath);

            var employeeDtos = MapEmployeesToDto(juniors, teamleads);

            var hackathon = new Compition();
            var score = hackathon.RunHackathon(juniors, teamleads, _hrManager, _hrDirector);
            hackathon.Score = score;

            var employees = employeeDtos.Select(dto => CompitionMapper.ToDomain(dto)).ToList();
            var hackathonDto = CompitionMapper.ToEntity(hackathon, employees);

            await SaveToDatabase(employeeDtos, hackathonDto, cancellationToken);

            return score;
        }

        private List<EmployeeDto> MapEmployeesToDto(IEnumerable<Employee> juniors, IEnumerable<Employee> teamleads)
        {
            var juniorDtos = juniors.Select(junior => new EmployeeDto
            {
                Id = junior.Id,
                Name = junior.Name,
                Role = "Junior"
            });

            var teamleadDtos = teamleads.Select(teamlead => new EmployeeDto
            {
                Id = teamlead.Id,
                Name = teamlead.Name,
                Role = "TeamLead"
            });

            return juniorDtos.Concat(teamleadDtos).ToList();
        }

        private async Task SaveToDatabase(List<EmployeeDto> employeeDtos, CompitionDto hackathonDto, CancellationToken cancellationToken)
        {
            _context.Employees.AddRange(employeeDtos);
            _context.Teams.AddRange(hackathonDto.Teams);
            _context.Wishlists.AddRange(hackathonDto.Wishlists);
            _context.Hackathons.Add(hackathonDto);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
