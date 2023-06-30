namespace WildFarm.Models.Interfaces
{
    public interface IAnimal
    {
        string Name { get; }
        double Weight { get; }

        double FoodEaten { get; }

        void Eat(IFood food);
        string ProduceSound();
    }
}
