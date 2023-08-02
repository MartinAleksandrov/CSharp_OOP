using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private readonly PilotRepository pilotRepository;
        private readonly RaceRepository raceRepository;
        private readonly FormulaOneCarRepository formulaOneCarRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            formulaOneCarRepository = new FormulaOneCarRepository();
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            IPilot pilot = pilotRepository.FindByName(pilotName);
            IFormulaOneCar car = formulaOneCarRepository.FindByName(carModel);

            if (pilot == null || pilot.Car != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }

            if (car == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            pilot.AddCar(car);
            formulaOneCarRepository.Remove(car);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IRace race = raceRepository.FindByName(raceName);
            IPilot pilot = pilotRepository.FindByName(pilotFullName);

            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            else if (pilot == null || pilot.CanRace == false || race.Pilots.Contains(pilot))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            race.AddPilot(pilot);
            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            IFormulaOneCar car = formulaOneCarRepository.FindByName(model);

            if (car != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }
            else if (type != nameof(Ferrari) && type != nameof(Williams))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            if (type == nameof(Ferrari))
            {
                car = new Ferrari(model, horsepower, engineDisplacement);
            }
            else
            {
                car = new Williams(model, horsepower, engineDisplacement);
            }

            formulaOneCarRepository.Add(car);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreatePilot(string fullName)
        {
            IPilot pilot = pilotRepository.FindByName(fullName);

            if (pilot != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }

            pilot = new Pilot(fullName);
            pilotRepository.Add(pilot);

            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            IRace race = raceRepository.FindByName(raceName);

            if (race != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            race = new Race(raceName, numberOfLaps);
            raceRepository.Add(race);
            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string PilotReport()  
        {
            StringBuilder sb = new StringBuilder();

            foreach (var pil in pilotRepository.Models.OrderByDescending(p => p.NumberOfWins))
            {
                sb.AppendLine(pil.ToString());
            }

            return sb.ToString().Trim();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var pil in raceRepository.Models.Where(p => p.TookPlace))
            {
                sb.AppendLine(pil.RaceInfo());
            }

            return sb.ToString().Trim();
        }

        public string StartRace(string raceName)
        {
            IRace race = raceRepository.FindByName(raceName);

            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            else if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }
            else if (race.TookPlace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            var fastestPilots = race.Pilots.
                OrderByDescending(p => p.Car.RaceScoreCalculator(race.NumberOfLaps)).
                ToList();


            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Pilot {fastestPilots[0].FullName} wins the {raceName} race.");
            sb.AppendLine($"Pilot {fastestPilots[1].FullName} is second in the {raceName} race.");
            sb.AppendLine($"Pilot {fastestPilots[2].FullName} is third in the {raceName} race.");

            fastestPilots[0].WinRace();
            race.TookPlace = true;

            return sb.ToString().Trim();
        }
    }
}
