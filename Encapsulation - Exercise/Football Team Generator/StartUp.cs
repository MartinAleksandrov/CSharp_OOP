using System.Reflection;

namespace FootballTeamGenerator;

public class StartUp
{

    public static void Main()
    {
        List<Team> teams = new List<Team>();

        string input = string.Empty;
        while ((input = Console.ReadLine()) != "END")
        {
            try
            {
                string[] inputArray = input.Split(";");

                string command = inputArray[0];

                switch (command)
                {
                    case "Team":
                        AddTeam(inputArray[1], teams);
                        break;
                    case "Add":
                        int endurance = int.Parse(inputArray[3]);
                        int sprint = int.Parse(inputArray[4]);
                        int dribble = int.Parse(inputArray[5]);
                        int passing = int.Parse(inputArray[6]);
                        int shooting = int.Parse(inputArray[7]);
                        AddPlayer(inputArray[1], inputArray[2], endurance, sprint, dribble, passing, shooting, teams);
                        break;
                    case "Remove":
                        RemovePlayer(inputArray[1], inputArray[2], teams);
                        break;
                    case "Rating":
                        Rating(inputArray[1], teams);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
    }

    public static void AddTeam(string teamName, List<Team> teams)
    {
        teams.Add(new Team(teamName));
    }

    public static void AddPlayer(string teamName, string playerName, int endurance, int sprint, int dribble, int passing, int shooting, List<Team> teams)
    {
        Team team = teams.FirstOrDefault(t => t.TeamName == teamName);
        if (team == null)
        {
            throw new ArgumentException($"Team {teamName} does not exist.");
        }
        Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);
        team.AddPlayer(player);
    }

    public static void RemovePlayer(string teamName, string playerName, List<Team> teams)
    {
        Team team = teams.FirstOrDefault(t => t.TeamName == teamName);
        if (team == null)
        {
            throw new ArgumentException($"Team {teamName} does not exist.");
        }
        team.RemovePlayer(playerName);

    }

    public static void Rating(string teamName, List<Team> teams)
    {
        Team team = teams.FirstOrDefault(t => t.TeamName == teamName);
        if (team == null)
        {
            throw new ArgumentException($"Team {teamName} does not exist.");
        }
        Console.WriteLine($"{teamName} - {Math.Round(team.Rating)}");
    }
}