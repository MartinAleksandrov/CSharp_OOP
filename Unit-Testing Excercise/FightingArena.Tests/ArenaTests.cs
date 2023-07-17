namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;
        Warrior arenaWarrior;
        Warrior centurion;

        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
        }
        [Test]
        public void EnrrolMethodMustWorkProperly()
        {
            arenaWarrior = new Warrior("Gilgamesh", 45, 46);
            centurion = new Warrior("Centurion", 49, 50);

            arena.Enroll(arenaWarrior);
            arena.Enroll(centurion);

            int expectedResult = 2;

            Assert.AreEqual(expectedResult, arena.Count);
        }

        [Test]
        public void EnrrolMethodMustThrowExceptiomWhenWarriorAlreadyExist()
        {
            arenaWarrior = new Warrior("Gilgamesh", 45, 46);
            arena.Enroll(arenaWarrior);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
             arena.Enroll(new Warrior("Gilgamesh", 49, 50)));

            Assert.AreEqual("Warrior is already enrolled for the fights!", ex.Message);
        }

        [Test]
        public void FightMethodMustWorkCorrectky()
        {
            arenaWarrior = new Warrior("Gilgamesh", 70, 80);
            centurion = new Warrior("Cesar", 60, 90);

            arena.Enroll(arenaWarrior);
            arena.Enroll(centurion);

            int expectedDefenderHp = 20;
            int expectedAttackerHp = 20;

            arena.Fight(arenaWarrior.Name, centurion.Name);

            Assert.AreEqual(expectedAttackerHp, arenaWarrior.HP);
            Assert.AreEqual(expectedDefenderHp, centurion.HP);


        }

        [Test]
        public void FightMethodMustThrowExceptionWhenAttackerNotFound()
        {
            arenaWarrior = new Warrior("Gilgamesh", 70, 80);
            centurion = new Warrior("Cesar", 60, 90);
            arena.Enroll(centurion);


            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                        arena.Fight(arenaWarrior.Name, centurion.Name));

            Assert.AreEqual($"There is no fighter with name {arenaWarrior.Name} enrolled for the fights!", ex.Message);
        }

        [Test]
        public void FightMethodMustThrowExceptionWhenDefenderrNotFound()
        {
            arenaWarrior = new Warrior("Gilgamesh", 70, 80);
            centurion = new Warrior("Cesar", 60, 90);
            arena.Enroll(arenaWarrior);


            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                        arena.Fight(arenaWarrior.Name, centurion.Name));

            Assert.AreEqual($"There is no fighter with name {centurion.Name} enrolled for the fights!", ex.Message);
        }
    }
}
