using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Xunit;
using Hackathon;
using Nsu.HackathonProblem.Contracts;
using System.Linq;

namespace Hackathon.Tests
{
    public class DatabaseTest : IAsyncLifetime
    {
        private PostgreSqlContainer _postgresContainer;
        private DbContextOptions<HackathonContext> _dbContextOptions;

        private HackathonContext CreateNewContext() => new HackathonContext(_dbContextOptions);

        public DatabaseTest()
        {
            _postgresContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase("testdb")
                .WithUsername("testuser")
                .WithPassword("testpassword")
                .Build();
        }

        public async Task InitializeAsync()
        {
            Console.WriteLine("Starting PostgreSqlContainer...");
            await _postgresContainer.StartAsync();
            Console.WriteLine("PostgreSqlContainer started.");

            _dbContextOptions = new DbContextOptionsBuilder<HackathonContext>()
                .UseNpgsql(_postgresContainer.GetConnectionString())
                .Options;

            using var context = CreateNewContext();
            Console.WriteLine("Ensuring database is created...");
            await context.Database.EnsureCreatedAsync();
            Console.WriteLine("Database is ready.");
        }

        public async Task DisposeAsync()
        {
            await _postgresContainer.DisposeAsync();
        }

        [Fact]
        public async Task Test_SaveHackathonToDatabase()
        {
            // Arrange
            var hackathon = new CompitionDto
            {
                Id = 1,
                Score = 95.0
            };

            using (var context = CreateNewContext())
            {
                context.Hackathons.Add(hackathon);
                await context.SaveChangesAsync();
            }

            // Act
            using (var verifyContext = CreateNewContext())
            {
                var handler = new GetHackathonByIdHandler(verifyContext);
                var result = await handler.Handle(new GetHackathonByIdRequest(1), CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(95.0, result?.Score);
            }
        }

        [Fact]
        public async Task Test_ReadHackathonFromDatabase()
        {
            // Arrange
            var hackathon = new CompitionDto
            {
                Id = 2,
                Score = 88.0,
                Teams = new List<TeamDto>
                {
                    new TeamDto
                    {
                        TeamLead = new EmployeeDto{Id = 1, Name = "TeamLead1", Role = "TeamLead"}, 
                        Junior = new EmployeeDto{Id = 2, Name = "Junior2", Role = "Junior"} 
                    },
                    new TeamDto
                    {
                        TeamLead = new EmployeeDto{Id = 2, Name = "TeamLead2", Role = "TeamLead"}, 
                        Junior = new EmployeeDto{Id = 1, Name = "Junior1", Role = "Junior"} 
                    }
                }
            };
            
            using (var context = CreateNewContext())
            {
                context.Hackathons.Add(hackathon);
                await context.SaveChangesAsync();
            }

            // Act
            using (var verifyContext = CreateNewContext())
            {
                var handler = new GetHackathonByIdHandler(verifyContext);
                var result = await handler.Handle(new GetHackathonByIdRequest(2), CancellationToken.None);

                // Assert
                Assert.NotNull(result); 
                Assert.Equal(88.0, result?.Score);  
                Assert.Equal(2, result?.Id); 

                Assert.NotNull(result?.Teams);  
                Assert.Equal(2, result?.Teams.Count());  

                var firstTeam = result?.Teams.FirstOrDefault();
                Assert.NotNull(firstTeam);
                Assert.Equal(1, firstTeam?.TeamLead?.Id);  
                Assert.Equal(2, firstTeam?.Junior?.Id);  

                var secondTeam = result?.Teams.Skip(1).FirstOrDefault();
                Assert.NotNull(secondTeam);
                Assert.Equal(2, secondTeam?.TeamLead?.Id); 
                Assert.Equal(1, secondTeam?.Junior?.Id);
            }
        }

        [Fact]
        public async Task Test_CalculateHarmonicMean()
        {
            // Arrange
            using var context = CreateNewContext();
            context.Hackathons.AddRange(
                new CompitionDto { Id = 1, Score = 80.0 },
                new CompitionDto { Id = 2, Score = 100.0 }
            );
            await context.SaveChangesAsync();

            using var verifyContext = CreateNewContext();
            var handler = new GetAverageScoreHandler(verifyContext);

            // Act
            var averageScore = await handler.Handle(new GetAverageScoreRequest(), default);

            // Assert
            Assert.Equal(90.0, averageScore); // Проверяем правильность расчета среднего
        }
    }
}
