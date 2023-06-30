namespace WildFarm.Models.AnimalClasses.Mammals
{
    public abstract class Mammal : Animal
    {
        public Mammal(string name, double weight, string region) :
            base(name, weight)
        {
            LivingRegion = region;
        }

        public string LivingRegion { get; private set; }

        public override string ToString()
        {
            return base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
