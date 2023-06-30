using WildFarm.Models.FoodClasses;

namespace WildFarm.Models.AnimalClasses.Birds
{
    public class Hen : Bird
    {
        public Hen(string name, double weight, double wingSize) :
            base(name, weight, wingSize)
        {
        }

        protected override double WeightMultiPlier => 0.35;

        public override IReadOnlyCollection<Type> PreferedFoods => new HashSet<Type> { typeof(Meat), typeof(Fruit),
            typeof(Seeds),typeof(Vegetable) };
        public override string ProduceSound()
        {
            return "Cluck";
        }
    }
}
