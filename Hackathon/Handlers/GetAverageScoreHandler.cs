using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hackathon{
    public class GetAverageScoreHandler : IRequestHandler<GetAverageScoreRequest, double>
    {
        private readonly HackathonContext _context;

        public GetAverageScoreHandler(HackathonContext context)
        {
            _context = context;
        }

        public async Task<double> Handle(GetAverageScoreRequest request, CancellationToken cancellationToken)
        {
            var scores = await _context.Hackathons.Select(h => h.Score).ToListAsync(cancellationToken);
            return scores.Any() ? scores.Average() : 0.0;
        }
    }
}
