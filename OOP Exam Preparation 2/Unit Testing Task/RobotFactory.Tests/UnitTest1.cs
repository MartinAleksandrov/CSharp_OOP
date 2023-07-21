using NUnit.Framework;
using System.Diagnostics;
using System.Reflection;

namespace RobotFactory.Tests
{
    public class Tests
    {
        private Supplement supplement;
        private Robot robot;
        private Factory factory;

        [SetUp]
        public void Setup()
        {

        }

        //Robot Tests
        [Test]
        public void CreatingRobot()
        {
            string model = "Tesla";
            double price = 20;
            int interFaceStandards = 10;

            robot = new Robot(model, price, interFaceStandards);

            Assert.That(robot.Model, Is.EqualTo(model));
            Assert.That(robot.Price, Is.EqualTo(price));
            Assert.That(robot.InterfaceStandard, Is.EqualTo(interFaceStandards));
        }
        [Test]
        public void RobotToStringMethodShouldWork()
        {
            string model = "Tesla";
            double price = 20;
            int interFaceStandards = 10;

            robot = new Robot(model, price, interFaceStandards);

            Assert.That(robot.ToString(), Is.EqualTo($"Robot model: {model} IS: {interFaceStandards}, Price: {price:f2}"));
        }
        [Test]
        public void RobotProperiesShoulSetCorrect()
        {
            string model = "Tesla";
            double price = 20;
            int interFaceStandards = 10;

            robot = new Robot(model, price, interFaceStandards);

            robot.Model = "NewModel";
            robot.Price = 10;
            robot.InterfaceStandard = 30;

            Assert.That(robot.Model, Is.EqualTo("NewModel"));
            Assert.That(robot.Price, Is.EqualTo(10));
            Assert.That(robot.InterfaceStandard, Is.EqualTo(30));
            Assert.That(robot.Supplements.Count, Is.EqualTo(0));
        }


        //Supplemt Tests
        [Test]
        public void CreatingSupplement()
        {
            string supplementName = "ZOB";
            int interFaceStandard = 10;

            supplement = new Supplement(supplementName, interFaceStandard);

            Assert.That(supplement.Name, Is.EqualTo(supplementName));
            Assert.That(supplement.InterfaceStandard, Is.EqualTo(interFaceStandard));

        }
        [Test]
        public void SupplementToStringMethodShouldWork()
        {
            string supplementName = "ZOB";
            int interfaceStandard = 10;

            supplement = new Supplement(supplementName, interfaceStandard);

            Assert.That($"Supplement: {supplementName} IS: {interfaceStandard}", Is.EqualTo(supplement.ToString())); ;
        }
        [Test]
        public void SupplementProperiesShoulSetCorrect()
        {
            string supplementName = "ZOB";
            int interfaceStandard = 10;

            supplement = new Supplement(supplementName, interfaceStandard);

            supplement.Name = "ZOBDOGROB";
            supplement.InterfaceStandard = 5;

            Assert.That(supplement.Name, Is.EqualTo("ZOBDOGROB"));
            Assert.That(supplement.InterfaceStandard, Is.EqualTo(5));
        }

        //Factory Tests
        [Test]
        public void CreatingFactory()
        {
            string factoryName = "QKOFACTORY";
            int capacity = 10;

            factory = new Factory(factoryName, capacity);

            Assert.That(factory.Name, Is.EqualTo(factoryName));
            Assert.That(factory.Capacity, Is.EqualTo(capacity));
        }

        [Test]
        public void FactoryProperiesShoulSetCorrect()
        {
            string factoryName = "QKOFACTORY";
            int capacity = 10;

            factory = new Factory(factoryName, capacity);

            factory.Name = "TupoFactory";
            factory.Capacity = 5;

            Assert.That(factory.Name, Is.EqualTo("TupoFactory"));
            Assert.That(factory.Capacity, Is.EqualTo(5));

            Assert.That(factory.Robots.Count, Is.EqualTo(0));
            Assert.That(factory.Supplements.Count, Is.EqualTo(0));

        }

        [Test]
        public void ProduceRobotShouldWork()
        {
            string expectedModel = "ROBOTO";
            double excpectedPrice = 12;
            int excpectedinterfacestandarts = 5;

            robot = new Robot(expectedModel, excpectedPrice, excpectedinterfacestandarts);
            factory = new Factory("Fac", 5);
            string actualReslt = factory.ProduceRobot(expectedModel, excpectedPrice, excpectedinterfacestandarts);

            Assert.That(actualReslt, Is.EqualTo($"Produced --> {robot}"));

            int excpectedCount = 1;
            Assert.That(excpectedCount, Is.EqualTo(factory.Robots.Count));

        }

        [Test]
        public void ProduceRobotUnableToCreateRobot()
        {
            factory = new Factory("Fac", 1);


            string expectedModel = "ROBOTO";
            double excpectedPrice = 12;
            int excpectedinterfacestandarts = 5;

            robot = new Robot(expectedModel, excpectedPrice, excpectedinterfacestandarts);
            Robot newRobot = new Robot("Rob", 5, 20);

            factory.ProduceRobot(expectedModel, excpectedPrice, excpectedinterfacestandarts);
            string actualReslt = factory.ProduceRobot("Rob", 5, 20);

            Assert.That(actualReslt, Is.EqualTo("The factory is unable to produce more robots for this production day!"));
        }


        [Test]
        public void ProduceSupplementShouldWork()
        {
            factory = new Factory("Fac", 5);

            string expectedName = "metan";
            int excpectedinterfacestandarts = 5;

            supplement = new Supplement(expectedName, excpectedinterfacestandarts);
            string actualReslt = factory.ProduceSupplement(expectedName, excpectedinterfacestandarts);

            Assert.That(actualReslt, Is.EqualTo($"Supplement: {expectedName} IS: {excpectedinterfacestandarts}"));

            int excpectedCount = 1;
            Assert.That(excpectedCount, Is.EqualTo(factory.Supplements.Count));

        }

        [Test]
        public void UpgradeRobotShouldWorkAndReturnTrue()
        {
            factory = new Factory("Fac", 3);

            robot = new Robot("Rob", 2, 5);
            supplement = new Supplement("Sup", 5);

            bool result = factory.UpgradeRobot(robot, supplement);

            Assert.IsTrue(result);

            Assert.That(robot.Supplements.Count, Is.EqualTo(1));
        }

        [Test]
        public void UpgradeRobotShouldWorkAndReturnFalse()
        {
            factory = new Factory("Fac", 3);

            robot = new Robot("Rob", 2, 2);
            supplement = new Supplement("Sup", 5);

            bool result = factory.UpgradeRobot(robot, supplement);

            Assert.IsFalse(result);
            Assert.That(robot.Supplements.Count, Is.EqualTo(0));


            robot = new Robot("Rob", 2, 5);
            supplement = new Supplement("Sup", 5);
            factory.UpgradeRobot(robot, supplement);

            bool result2 = factory.UpgradeRobot(robot, supplement);

            Assert.IsFalse(result2);
        }


        [Test]
        public void SellRobotShouldWork()
        {
            factory = new Factory("Fac", 3);
            robot = new Robot("Lob", 20, 6);

            factory.ProduceRobot("Rob", 12, 4);
            factory.ProduceRobot("Mob", 10, 5);
            factory.ProduceRobot("Lob", 20, 6);

            Robot newRob = factory.SellRobot(25);

            Assert.That(newRob.Model, Is.EqualTo(robot.Model));
            Assert.That(newRob.Price, Is.EqualTo(robot.Price));
            Assert.That(newRob.InterfaceStandard, Is.EqualTo(robot.InterfaceStandard));

        }
    }
}