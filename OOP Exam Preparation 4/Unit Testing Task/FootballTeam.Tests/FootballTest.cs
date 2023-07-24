using NUnit.Framework;
using System;
using System.Xml.Linq;

namespace FootballTeam.Tests
{
    public class FootballTest
    {
        private FootballPlayer player;

        [SetUp]
        public void Setup()
        {
            string name = "Messi";
            int playerNum = 11;
            string position = "Goalkeeper";

            player = new FootballPlayer(name, playerNum,position);
        }

        [Test]
        public void ConstructorShouldWork()
        {
            Assert.That(player.Name, Is.EqualTo("Messi"));
            Assert.That(player.PlayerNumber, Is.EqualTo(11));
            Assert.That(player.Position, Is.EqualTo("Goalkeeper"));
            Assert.That(player.ScoredGoals, Is.EqualTo(0));

            player = new FootballPlayer("az", 6, "Midfielder");

            Assert.That(player.Name, Is.EqualTo("az"));
            Assert.That(player.PlayerNumber, Is.EqualTo(6));
            Assert.That(player.Position, Is.EqualTo("Midfielder"));

        }

        [TestCase("")]
        [TestCase(null)]
        public void NameShouldThrowxceptionWhenIsNullOrWhiteSpace(string input)
        {
            int playerNum = 11;
            string position = "Attacker";
            
            ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            player = new FootballPlayer(input, playerNum, position));

            Assert.That(exception.Message, Is.EqualTo("Name cannot be null or empty!"));
         
        }


        [TestCase(0)]
        [TestCase(-23)]
        [TestCase(65)]
        public void PlayerNumberShouldThrowxceptionWhenIsNullOrWhiteSpace(int input)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
           player = new FootballPlayer("Messi", input, "Attack"));

            Assert.That(exception.Message, Is.EqualTo("Player number must be in range [1,21]"));
        }


        [TestCase("alabala")]
        [TestCase("portokala")]
        public void PositionShouldThrowxceptionWhenIsNullOrWhiteSpace(string input)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            player = new FootballPlayer("Messi", 9, input));

            Assert.That(exception.Message, Is.EqualTo("Invalid Position"));
        }


        [Test]
        public void ScoreShouldWork()
        {
            player.Score();
            player.Score();
            player.Score();

            Assert.That(player.ScoredGoals, Is.EqualTo(3));
        }

    }
}