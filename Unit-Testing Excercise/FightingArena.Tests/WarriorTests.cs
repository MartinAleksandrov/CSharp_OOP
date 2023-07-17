namespace FightingArena.Tests
{
    using NUnit.Compatibility;
    using NUnit.Framework;
    using System;
    using System.Reflection;

    [TestFixture]
    public class WarriorTests
    {
        private Warrior warrior;

        [SetUp]
        public void SetUp()
        {
            string waarriorName = "Aragorn";
            int warriorDamage = 50;
            int warriorHP = 45;

            warrior = new Warrior(waarriorName, warriorDamage, warriorHP);
        }

        [Test]
        public void WarriorConstructorMustWorkProperly()
        {
            Assert.AreEqual("Aragorn", warrior.Name);
            Assert.AreEqual(50, warrior.Damage);
            Assert.AreEqual(45, warrior.HP);
        }

        [TestCase(" ")]
        [TestCase("     ")]
        [TestCase(null)]
        public void NameShouldThrowExceptionWhenIsNullOrWhiteSpace(string name)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
             warrior = new Warrior(name, 51, 52));

            Assert.AreEqual("Name should not be empty or whitespace!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-62)]
        public void DamageShouldThrowExceptionWhenIs0_OrNegative(int damage)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
             warrior = new Warrior("Gimli", damage, 52));

            Assert.AreEqual("Damage value should be positive!", ex.Message);
        }

        [TestCase(-62)]
        [TestCase(-24)]
        public void HPShouldThrowExceptionWhenNegative(int HP)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
             warrior = new Warrior("Legolas", 62, HP));

            Assert.AreEqual("HP should not be negative!", ex.Message);
        }

        [Test]
        public void AttackMethodShouldDecreasethisHP_andSetWarriorHP_To0()
        {
            int excpectedHP = 5;
            Warrior orc = new Warrior("orc", 40, 40);
            warrior.Attack(orc);

            Assert.AreEqual(excpectedHP, warrior.HP);
            Assert.AreEqual(0, orc.HP);

        }

        [Test]
        public void AttackMethodShouldDecreasethisHPandWarriotHp()
        {

            int excpectedHP = 5;
            Warrior orcs = new Warrior("orcs", 40, 51);
            warrior.Attack(orcs);

            Assert.AreEqual(excpectedHP, warrior.HP);
            Assert.AreEqual(1, orcs.HP);
        }

        [Test]
        public void AttackMethodShouldThrowExceptionWhenHPIsBellowMinAttack()
        {
            warrior = new Warrior("troll", 30, 25);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                        warrior.Attack(warrior));

            Assert.AreEqual("Your HP is too low in order to attack other warriors!", ex.Message);
        }

        [Test]
        public void AttackMethodShouldThrowExceptionWhenInputHPIsBellowMinAttack()
        {
            //Taking value of private Const using reflection;
            Type type = typeof(Warrior);

            FieldInfo minAttackHP = type.GetField("MIN_ATTACK_HP", BindingFlags.NonPublic | BindingFlags.Static);
            object fieldValue = minAttackHP.GetValue(warrior);

            int intValue = Convert.ToInt32(fieldValue);
            
            Warrior goblin = new Warrior("goblin", 30, 15);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                        warrior.Attack(goblin));

            Assert.AreEqual($"Enemy HP must be greater than {intValue} in order to attack him!", ex.Message);
        }

        [Test]
        public void AttackMethodShouldThrowExceptionWhenthisHPIsBellowWarriorDamage()
        {
            Warrior nekromant = new Warrior("troll", 300, 55);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
                        warrior.Attack(nekromant));

            Assert.AreEqual("You are trying to attack too strong enemy", ex.Message);
        }
    }
}