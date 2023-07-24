using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace FootballTeam.Tests
{
    public class FootballTeamTest
    {

        private FootballTeam footballTeam;

        [SetUp]
        public void Setup()
        {
            string name = "Inter";
            int capacity = 16;

            footballTeam = new FootballTeam(name, capacity);
        }

        [Test]
        public void CreateFootballTeam_ValidParameters()
        {
            FootballTeam team = new FootballTeam("Real Madrid", 21);


            Assert.IsNotNull(team);
            Assert.That(team.Name, Is.EqualTo("Real Madrid"));
            Assert.That(team.Capacity, Is.EqualTo(21));


            Type t = team.Players.GetType();
            Type expectedType = typeof(List<FootballPlayer>);

            Assert.That(expectedType, Is.EqualTo(t));
        }
        [Test]
        public void ConstructorShouldWork()
        {
            Assert.That(footballTeam.Name, Is.EqualTo("Inter"));
            Assert.That(footballTeam.Capacity, Is.EqualTo(16));
            Assert.That(footballTeam.Players.Count, Is.EqualTo(0));
        }

        [Test]
        public void PropertiesShouldSetProperly()
        {
            footballTeam.Name = "Milan";
            footballTeam.Capacity = 19;

            Assert.That(footballTeam.Name, Is.EqualTo("Milan"));
            Assert.That(footballTeam.Capacity, Is.EqualTo(19));
        }

        [TestCase("")]
        [TestCase(null)]
        public void PropertiesShouldThrowsException(string input)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            footballTeam = new FootballTeam(input, 20));

            Assert.That(exception.Message, Is.EqualTo("Name cannot be null or empty!"));
        }

        [TestCase(8)]
        [TestCase(5)]
        public void PropertiesShouldThrowsExceptions(int input)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            footballTeam = new FootballTeam("Barca", input));

            Assert.That(exception.Message, Is.EqualTo("Capacity min value = 15"));
        }


        [Test]
        public void AddNewPlayerShouldAddNewPlayer()
        {
            FootballPlayer player = new FootballPlayer("NewMadafaka", 10, "Midfielder");
            FootballPlayer player2 = new FootballPlayer("NewMadafaka2", 9, "Forward");
            FootballPlayer player3 = new FootballPlayer("NewMadafaka3", 8, "Forward");

            footballTeam.AddNewPlayer(player);
            footballTeam.AddNewPlayer(player2);
            string result = footballTeam.AddNewPlayer(player3);

            Assert.That(result, Is.EqualTo($"Added player {player3.Name} in position {player3.Position} with number {player3.PlayerNumber}"));
            Assert.That(footballTeam.Players.Count, Is.EqualTo(3));
        }


        [Test]
        public void AddNewPlayerShouldNotAddNewPlayer()
        {
            string result = string.Empty;
            for (int i = 0; i < 19; i++)
            {
                result = footballTeam.AddNewPlayer(new FootballPlayer("NewMadafaka", 10, "Midfielder"));
            }

            Assert.That(result, Is.EqualTo("No more positions available!"));
            Assert.That(footballTeam.Players.Count, Is.EqualTo(16));
        }

        [Test]
        public void PickPlayerShouldReturnSearchedPlayer()
        {
            FootballPlayer player = new FootballPlayer("NewMadafaka", 11, "Midfielder");
            FootballPlayer player2 = new FootballPlayer("NewMadafaka2", 10, "Midfielder");

            footballTeam.AddNewPlayer(player);
            footballTeam.AddNewPlayer(player2);

            FootballPlayer myPlayer = footballTeam.PickPlayer("NewMadafaka2");

            Assert.That(myPlayer.Name, Is.EqualTo("NewMadafaka2"));
            Assert.That(myPlayer.PlayerNumber, Is.EqualTo(10));
            Assert.That(myPlayer.Position, Is.EqualTo("Midfielder"));


            FootballPlayer myPlayer2 = footballTeam.PickPlayer("NewMadafaka");

            Assert.That(myPlayer2.Name, Is.EqualTo("NewMadafaka"));
            Assert.That(myPlayer2.PlayerNumber, Is.EqualTo(11));
            Assert.That(myPlayer2.Position, Is.EqualTo("Midfielder"));

        }

        [Test]
        public void PlayerScoreShouldWork()
        {
            FootballPlayer player = new FootballPlayer("NewMadafaka", 11, "Midfielder");
            FootballPlayer player2 = new FootballPlayer("NewMadafaka2", 10, "Midfielder");

            player2.Score();
            player2.Score();


            footballTeam.AddNewPlayer(player);
            footballTeam.AddNewPlayer(player2);

            string result = footballTeam.PlayerScore(11);

            Assert.That(result, Is.EqualTo($"{player.Name} scored and now has {player.ScoredGoals} for this season!"));

            string result2 = footballTeam.PlayerScore(10);

            Assert.That(result2, Is.EqualTo($"{player2.Name} scored and now has {player2.ScoredGoals} for this season!"));


            NullReferenceException ex = Assert.Throws<NullReferenceException>(() => footballTeam.PlayerScore(8));

            Assert.That(ex.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }
    }
}
