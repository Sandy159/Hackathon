using CommonLibrary.Contracts;
using HRDirectorApp.Database;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace HRDirectorApp.Service;

public class HRDirectorService 
{
    private readonly HRDirectorDbContext _dbContext;
    private readonly ILogger<HRDirectorService> _logger;

    public HRDirectorService(HRDirectorDbContext dbcontext, ILogger<HRDirectorService> logger)
    {
        _logger = logger;
        _dbContext = dbcontext;
    }

    public async Task<double> EvaluateHarmonyAsync(PreferencesAndTeamsResponse preferencesAndTeamsResponse)
    {
        var teams = preferencesAndTeamsResponse.Teams;
        var juniorPreferences = preferencesAndTeamsResponse.JuniorPreferences;
        var teamLeadPreferences = preferencesAndTeamsResponse.TeamLeadPreferences;

        double sumOfReciprocals = 0;
        int n = teams.Count() * 2;

        foreach (var team in teams)
        {
            var teamLeadPreference = teamLeadPreferences.FirstOrDefault(p => p.EmployeeId == team.TeamLeadId);
            var juniorPreference = juniorPreferences.FirstOrDefault(p => p.EmployeeId == team.JuniorId);

            var teamLeadIndex = teamLeadPreference != null
                ? Array.IndexOf(teamLeadPreference.Preferences, team.JuniorId) + 1
                : -1;
            var juniorIndex = juniorPreference != null
                ? Array.IndexOf(juniorPreference.Preferences, team.TeamLeadId) + 1
                : -1;

            if (teamLeadIndex > 0) sumOfReciprocals += 1.0 / teamLeadIndex;
            if (juniorIndex > 0) sumOfReciprocals += 1.0 / juniorIndex;
        }

        double averageHarmony = n / sumOfReciprocals;

        await SaveHarmonyAsync(preferencesAndTeamsResponse.HackathonId, averageHarmony);

        return averageHarmony;
    }

    public async Task SaveHarmonyAsync(int id, double harmony)
    {
        var harmonyRecord = new HarmonyRecord {Id = id, HarmonyScore = harmony};
        _dbContext.HarmonyRecords.Add(harmonyRecord);
        await _dbContext.SaveChangesAsync();
    }
}