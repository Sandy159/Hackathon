using Hackathon;
using MediatR;
using Nsu.HackathonProblem.Contracts;
using Microsoft.Extensions.Options;

namespace Hackathon
{  
    public class RunHackathonHandler : IRequestHandler<RunHackathonRequest, double>
    {
        private readonly HackathonContext _context;
        private HRManager _hrManager;
        private HRDirector _hrDirector;
        private IOptions<ConstantOptions> _constants;

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

    var employeeDtos = juniors
        .Select(junior => new EmployeeDto
        {
            Id = junior.Id,
            Name = junior.Name,
            Role = "Junior" 
        })
        .Concat(teamleads.Select(teamlead => new EmployeeDto
        {
            Id = teamlead.Id,
            Name = teamlead.Name,
            Role = "TeamLead" 
        }))
        .ToList();

    var hackathon = new Compition();

    var score = hackathon.RunHackathon(juniors, teamleads, _hrManager, _hrDirector);
    hackathon.Score = score;

     var employees = employeeDtos
        .Select(dto => CompitionMapper.ToDomain(dto)) 
        .ToList(); 

    var hackathonDto = CompitionMapper.ToEntity(hackathon, employees);

    var employeeEntities = employees;
    _context.Employees.AddRange(employeeDtos); 

    _context.Teams.AddRange(hackathonDto.Teams); 
    _context.Wishlists.AddRange(hackathonDto.Wishlists); 
    _context.Hackathons.Add(hackathonDto); 

    await _context.SaveChangesAsync(cancellationToken);

    return score;
}

    }
}