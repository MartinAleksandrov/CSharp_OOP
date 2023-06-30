using WildFarm.Models.FoodClasses;

namespace WildFarm.Models.AnimalClasses.Mammals
{
    public class Dog : Mammal
    {
        public Dog(string name, double weight, string region) : 
            base(name, weight, region)
        {
        }

        protected override double WeightMultiPlier => 0.40;

        public override IReadOnlyCollection<Type> PreferedFoods => new HashSet<Type> { typeof(Meat) };

        public override string ProduceSound()
        {
            return "Woof!";
        }
    }
}
