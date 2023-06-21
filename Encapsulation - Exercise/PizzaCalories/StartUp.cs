namespace PizzaCalories;

public class StartUp
{

    public static void Main()
    {
        Pizza pizza;
        double calOfDoght = 0;
        try
        {
            string[] pizzaType = Console.ReadLine().Split();

             pizza = new Pizza(pizzaType[1]);

            string[] doughInfo = Console.ReadLine().Split();

            string flourType = doughInfo[1];
            string bakingTechnique = doughInfo[2];
            double grams = double.Parse(doughInfo[3]);

            Dough dough = new Dough(flourType, bakingTechnique, grams);
            calOfDoght += dough.CalOfDough(flourType, bakingTechnique, grams);

            //Console.WriteLine($"{dough.CalOfDough(flourType, bakingTechnique, grams):f2}");

            string toppingInput = string.Empty;
            while ((toppingInput = Console.ReadLine()) != "END")
            {
                string[] toppingInfo = toppingInput.Split();


                string toppingType = toppingInfo[1];
                double toppingGrams = double.Parse(toppingInfo[2]);

                Topping topping = new Topping(toppingType, toppingGrams);
                topping.CalOfTopping(toppingType, toppingGrams);
                pizza.AddPizza(topping);

                //Console.WriteLine($"{topping.CalOfTopping(toppingType, toppingGrams):f2}");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        Console.WriteLine($"{pizza.Name} - {pizza.TotalCalories()+ calOfDoght:f2} Calories.");

    }
}