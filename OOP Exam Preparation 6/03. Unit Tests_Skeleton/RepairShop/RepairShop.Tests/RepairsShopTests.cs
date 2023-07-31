using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            private Garage garage;
            [SetUp]
            public void SetUp()
            {
                garage = new Garage("MyGarage",5);
            }

            [Test]
            public void ConstructorWork()
            {
                Assert.AreEqual("MyGarage" , garage.Name);
                Assert.AreEqual(5, garage.MechanicsAvailable);
                Assert.AreEqual(0,garage.CarsInGarage);
            }

            [Test]
            public void PropertiesMustThrowsExceptions()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    garage = new Garage(null, 5);
                });
                Assert.Throws<ArgumentException>(() =>
                {
                    garage = new Garage("MyGarsage", -5);
                }); 
            }

            [Test]
            public void AddCarShouldWork()
            {
                garage.AddCar(new Car("BMW",12));
                garage.AddCar(new Car("BMW1", 12));
                garage.AddCar(new Car("BMW2", 12));
                garage.AddCar(new Car("BMW3", 12));
                garage.AddCar(new Car("BMW5", 12));


                Assert.AreEqual(5, garage.CarsInGarage);


                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.AddCar(new Car("BMW5", 12));
                });
            }

            [Test]
            public void FixCarShouldWork()
            {
                Car car = new Car("BMW", 12);
                garage.AddCar(car);
                garage.AddCar(new Car("BMW1", 12));
                garage.AddCar(new Car("BMW2", 12));
                garage.AddCar(new Car("BMW3", 12));

                Car fixedCar = garage.FixCar("BMW");

                Assert.AreEqual(0,car.NumberOfIssues);
                Assert.AreEqual(fixedCar.IsFixed,car.IsFixed);
                Assert.AreEqual(fixedCar.CarModel, car.CarModel);
            }

            public void FixCarShouldNotWork()
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    Car fixedCar = garage.FixCar("BMW213");
                });
            }


            public void RemoveFixedCar()
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    Car fixedCar = garage.FixCar("BMW213");
                });

                Car car = new Car("BMW", 12);
                Car car1 = new Car("BMW1", 12);
                Car car2 = new Car("BMW2", 12);

                garage.AddCar(car);
                garage.AddCar(car1);
                garage.AddCar(car2);

                garage.FixCar("BMW");
                garage.FixCar("BMW1");
                garage.FixCar("BMW2");

                Assert.AreEqual(3, garage.CarsInGarage);
                Assert.AreEqual(3, garage.RemoveFixedCar());
                Assert.AreEqual(0, garage.CarsInGarage);
            }
            [Test]
            public void ReportShouldWork()
            {
                List<string> cars = new List<string>();


                Car car = new Car("BMW", 0);
                garage.AddCar(car);
                garage.AddCar(new Car("BMW1", 3));
                garage.AddCar(new Car("BMW2", 2));
                garage.AddCar(new Car("BMW3", 1));

                cars.Add("BMW1");
                cars.Add("BMW2");
                cars.Add("BMW3");



                string carsNames = string.Join(", ", cars);
                string report = $"There are {cars.Count} which are not fixed: {carsNames}.";

                Assert.AreEqual(report, garage.Report());
            }

        }
    }
}