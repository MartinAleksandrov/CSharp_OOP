using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FootballTeamGenerator;

public class Team
{

    private string teamName;
    private readonly List<Player> playerList;

    public Team(string teamName)
    {
        TeamName = teamName;
        playerList = new List<Player>();
    }

    public string TeamName
    {
        get => teamName;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("A name should not be empty.");
            }
            teamName = value;
        }
    }

    public double Rating
    {
        get 
        {
            if (playerList.Any())
            {
                return playerList.Average(p => p.Stats());
            }
            return 0;
        }
    }

    public void AddPlayer(Player player) => playerList.Add(player);

    public void RemovePlayer(string playerName)
    {
        Player player = playerList.FirstOrDefault(p => p.Name == playerName);

        if (player==null)
        {
            throw new ArgumentException($"Player {playerName} is not in {TeamName} team.");
        }
        playerList.Remove(player);
    }
}
