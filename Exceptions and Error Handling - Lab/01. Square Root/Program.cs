namespace SquareRoot;

public class StartUp
{
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());

		try
		{
            if (n<0)
            {
                throw new ArithmeticException();
            }
            double result = Math.Sqrt(n);
            Console.WriteLine(result);

        }
        catch (ArithmeticException ex)
		{
            Console.WriteLine("Invalid number.");
		}
        finally
        {
            Console.WriteLine("Goodbye.");
        }
    }
}