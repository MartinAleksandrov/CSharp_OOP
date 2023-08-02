using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        private List<Smartphone> phones;
        private int capactiy;
        private Shop shop;
        private Smartphone samsung;

        [SetUp]
        public void SetUp()
        {
            this.capactiy = 2;
            this.phones = new List<Smartphone>();
            this.shop = new Shop(2);
            this.samsung = new Smartphone("Samsung", 100);
        }

        [Test]
        public void Test_Constructor_Works_Correct()
        {
            Assert.IsNotNull(this.phones);
            Assert.AreEqual(2, this.shop.Capacity);
        }

        [Test]
        public void Test_Throw_Exception_If_Phone_Capacity_Is_Lower_Than_Zero()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                this.shop = new Shop(-1);
            });

            Assert.AreEqual("Invalid capacity.", ex.Message);
        }

        [Test]
        public void Test_Can_Add_Phones()
        {
            this.shop.Add(this.samsung);

            Assert.AreEqual(1, this.shop.Count);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Add_Existing_Phone()
        {
            this.shop.Add(this.samsung);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.shop.Add(new Smartphone("Samsung", 100));
            });

            Assert.AreEqual("The phone model Samsung already exist.", ex.Message);
        }

        [Test]
        public void Test_Cannot_Add_Phone_If_Capacity_Is_Max()
        {
            this.shop = new Shop(1);
            this.shop.Add(this.samsung);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.shop.Add(new Smartphone("Iphone", 100));
            });

            Assert.AreEqual("The shop is full.", ex.Message);
        }

        [Test]
        public void Test_Can_Remove_Phone()
        {
            this.shop.Add(this.samsung);

            this.shop.Remove("Samsung");
            Assert.AreEqual(0, this.shop.Count);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Remove_Not_Existing_Phone()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.shop.Remove("Nokia");
            });

            Assert.AreEqual("The phone model Nokia doesn't exist.", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Test_Not_Existing_Phone()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.shop.TestPhone("Nokia", 100);
            });

            Assert.AreEqual("The phone model Nokia doesn't exist.", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Test_Phone_With_Low_Battery()
        {
            this.samsung = new Smartphone("Samsung", 100)
            {
                CurrentBateryCharge = 10
            };

            this.shop.Add(this.samsung);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.shop.TestPhone("Samsung", 20);
            });

            Assert.AreEqual($"The phone model {this.samsung.ModelName} is low on batery.", ex.Message);
        }

        [Test]
        public void Test_Test_Phone_Reduces_Battery_Charge()
        {
            this.shop.Add(this.samsung);
            this.shop.TestPhone("Samsung", 50);

            Assert.AreEqual(50, this.samsung.CurrentBateryCharge);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Charge_Not_Existing_Phone()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.shop.ChargePhone("Nokia");
            });

            Assert.AreEqual("The phone model Nokia doesn't exist.", ex.Message);
        }

        [Test]
        public void Test_Charge_Phone_Works_Correct()
        {
            this.samsung = new Smartphone("Samsung", 100)
            {
                CurrentBateryCharge = 50
            };

            this.shop.Add(this.samsung);

            this.shop.ChargePhone("Samsung");
            Assert.AreEqual(100, this.samsung.CurrentBateryCharge);
        }
    }
}