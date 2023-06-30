
namespace WildFarm.Models.AnimalClasses.Birds
{
using WildFarm.Models.FoodClasses;
    public class Owl : Bird
    {
        public Owl(string name, double weight, double wingSize) :
            base(name, weight, wingSize)
        {
        }

        protected override double WeightMultiPlier => 0.25;

        public override IReadOnlyCollection<Type> PreferedFoods => new HashSet<Type> { typeof(Meat) };
        public override string ProduceSound()
        {
            return "Hoot Hoot";
        }
    }
}
