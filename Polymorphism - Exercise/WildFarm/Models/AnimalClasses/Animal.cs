namespace WildFarm.Models.AnimalClasses
{
    using Interfaces;
    public abstract class Animal : IAnimal
    {
        public Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public string Name { get; private set; }

        public double Weight { get; private set; }

        public double FoodEaten { get; private set; }

        protected abstract double WeightMultiPlier { get; }

        public abstract IReadOnlyCollection<Type> PreferedFoods { get; }

        public void Eat(IFood food)
        {
            if (!PreferedFoods.Any(f=> f.Name == food.GetType().Name))
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.GetType().Name}!");
            }
            this.Weight += food.FoodQuantity * WeightMultiPlier;
            FoodEaten += food.FoodQuantity;
        }

        public abstract string ProduceSound();

        public override string ToString()
        {
            return $"{this.GetType().Name} [{Name}, ";
        }
    }
}
