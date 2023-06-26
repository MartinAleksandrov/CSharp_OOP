
namespace BorderControl.Models
{
    using Interfaces;
    public class Citizens : ICitizens
    {
        public Citizens(string name, int age, string id)
        {
            Name = name;
            Age = age;
            CitizneId = id;
        }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public string CitizneId { get; private set; }
    }
}
