using NUnit.Framework;
using System.Linq;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Garage_CheckCapacity()
        {
            Garage garage = new Garage(3);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);

            bool actualResult = garage.AddVehicle(scooter);
            Assert.IsFalse(actualResult);
        }

        [Test]
        public void Garage_AddVehicle_LicensePlateAlreadyExists()
        {
            Garage garage = new Garage(3);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");

            garage.AddVehicle(car);

            bool actualResult = garage.AddVehicle(car);
            Assert.IsFalse(actualResult);
        }

        [Test]
        public void Garage_AddVehicle_WorksProperly()
        {
            Garage garage = new Garage(3);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");

            bool actualResult = garage.AddVehicle(car);
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void Garage_DriveVehicle_WorksProperly()
        {
            Garage garage = new Garage(5);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);
            garage.AddVehicle(scooter);

            garage.DriveVehicle("H7806AH", 37, false);

            int actualBatteryLevel = van.BatteryLevel;
            int expectedBatteryLevel = 63;

            Assert.AreEqual(expectedBatteryLevel, actualBatteryLevel);
        }

        [Test]
        public void Garage_DriveVehicle_AccidentOccured()
        {
            Garage garage = new Garage(5);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);
            garage.AddVehicle(scooter);

            garage.DriveVehicle("PB6006PA", 50, true);

            bool actualVehicleCondition = scooter.IsDamaged;

            Assert.IsTrue(actualVehicleCondition);
        }

        [Test]
        public void Garage_DriveVehicle_VehicleAlreadyDamaged()
        {
            Garage garage = new Garage(5);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);
            garage.AddVehicle(scooter);

            garage.DriveVehicle("P7006XX", 25, true);
            garage.DriveVehicle("P7006XX", 25, true);

            int actualBatteryLevel = truck.BatteryLevel;

            Assert.AreEqual(75, actualBatteryLevel);
        }

        [Test]
        public void Garage_DriveVehicle_VehicleAlready2()
        {
            Garage garage = new Garage(5);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);
            garage.AddVehicle(scooter);

            garage.DriveVehicle("P7006XX", 101, false);

            int actualBatteryLevel = truck.BatteryLevel;

            Assert.AreEqual(100, actualBatteryLevel);
        }

        [Test]
        public void Garage_DriveVehicle_VehicleAlready3()
        {
            Garage garage = new Garage(5);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);
            garage.AddVehicle(scooter);

            garage.DriveVehicle("P7006XX", 60, false);
            garage.DriveVehicle("P7006XX", 60, false);

            int actualBatteryLevel = truck.BatteryLevel;

            Assert.AreEqual(40, actualBatteryLevel);
        }

        [Test]
        public void Garage_ChargeVihicle()
        {
            Garage garage = new Garage(5);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);
            garage.AddVehicle(scooter);

            garage.DriveVehicle("CT7006H", 51, false);
            garage.DriveVehicle("H7806AH", 51, false);
            garage.DriveVehicle("P7006XX", 51, false);
            garage.DriveVehicle("PB6006PA", 50, false);

            int actualChargedVehicles = garage.ChargeVehicles(49);

            Assert.AreEqual(3, actualChargedVehicles);
        }

        [Test]
        public void Garage_RepairVihicles()
        {
            Garage garage = new Garage(5);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);
            garage.AddVehicle(scooter);

            garage.DriveVehicle("CT7006H", 51, true);
            garage.DriveVehicle("H7806AH", 51, true);
            garage.DriveVehicle("P7006XX", 51, true);
            garage.DriveVehicle("PB6006PA", 50, false);

            string actualResult = garage.RepairVehicles();
            string expectedResult = "Vehicles repaired: 3";

            Assert.AreEqual(expectedResult, actualResult);
            Assert.IsFalse(car.IsDamaged);
            Assert.IsFalse(van.IsDamaged);
            Assert.IsFalse(truck.IsDamaged);
        }
    }
}