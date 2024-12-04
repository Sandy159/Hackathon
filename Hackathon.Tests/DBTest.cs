using Hackathon;
using Microsoft.EntityFrameworkCore;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon.Tests;
public class HackathonTests : IClassFixture<TestFixture>, IDisposable
{
    private readonly HackathonContext _context;

    public HackathonTests(TestFixture fixture)
    {
        _context = fixture.Context;
    }

    public void Cleanup()
    {
        _context.Hackathons.RemoveRange(_context.Hackathons);
        _context.SaveChanges();
    }


    public void Dispose()
    {
        Cleanup();
    }

    [Fact]
    public async Task Should_Save_Hackathon_To_Database()
    {
        // Arrange
        var hackathon = new Compition
        {
            Score = 85.0,
            Wishlists = new List<Wishlist>(),
            Teams = new List<Team>()
        };

        var hackathonDto = CompitionMapper.ToEntity(hackathon, new List<Employee>());

        // Act
        _context.Hackathons.Add(hackathonDto);
        await _context.SaveChangesAsync();

        // Assert
        var savedHackathon = await _context.Hackathons.FirstOrDefaultAsync();
        Assert.NotNull(savedHackathon);
        Assert.Equal(85.0, savedHackathon.Score);
    }

    [Fact]
    public async Task Should_Read_Hackathon_From_Database()
    {
        // Arrange
        var hackathonDto = new CompitionDto
        {
            Score = 92.5,
            Wishlists = new List<WishlistDto>(),
            Teams = new List<TeamDto>()
        };
        _context.Hackathons.Add(hackathonDto);
        await _context.SaveChangesAsync();

        // Act
        var savedHackathon = await _context.Hackathons.FirstOrDefaultAsync();

        // Assert
        Assert.NotNull(savedHackathon);
        Assert.Equal(92.5, savedHackathon.Score);
    }

    [Fact]
    public async Task Should_Calculate_Average_Harmonic_And_Save()
    {
        // Arrange
        var hackathon1 = new CompitionDto { Score = 10, 
                                Wishlists = new List<WishlistDto>(),
                                Teams = new List<TeamDto>()};
        var hackathon2 = new CompitionDto { Score = 20, 
                                Wishlists = new List<WishlistDto>(),
                                Teams = new List<TeamDto>()};
        var hackathon3 = new CompitionDto { Score = 30, 
                                Wishlists = new List<WishlistDto>(),
                                Teams = new List<TeamDto>()};
        _context.Hackathons.AddRange(hackathon1, hackathon2, hackathon3);
        await _context.SaveChangesAsync();

        // Act
        var scores = await _context.Hackathons.Select(h => h.Score).ToListAsync();
        var harmonicMean = HarmonicMean(scores);

        // Assert
        Assert.Equal(0.06, harmonicMean, 2);
    }

    private double HarmonicMean(List<double> scores)
    {
        double sumOfInverses = scores.Sum(s => 1 / s);
        return sumOfInverses/scores.Count;
    }
}
