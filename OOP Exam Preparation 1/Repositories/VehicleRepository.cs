using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private List<IVehicle> vehicles;

        public VehicleRepository()
        {
            vehicles = new List<IVehicle>();
        }

        public IReadOnlyCollection<IVehicle> routesCollection => vehicles;

        public void AddModel(IVehicle model)
        {
            vehicles.Add(model);
        }

        public IVehicle FindById(string identifier) => vehicles.FirstOrDefault(r => r.LicensePlateNumber == identifier);

        public IReadOnlyCollection<IVehicle> GetAll() => this.vehicles;

        public bool RemoveById(string identifier) => this.vehicles.Remove(FindById(identifier));
    }
}
