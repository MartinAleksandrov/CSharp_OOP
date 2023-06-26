namespace Telephony;
using Core;
using Core.Interfaces;
using IO;
using IO.Interfaces;
using Models;
using Models.Interfaces;

public class StartUp
{
    public static void Main()
    {
        IReader reader = new ConsoleReader();
        IWriter writer = new ConsoleWriter();

        IEngine engine = new Engine(reader,writer);
        engine.Run();
    }
}