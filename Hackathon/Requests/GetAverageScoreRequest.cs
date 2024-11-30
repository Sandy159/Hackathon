using MediatR;

public record GetAverageScoreRequest : IRequest<double>;