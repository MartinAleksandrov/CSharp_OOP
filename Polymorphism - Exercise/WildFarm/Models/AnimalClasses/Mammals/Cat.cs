namespace WildFarm.Models.AnimalClasses.Mammals
{
    using FoodClasses;
    public class Cat : Feline
    {
        public Cat(string name, double weight, string region, string breed) :
            base(name, weight, region, breed)
        {
        }

        protected override double WeightMultiPlier => 0.30;

        public override IReadOnlyCollection<Type> PreferedFoods => new HashSet<Type> { typeof(Meat), typeof(Vegetable) };

        public override string ProduceSound()
        {
            return "Meow";
        }
    }
}
