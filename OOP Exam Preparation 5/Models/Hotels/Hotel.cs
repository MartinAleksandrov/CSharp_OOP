using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Hotels
{
    public class Hotel : IHotel
    {
        private IBooking booking;
        private IRoom room;
        private string fullName;
        private int category;
        private IRepository<IRoom> rooms;
        private IRepository<IBooking> bookings;


        public Hotel(string fullName, int category)
        {
            FullName = fullName;
            Category = category;
            rooms = new RoomRepository();
            bookings = new BookingRepository();
        }

        public string FullName
        {
            get { return fullName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HotelNameNullOrEmpty);
                }
                fullName = value;
            }
        }

        public int Category
        {
            get { return category; }
            private set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCategory);
                }
                category = value;
            }
        }

        public double Turnover
           => Math.Round(Bookings.All().Sum(x => x.ResidenceDuration * x.Room.PricePerNight), 2);

        public IRepository<IRoom> Rooms
        {
            get { return rooms; }
        }

        public IRepository<IBooking> Bookings
        {
            get { return bookings; }
        }
    }
}
