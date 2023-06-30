namespace WildFarm.Factories
{
    using Factories.Interfaces;
    using Models.Interfaces;
    using Models.FoodClasses;

    public class FoodFactory : IFoodFactory
    {
        public IFood CreateFood(string foodType, int quantity)
        {
            IFood food = null;
            if (foodType == "Fruit")
            {
                food = new Fruit(quantity);
            }
            else if (foodType == "Meat")
            {
                food = new Meat(quantity);
            }
            else if (foodType == "Seeds")
            {
                food = new Seeds(quantity);
            }
            else if (foodType == "Vegetable")
            {
                food = new Vegetable(quantity);
            }

            return food;
        }
    }
}
