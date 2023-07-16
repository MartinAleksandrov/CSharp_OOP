namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;
    using System.Reflection;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;

        [SetUp]
        public void SetUp()
        {
            string make = "Mazda";
            string model = "3";
            double fuelConsumption = 1;
            double fuelCapacity = 10;

            car = new Car(make, model, fuelConsumption, fuelCapacity);
        }

        [Test]
        public void DoesConstructorSetsParamsProperly()
        {

            string expectedMake = "Mazda";
            string expectedModel = "3";
            double expectedFuelConsumption = 1;
            double expectedFuelCapacity = 10;

            Assert.AreEqual(expectedMake, car.Make);
            Assert.AreEqual(expectedModel, car.Model);
            Assert.AreEqual(expectedFuelConsumption, car.FuelConsumption);
            Assert.AreEqual(expectedFuelCapacity, car.FuelCapacity);
        }

        [TestCase("")]
        [TestCase(null)]
        public void MakePropertyShouldThrowExceptionIfInputIsNullOrEmpty(string input)
        {

            string model = "2";
            double fuelConsumption = 1;
            double fuelCapacity = 10;

            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => car = new Car(input, model, fuelConsumption, fuelCapacity));

            Assert.AreEqual("Make cannot be null or empty!", ex.Message);

        }

        [TestCase("")]
        [TestCase(null)]
        public void ModelPropertyShouldThrowExceptionIfInputIsNullOrEmpty(string input)
        {

            string make = "BMW";
            double fuelConsumption = 1;
            double fuelCapacity = 10;

            ArgumentException ex = Assert.Throws<ArgumentException>(()

                => car = new Car(make, input, fuelConsumption, fuelCapacity));

            Assert.AreEqual("Model cannot be null or empty!", ex.Message);

        }

        [TestCase(0)]
        [TestCase(-528)]
        public void FuelConsumptionPropertyShouldThrowExceptionIfInputIs0OrNegative(int input)
        {

            string make = "BMW";
            string model = "e3";
            double fuelCapacity = 10;

            ArgumentException ex = Assert.Throws<ArgumentException>(()

                => car = new Car(make, model, input, fuelCapacity));

            Assert.AreEqual("Fuel consumption cannot be zero or negative!", ex.Message);

        }

        [TestCase(0)]
        [TestCase(-28)]
        public void FuelCapacityPropertyShouldThrowExceptionIfInputIs0OrNegative(int input)
        {

            string make = "BMW";
            string model = "e3";
            double fuelConsumption = 1;

            ArgumentException ex = Assert.Throws<ArgumentException>(()

                => car = new Car(make, model, fuelConsumption, input));

            Assert.AreEqual("Fuel capacity cannot be zero or negative!", ex.Message);

        }


        [Test]
        public void DoesFuelAmountWorkProperly()
        {
            double excpectedResult = 0;

            Assert.AreEqual(excpectedResult, car.FuelAmount);
        }

        [Test]
        public void DoesFuelAmountIncreaseWhenRefuel()
        {
            double fuelToRefuel = 10;

            car.Refuel(fuelToRefuel);
            double actualResult = car.FuelAmount;


            Assert.AreEqual(fuelToRefuel, actualResult);
        }

        [Test]
        public void DoesFuelAmountIsMoreTnahFuelCapacity()
        {
            double fuelToRefuel = 10.0;

            car.Refuel(15);
            double actualResult = car.FuelAmount;

            Assert.AreEqual(fuelToRefuel, actualResult);
        }

        [TestCase(0)]
        [TestCase(-52)]
        public void DoestRefuelMethodThrowExceptionWhenInputIs0_OrNegative(double input)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => car.Refuel(input));

            Assert.AreEqual("Fuel amount cannot be zero or negative!",ex.Message);
        }

        [Test]
        public void DriveMethodMustWorkCorrectky()
        {
            double carDistance = 9.9;

            car.Refuel(10);
            car.Drive(10);

            Assert.AreEqual(carDistance, car.FuelAmount);
        }

        [Test]
        public void DriveMethodMustThrowEceptionIfFuelNeedIsMoreThanFelAmount()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            car.Drive(2));

            Assert.AreEqual("You don't have enough fuel to drive!",ex.Message);
        }
    }
}