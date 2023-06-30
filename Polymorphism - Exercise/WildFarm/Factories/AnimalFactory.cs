namespace WildFarm.Factories
{
    using Factories.Interfaces;
    using Models.Interfaces;
    using Models.AnimalClasses.Birds;
    using Models.AnimalClasses.Mammals;

    public class AnimalFactory : IAnimalFactory
    {
        public IAnimal CreateAnimal(string[] input)
        {
            IAnimal animal = null;

            string animalType = input[0];
            string animalName = input[1];
            double animalWeight = double.Parse(input[2]);

            if (animalType == "Cat")
            {
                string region = input[3];
                string breed = input[4];

                animal = new Cat(animalName, animalWeight, region, breed);
            }
            else if (animalType == "Tiger")
            {
                string region = input[3];
                string breed = input[4];

                animal = new Tiger(animalName, animalWeight, region, breed);
            }
            else if (animalType == "Hen")
            {
                double wingSize = double.Parse(input[3]);

                animal = new Hen(animalName, animalWeight, wingSize);
            }
            else if (animalType == "Owl")
            {
                double wingSize = double.Parse(input[3]);

                animal = new Owl(animalName, animalWeight, wingSize);
            }
            else if (animalType == "Mouse")
            {
                string region = input[3];

                animal = new Mouse(animalName, animalWeight, region);
            }
            else if (animalType == "Dog")
            {
                string region = input[3];

                animal = new Dog(animalName, animalWeight, region);
            }
            return animal;
        }
    }
}
