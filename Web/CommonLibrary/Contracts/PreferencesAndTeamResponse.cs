namespace CommonLibrary.Contracts;
public class PreferencesAndTeamsResponse
{
    public List<Team> Teams { get; set; }
    public List<PreferencesMessage> JuniorPreferences { get; set; }
    public List<PreferencesMessage> TeamLeadPreferences { get; set; }
}