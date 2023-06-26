using BirthdayCelebrations.Models.Interfaces;

namespace BirthdayCelebrations.Models
{
    public class Rebel : IRebel ,IBuyer
    {
        public Rebel(string name, int age, string group)
        {
            Name = name;
            Age = age;
            Group = group;
        }

        public string Name {get; private set; }

        public int Age { get; private set; }

        public string Group { get; private set; }

        public int TotalFood { get; private set; }

        public void BuyFood()
        {
            TotalFood += 5;
        }
    }
}
