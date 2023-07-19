﻿using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Repositories.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private IRepository<IUser> users;
        private IRepository<IVehicle> vehicles;
        private IRepository<IRoute> routes;

        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }

        public string AllowRoute(string startPoint, string endPoint, double length) 
        {
            int routeId = routes.GetAll().Count + 1;

            IRoute route = routes.GetAll().FirstOrDefault(s => s.StartPoint == startPoint && s.EndPoint == endPoint);

            if (route != null)
            {
                if (route.Length == length)
                {
                    return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
                }
                else if (route.Length < length)
                {
                    return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint, length);
                }
                else if (route.Length > length)
                {
                    route.LockRoute();
                }
            }

            IRoute newRoute = new Route(startPoint, endPoint, length, routeId);

            routes.AddModel(newRoute);
            return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {

            IUser user = users.FindById(drivingLicenseNumber);
            IVehicle vehicle = vehicles.FindById(licensePlateNumber);
            IRoute route = routes.FindById(routeId);

            if (user.IsBlocked)
            {
                return string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }

            if (vehicle.IsDamaged)
            {
                return string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }

            if (route.IsLocked)
            {
                return string.Format(OutputMessages.RouteLocked, routeId);
            }

            vehicle.Drive(route.Length);

            ////////////////////////////////////////////////
            if (isAccidentHappened)
            {
                vehicle.ChangeStatus();
                user.DecreaseRating();
            }
            else if (isAccidentHappened == false)
            {
                user.IncreaseRating();
            }

            return vehicle.ToString();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            IUser user = users.FindById(drivingLicenseNumber);

            if (user != null)
            {
                return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }

            user = new User(firstName, lastName, drivingLicenseNumber);

            users.AddModel(user);

            return string.Format(OutputMessages.UserSuccessfullyAdded,firstName,lastName, drivingLicenseNumber);
        }

        public string RepairVehicles(int count)
        {
            var damagedVehicles = this.vehicles.GetAll().Where(v => v.IsDamaged == true).OrderBy(v => v.Brand).ThenBy(v => v.Model);

            int vehiclesCount = 0;

            if (damagedVehicles.Count() < count)
            {
                vehiclesCount = damagedVehicles.Count();
            }
            else
            {
                vehiclesCount = count;
            }

            var selectedVehicles = damagedVehicles.ToArray().Take(vehiclesCount);

            foreach (var vehicle in selectedVehicles)
            {
                vehicle.ChangeStatus();
                vehicle.Recharge();
            }

            return string.Format(OutputMessages.RepairedVehicles, vehiclesCount);
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != "CargoVan" && vehicleType != "PassengerCar")
            {
                return string.Format(OutputMessages.VehicleTypeNotAccessible,vehicleType);
            }

            IVehicle vehicle = this.vehicles.FindById(licensePlateNumber);

            if (vehicle != null )
            {
                return string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);

            }
            else
            {
                if (vehicleType == nameof(PassengerCar))
                {
                     vehicle = new PassengerCar(brand,model,licensePlateNumber);
                }
                else if (vehicleType == nameof(CargoVan))
                {
                     vehicle = new CargoVan(brand, model, licensePlateNumber);

                }

                vehicles.AddModel(vehicle);
                return string.Format(OutputMessages.VehicleAddedSuccessfully,brand,model,licensePlateNumber);
            }
          
        }

        public string UsersReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("*** E-Drive-Rent ***");
            foreach (var user in this.users.GetAll().OrderByDescending(u => u.Rating).ThenBy(u => u.LastName).ThenBy(u => u.FirstName))
            {
                sb.AppendLine(user.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
