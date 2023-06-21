using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator;

public class Player
{
    private const string exceptionName = "A name should not be empty.";

    private string playernName;
    private double endurance;
    private double sptint;
    private double dribble;
    private double passing;
    private double shooting;

    public Player(string name, double endurance, double sptint, double dribble, double passing, double shooting)
    {
        Name = name;
        Endurance = endurance;
        Sptint = sptint;
        Dribble = dribble;
        Passing = passing;
        Shooting = shooting;
    }

    public string Name
    {
        get => playernName;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(exceptionName);
            }
            playernName = value;
        }
    }
    public double Endurance
    {
        get => endurance;
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException($"Endurance should be between 0 and 100.");
            }
            endurance = value;
        }
    }
    public double Sptint
    {
        get => sptint;
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException($"Sprint should be between 0 and 100.");
            }
            sptint = value;
        }
    }
    public double Dribble
    {
        get => dribble;
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException($"Dribble should be between 0 and 100.");
            }
            dribble = value;
        }
    }
    public double Passing
    {
        get => passing;
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException($"Passing should be between 0 and 100.");
            }
            passing = value;
        }
    }
    public double Shooting
    {
        get => shooting;
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException($"Shooting should be between 0 and 100.");
            }
            shooting = value;
        }
    }

    public double Stats()
    {
        return (endurance + sptint + dribble + passing + shooting ) / 5.0;
    }


}