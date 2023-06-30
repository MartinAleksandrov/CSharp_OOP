namespace WildFarm.Models.AnimalClasses.Mammals
{
    public abstract class Feline : Mammal
    {
        public  Feline(string name, double weight, string region,string breed) :
            base(name, weight, region)
        {
            Breed = breed;
        }

        public string Breed { get; private set; }

        public override string ToString()
        {
            return  $"{this.GetType().Name} [{Name}, {Breed}, {Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
