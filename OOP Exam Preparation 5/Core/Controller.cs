using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private readonly HotelRepository hotels;
        private IHotel hotel;

        public Controller()
        {
            hotels = new HotelRepository();
        }

        public string AddHotel(string hotelName, int category)
        {
            hotel = hotels.Select(hotelName);

            if (hotel != null)
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }

            Hotel newHotel = new Hotel(hotelName, category);
            hotels.AddNew(newHotel);

            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);

        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (this.hotels.All().FirstOrDefault(x => x.Category == category) == null)
            {
                return string.Format(OutputMessages.CategoryInvalid, category);
            }
            var orderedHotels =
                this.hotels.All().Where(x => x.Category == category).OrderBy(x => x.Turnover).ThenBy(x => x.FullName);


            foreach (var hotel in orderedHotels)
            {
                IRoom room = hotel.Rooms.
                    All().
                    Where(r => r.PricePerNight > 0).
                    OrderBy(r => r.BedCapacity).
                    FirstOrDefault(r => r.BedCapacity >= adults + children);

                if (room != null)
                {
                    int bookingNumber = hotel.Bookings.All().Count + 1;
                    IBooking booking = new Booking(room, duration, adults, children, bookingNumber);

                    hotel.Bookings.AddNew(booking);
                    return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotel.FullName);
                }
            }

            return string.Format(OutputMessages.RoomNotAppropriate);

        }

        public string HotelReport(string hotelName)
        {
            hotel = hotels.Select(hotelName);

            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Hotel name: {hotelName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine($"--Bookings:");
            sb.AppendLine();

            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine("none");
            }
            else
            {
                foreach (var booking in hotel.Bookings.All())
                {
                    sb.AppendLine(booking.BookingSummary());
                    sb.AppendLine();
                }
            }
            
            return sb.ToString().TrimEnd();
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            hotel = hotels.Select(hotelName);

            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            else if (roomTypeName != nameof(Apartment) && roomTypeName != nameof(Studio) && roomTypeName != nameof(DoubleBed))
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }
            else if (hotel.Rooms.Select(roomTypeName) == null)
            {
                return string.Format(OutputMessages.RoomTypeNotCreated);
            }

            IRoom room = hotel.Rooms.Select(roomTypeName);

            if (room.PricePerNight > 0)
            {
                throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);
            }

            room.SetPrice(price);
            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            hotel = hotels.Select(hotelName);
            IRoom room = hotel.Rooms.Select(roomTypeName);

            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            else if (room != null)
            {
                return string.Format(OutputMessages.RoomTypeAlreadyCreated);
            }
            else if (roomTypeName != nameof(Apartment) && roomTypeName != nameof(Studio) && roomTypeName != nameof(DoubleBed))
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }
            else
            {
                if (roomTypeName == nameof(Apartment))
                {
                    room = new Apartment();
                }
                else if (roomTypeName == nameof(Studio))
                {
                    room = new Studio();
                }
                else
                {
                    room = new DoubleBed();
                }
                hotel.Rooms.AddNew(room);

                return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
            }
        }
    }
}
