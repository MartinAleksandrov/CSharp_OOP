namespace Stealer;

public class StartUp
{
    public static void Main()
    {

        Spy spy = new();

        string result = spy.RevealPrivateMethods("Stealer.Hacker");
        Console.WriteLine(result);
    }
}