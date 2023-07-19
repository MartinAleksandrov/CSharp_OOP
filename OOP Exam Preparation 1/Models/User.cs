using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class User : IUser
    {
        public User(string firstName, string lastName, string drivingLicenseNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DrivingLicenseNumber = drivingLicenseNumber;
            this.rating = 0;
            this.isBlocked = false;
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.FirstNameNull);
                }
                firstName = value;
            }
        }


        private string lastName;
        public string LastName
        {
            get { return lastName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LastNameNull);
                }

                lastName = value;
            }
        }


        private string drivingLicenseNumber;
        public string DrivingLicenseNumber
        {
            get { return drivingLicenseNumber; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.DrivingLicenseRequired);
                }
                drivingLicenseNumber = value;
            }
        }


        private double rating;
        public double Rating => this.rating;


        private bool isBlocked;
        public bool IsBlocked => isBlocked;


        public void DecreaseRating()
        {

            if (this.rating < 2)
            {
                this.rating = 0;
                this.isBlocked = true;
            }
            else
            {
                this.rating -= 2;
            }
        }

        public void IncreaseRating()
        {
            if (this.rating < 10)
            {
                this.rating += 0.5;
            }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} Driving license: {DrivingLicenseNumber} Rating: {Rating}";
        }
    }
}
