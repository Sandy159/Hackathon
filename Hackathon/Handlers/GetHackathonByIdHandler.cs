using Hackathon;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class GetHackathonByIdHandler : IRequestHandler<GetHackathonByIdRequest, Compition?>
{
    private readonly HackathonContext _context;

    public GetHackathonByIdHandler(HackathonContext context)
    {
        _context = context;
    }

    public async Task<Compition?> Handle(GetHackathonByIdRequest request, CancellationToken cancellationToken)
    {
        var hackathonDto = await _context.Hackathons
            .Include(h => h.Wishlists)
                .ThenInclude(w => w.Employee)
            .Include(h => h.Teams)
                .ThenInclude(t => t.Junior)
            .Include(h => h.Teams)
                .ThenInclude(t => t.TeamLead)
            .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

        if (hackathonDto == null)
            return null;

        var employees = await _context.Employees.ToListAsync(cancellationToken);
        var hackathon = CompitionMapper.ToDomain(hackathonDto, employees.Select(CompitionMapper.ToDomain).ToList());

        return hackathon;
    }
}
