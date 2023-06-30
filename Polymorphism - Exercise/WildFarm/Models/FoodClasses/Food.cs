namespace WildFarm.Models.Classes
{
    using Interfaces;
    public abstract class Food : IFood
    {
        protected Food(double foodQuantity)
        {
            FoodQuantity = foodQuantity;
        }

        public double FoodQuantity { get; private set; }
    }
}
