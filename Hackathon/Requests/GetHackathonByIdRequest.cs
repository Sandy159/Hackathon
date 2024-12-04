using Hackathon;
using MediatR;

public class GetHackathonByIdRequest : IRequest<Compition?>
{
    public int Id { get; }
    public GetHackathonByIdRequest(int id) => Id = id;
}