using NUnit.Framework;
using System;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            private Planet planet;
            private Weapon weapon;

            [Test]
            public void ConstuctorShouldWork()
            {
                planet = new Planet("PlanetX", 10);

                Assert.AreEqual("PlanetX", planet.Name);
                Assert.AreEqual(10, planet.Budget);
                Assert.AreEqual(0, planet.Weapons.Count);
            }

            [Test]
            public void PropertiesShouldThrowsExceptions()
            {
                Assert.Throws<ArgumentException>(() => planet = new Planet("", 10));
                Assert.Throws<ArgumentException>(() => planet = new Planet(null, 10));

                Assert.Throws<ArgumentException>(() => planet = new Planet("planet", -5));

                Assert.Throws<ArgumentException>(() => weapon = new Weapon("Weapon", -5, 5));
            }

            [Test]
            public void ProfitShoeldWork()
            {
                planet = new Planet("PlanetX", 10);

                planet.Profit(100);

                Assert.AreEqual(110, planet.Budget);
            }

            [Test]
            public void SpendFundsWhouldWork()
            {
                planet = new Planet("PlanetX", 10);

                planet.SpendFunds(5);

                Assert.AreEqual(5, planet.Budget);

                Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(15));
            }

            [Test]
            public void AddWeaponSholdWork()
            {
                planet = new Planet("PlanetX", 10);


                for (int i = 0; i < 10; i++)
                {
                    planet.AddWeapon(new Weapon($"MyWeapon{i}",12,5));
                }

                Assert.AreEqual(10, planet.Weapons.Count);

                Assert.Throws<InvalidOperationException>(() => planet.AddWeapon(new Weapon($"MyWeapon0", 12, 5)));
            }

            [Test]
            public void RemoveMethodWhouldWork()
            {
                planet = new Planet("PlanetX", 10);

                planet.AddWeapon(new Weapon("MyWeapon1", 12, 5));
                planet.AddWeapon(new Weapon("MyWeapon2", 12, 5));
                planet.AddWeapon(new Weapon("MyWeapon3", 12, 5));

                Assert.AreEqual(3, planet.Weapons.Count);

                planet.RemoveWeapon("MyWeapon1");
                Assert.AreEqual(2, planet.Weapons.Count);

                planet.RemoveWeapon("MyWeapon0");
                Assert.AreEqual(2, planet.Weapons.Count);

            }

            [Test]
            public void UpgradeWeaponShouldWork()
            {
                planet = new Planet("PlanetX", 10);
                Weapon weapon1 = new Weapon("MyWeapon0", 12, 5);
                weapon = new Weapon("MyWeapon1", 12, 5);

                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon1);

                Assert.AreEqual(5, weapon.DestructionLevel);

                planet.UpgradeWeapon("MyWeapon1");

                Assert.AreEqual(6, weapon.DestructionLevel);
                Assert.IsFalse(weapon.IsNuclear);


                planet.UpgradeWeapon("MyWeapon1");
                planet.UpgradeWeapon("MyWeapon1");
                planet.UpgradeWeapon("MyWeapon1");
                planet.UpgradeWeapon("MyWeapon1");

                Assert.AreEqual(10, weapon.DestructionLevel);
                Assert.IsTrue(weapon.IsNuclear);

                Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon("MyWeap"));

                Assert.AreEqual(15,planet.MilitaryPowerRatio);

            }

            [Test]
            public void DestructOpponentShouldWork()
            {
                Planet opponent = new Planet("PlanetX", 5);
                planet = new Planet("PlanetX", 10);

                Weapon weapon1 = new Weapon("MyWeapon0", 12, 5);
                weapon = new Weapon("MyWeapon1", 12, 5);

                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon1);

                opponent.AddWeapon(weapon);

               string message = planet.DestructOpponent(opponent);

                Assert.AreEqual($"{opponent.Name} is destructed!",message);

                Assert.Throws<InvalidOperationException>(() => opponent.DestructOpponent(planet));

            }
        }
    }
}
