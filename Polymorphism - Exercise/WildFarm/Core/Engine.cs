namespace WildFarm.Core
{
    using Interfaces;
    using Factories;
    using Factories.Interfaces;
    using IO.Interfaces;
    using Models.Interfaces;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private IFoodFactory foodFactory;
        private IAnimalFactory animalFactory;


        public Engine(IReader reader, IWriter writer, IFoodFactory foodFactory, IAnimalFactory animalFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.foodFactory = foodFactory;
            this.animalFactory = animalFactory;
        }

        public void Run()
        {
            IFood food;
            IAnimal animal;

            List<IAnimal> animals = new List<IAnimal>();

            int oddOrEven = 0;
            string input = reader.ReadLine();
            while (true)
            {
                if (input.ToLower()=="end" )
                {
                    break;
                }
                try
                {
                    string[] commArgsAnimal = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    animal = animalFactory.CreateAnimal(commArgsAnimal);
                    animals.Add(animal);
                    writer.WriteLine(animal.ProduceSound());

                    string[] commArgs = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string foodType = commArgs[0];
                    int foodQuantity = int.Parse(commArgs[1]);

                    food = foodFactory.CreateFood(foodType, foodQuantity);

                    animal.Eat(food);
                }
                catch (ArgumentException ex)
                {
                    writer.WriteLine(ex.Message);
                }
                input = reader.ReadLine();
            }
            foreach (var anim in animals)
            {
                writer.WriteLine(anim.ToString());
            }
        }
    }
}
