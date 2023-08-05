using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {
        private CoffeeMat coffeeMat;

        [SetUp]
        public void Setup()
        {
            int waterCapacity = 20;
            int buttonsCount = 10;

            coffeeMat = new CoffeeMat(waterCapacity, buttonsCount);
        }

        [Test]
        public void ConstructorShouldWorkCorrect()
        {
            Assert.That(coffeeMat.WaterCapacity, Is.EqualTo(20));
            Assert.That(coffeeMat.ButtonsCount, Is.EqualTo(10));
            Assert.That(coffeeMat.Income, Is.EqualTo(0));
        }

        [Test]
        public void FillWaterTankShouldWorkCorrect()
        {
            CoffeeMat newCoffeMat = new CoffeeMat(150,15);

            string excpectedResult = coffeeMat.FillWaterTank();

            Assert.That(excpectedResult, Is.EqualTo("Water tank is filled with 20ml"));
            Assert.That(newCoffeMat.FillWaterTank(), Is.EqualTo("Water tank is filled with 150ml"));

        }


        [Test]
        public void FillWaterTankShouldNotFillWhenIsFull()
        {
            coffeeMat.FillWaterTank();

            string excpectedResult = coffeeMat.FillWaterTank();

            Assert.That(excpectedResult, Is.EqualTo("Water tank is already full!"));
        }

        [Test]
        public void AddDrinkShouldWorkCorrectAndReturnTrue()
        {
            bool result = coffeeMat.AddDrink("Cola", 2);

            Assert.IsTrue(result);
            Assert.IsTrue(coffeeMat.AddDrink("Cola2", 2));

        }
        [Test]
        public void AddDrinkShouldWorkReturnFalse()
        {
            coffeeMat.AddDrink("Cola", 2);

            Assert.IsFalse(coffeeMat.AddDrink("Cola", 5));


            coffeeMat.AddDrink("Pepsi", 3);
            coffeeMat.AddDrink("Fanta", 7);
            coffeeMat.AddDrink("Sprait", 5);
            coffeeMat.AddDrink("Lemonade", 6);
            coffeeMat.AddDrink("Bear", 2);
            coffeeMat.AddDrink("Rakiq", 8);
            coffeeMat.AddDrink("Vodka", 5);
            coffeeMat.AddDrink("Tonik", 2);
            coffeeMat.AddDrink("TonikSled", 9);

            bool result = coffeeMat.AddDrink("Cola2", 2);

            Assert.IsFalse(result);
        }

        [Test]
        public void Propgetters()
        {
            int buttCount = coffeeMat.ButtonsCount;
            int waterCapacity = coffeeMat.WaterCapacity;
            double income = coffeeMat.Income;

            Assert.That(buttCount, Is.EqualTo(10));
            Assert.That(waterCapacity, Is.EqualTo(20));
            Assert.That(income, Is.EqualTo(0));

        }

        [Test]
        public void BuyDrinkIsOutOfWater()
        {
            string result = coffeeMat.BuyDrink("Cola");
            Assert.That(result, Is.EqualTo("CoffeeMat is out of water!"));
        }

        [Test]
        public void BuyDrinkReturnYourBill()
        {
            CoffeeMat newCoffeMat = new CoffeeMat(100,10);

            newCoffeMat.FillWaterTank();

            newCoffeMat.AddDrink("Pepsi", 3);
            newCoffeMat.AddDrink("Fanta", 7);
            newCoffeMat.AddDrink("Sprait", 5);


            string result = newCoffeMat.BuyDrink("Pepsi");

            Assert.That(result, Is.EqualTo("Your bill is 3.00$"));

            Assert.That(newCoffeMat.Income, Is.EqualTo(3));


            string result2 = coffeeMat.BuyDrink("Pepsi");

            Assert.That(result2, Is.EqualTo("CoffeeMat is out of water!"));

        }

        [Test]
        public void BuyDrinkReturnNotAvailableName()
        {
            CoffeeMat newCoffeMat = new CoffeeMat(100, 10);

            newCoffeMat.FillWaterTank();

            newCoffeMat.AddDrink("Pepsi", 3);
            newCoffeMat.AddDrink("Fanta", 7);
            newCoffeMat.AddDrink("Sprait", 5);


            string result = newCoffeMat.BuyDrink("Cola");

            Assert.That(result, Is.EqualTo("Cola is not available!"));
            Assert.That(newCoffeMat.Income, Is.EqualTo(0));

        }

        [Test]
        public void CollectincomeShouldWork()
        {
            CoffeeMat newCoffeMat = new CoffeeMat(300, 10);

            newCoffeMat.FillWaterTank();

            newCoffeMat.AddDrink("Pepsi", 3);
            newCoffeMat.AddDrink("Fanta", 7);
            newCoffeMat.AddDrink("Sprait", 5);
            Assert.That(newCoffeMat.Income, Is.EqualTo(0));


            newCoffeMat.BuyDrink("Pepsi");
            newCoffeMat.BuyDrink("Fanta");
            newCoffeMat.BuyDrink("Sprait");

            double result = newCoffeMat.CollectIncome();

            Assert.That(result, Is.EqualTo(15));
            Assert.That(newCoffeMat.Income, Is.EqualTo(0));

        }
    }
}