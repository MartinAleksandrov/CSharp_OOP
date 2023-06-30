using WildFarm.Models.FoodClasses;

namespace WildFarm.Models.AnimalClasses.Mammals
{
    public class Mouse : Mammal
    {
        public Mouse(string name, double weight, string region) : base(name, weight, region)
        {
        }

        protected override double WeightMultiPlier => 0.10;

        public override IReadOnlyCollection<Type> PreferedFoods => new HashSet<Type> { typeof(Vegetable), typeof(Fruit) };

        public override string ProduceSound()
        {
            return "Squeak";
        }
    }
}
