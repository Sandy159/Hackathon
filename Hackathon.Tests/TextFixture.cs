using Hackathon;
using Microsoft.EntityFrameworkCore;

public class TestFixture
{
    public HackathonContext Context { get; private set; }

    public TestFixture()
    {
        var options = new DbContextOptionsBuilder<HackathonContext>()
            .UseSqlite("DataSource=:memory:") // Использование In-Memory базы данных
            .Options;

        Context = new HackathonContext(options);
        Context.Database.OpenConnection();
        Context.Database.EnsureCreated();
    }
}
