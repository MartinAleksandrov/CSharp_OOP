using BookingApp.Models.Rooms.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Rooms
{
    public abstract class Room : IRoom
    {
        public Room(int bedCapacity)
        {
            BedCapacity = bedCapacity;
            pricePerNight = 0;
        }

        private int bedCapacity;
        public int BedCapacity
        {
            get { return bedCapacity; }
            private set
            {
                bedCapacity = value;
            }
        }


        private double pricePerNight;
        public double PricePerNight
        {
            get { return pricePerNight; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.PricePerNightNegative);
                }
                pricePerNight = value;
            }
        }

        public void SetPrice(double price)
        {
            PricePerNight = price;
        }
    }
}
