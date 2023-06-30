using WildFarm.Models.FoodClasses;

namespace WildFarm.Models.AnimalClasses.Mammals
{
    public class Tiger : Feline
    {
        public Tiger(string name, double weight, string region, string breed) : 
            base(name, weight, region, breed)
        {
        }

        protected override double WeightMultiPlier => 1.00;

        public override IReadOnlyCollection<Type> PreferedFoods => new HashSet<Type> { typeof(Meat) };

        public override string ProduceSound()
        {
            return "ROAR!!!"; 
        }
    }
}
