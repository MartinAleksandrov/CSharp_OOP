using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Linq;

namespace BookigApp.Tests
{
    public class Tests
    {
        private Hotel hotel;

        [SetUp]
        public void Setup()
        {
            string fullName = "Royal";
            int category = 5;

            hotel = new Hotel(fullName, category);
        }

        [Test]
        public void HotelConstrShouldWork()
        {
            

            Assert.AreEqual("Royal", hotel.FullName);
            Assert.AreEqual(5, hotel.Category);
            Assert.AreEqual(0, hotel.Turnover);
            Assert.AreEqual(0,hotel.Rooms.Count);
            Assert.AreEqual(0,hotel.Bookings.Count);
        }

        [Test]
        public void PropsShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => hotel = new Hotel(" ",3));
            Assert.Throws<ArgumentException>(() => hotel = new Hotel("asd", 8));
        }

        [Test]
        public void AddRoomShouldWork()
        {
            for (int i = 0; i < 5; i++)
            {
                Room room = new Room(i+1, 10+i);
                hotel.AddRoom(room);
            }

            Assert.AreEqual(5,hotel.Rooms.Count);
        }

        [Test]
        public void BookRoomShouldWork()
        {
            for (int i = 0; i < 4; i++)
            {
                Room room = new Room(i + 1, 10);
                hotel.AddRoom(room);
            }

            hotel.BookRoom(2,2,5,100);

            Assert.AreEqual(50,hotel.Turnover);
            Assert.AreEqual(1, hotel.Bookings.Count);
            
        }

        //[Test]
        //public void BudgetIsTooLow()
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        Room room = new Room(i + 1, 10);
        //        hotel.AddRoom(room);
        //    }

        //    hotel.BookRoom(2, 2, 5, 5);

        //    Assert.AreEqual(0, hotel.Turnover);
        //    Assert.AreEqual(0, hotel.Bookings.Count);

        //}
        [Test]
        public void BookRoomShouldThrowException()
        {

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(0, 2, 5, 100));
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, -5, 5, 100));
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, 5, 0, 100));

        }
    }
}