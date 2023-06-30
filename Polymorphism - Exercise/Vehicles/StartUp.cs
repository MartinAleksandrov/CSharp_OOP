
namespace Vehicles;

using Models;
using Vehicles.Models.Interfaces;

public class StartUp
{
    public static void Main()
    {
        List<IVehicle> listVehicles = new List<IVehicle>();

        listVehicles.Add(CreateVehicle());
        listVehicles.Add(CreateVehicle());
        listVehicles.Add(CreateVehicle());


        int n = int.Parse(Console.ReadLine());

        IVehicle vehicle;
        for (int i = 0; i < n; i++)
        {
            string[] commands = Console.ReadLine().Split();

            string currCamond = commands[0];
            string typeVehicle = commands[1];
            double distanceOrLitters = double.Parse(commands[2]);

            try
            {
                if (currCamond == "Drive")
                {
                    vehicle = listVehicles.FirstOrDefault(v => v.GetType().Name == typeVehicle);
                    if (typeVehicle=="Bus")
                    {
                        vehicle.AdditionalFuel(currCamond);
                    }
                    vehicle.DriveVehicle(distanceOrLitters);
                }
                else if (currCamond == "DriveEmpty")
                {
                    vehicle = listVehicles.FirstOrDefault(v => v.GetType().Name == typeVehicle);
                    if (typeVehicle == "Bus")
                    {
                        vehicle.AdditionalFuel(currCamond);
                    }
                    vehicle.DriveVehicle(distanceOrLitters);
                }
                else
                {
                    vehicle = listVehicles.FirstOrDefault(v => v.GetType().Name == typeVehicle);
                    vehicle.RefuelVehicle(distanceOrLitters);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        foreach (var veh in listVehicles)
        {
            Console.WriteLine(veh.ToString());
        }
    }
    public static IVehicle CreateVehicle()
    {
        string[] vehicles = Console.ReadLine().Split();

        string vehicleType = vehicles[0];
        double fuelQuantity = double.Parse(vehicles[1]);
        double fuelCinspumtion = double.Parse(vehicles[2]);
        double tankCapacity = double.Parse(vehicles[3]);

        Vehicle vehicle;
        if (vehicleType == "Car")
        {
            vehicle = new Car(fuelQuantity, fuelCinspumtion,tankCapacity);
        }
        else if(vehicleType=="Truck") 
        {
            vehicle = new Truck(fuelQuantity, fuelCinspumtion,tankCapacity);
        }
        else
        {
            vehicle = new Bus(fuelQuantity, fuelCinspumtion, tankCapacity);
        }

        return vehicle;
    }
}
