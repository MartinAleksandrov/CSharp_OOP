namespace ShoppingSpree;

using System;
public class Program
{
    static void Main(string[] args)
    {
        List<Person> persons = new List<Person>();
        List<Product> products = new List<Product>();

        try
        {
            string[] personInput = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in personInput)
            {
                string[] data = item.Split("=", StringSplitOptions.RemoveEmptyEntries);

                Person person = new Person(data[0], decimal.Parse(data[1]));

                persons.Add(person);
            }


            string[] productInput = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in productInput)
            {
                string[] data = item.Split("=", StringSplitOptions.RemoveEmptyEntries);

                Product product = new Product(data[0], decimal.Parse(data[1]));

                products.Add(product);
            }
        }
        catch (ArgumentException e)
        {

            Console.WriteLine(e.Message);
            return;
        }



        string command = string.Empty;
        while ((command = Console.ReadLine()) != "END")
        {
            string[] commArgs = command.Split(" ",StringSplitOptions.RemoveEmptyEntries);

            Person person = persons.FirstOrDefault(p => p.Name == commArgs[0]);
            Product product = products.FirstOrDefault(p => p.Name == commArgs[1]);



            if (person != null && product != null)
            {
                Console.WriteLine(person.AddProduct(product));
            }

        }
        Console.WriteLine(string.Join(Environment.NewLine,persons));

    }
}
