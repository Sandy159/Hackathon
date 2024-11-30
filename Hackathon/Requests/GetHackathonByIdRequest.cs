using Hackathon;
using MediatR;

public record GetHackathonByIdRequest(int Id) : IRequest<Compition>;