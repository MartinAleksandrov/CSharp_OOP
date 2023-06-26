
namespace BirthdayCelebrations.Models
{
    using BirthdayCelebrations.Models.Interfaces;
    public class Citizens : ICitizens,IBuyer
    {
        public Citizens(string name, int age, string id, string birthDate)
        {
            Name = name;
            Age = age;
            CitizneId = id;
            Birthday = birthDate;
        }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public string CitizneId { get; private set; }
        public string Birthday { get; private set; }

        public int TotalFood { get; private set; }

        public void BuyFood()
        {
            TotalFood += 10;
        }
    }
}
