using BorderControl.Core;
using BorderControl.Core.Interfaces;
using BorderControl.IO;
using BorderControl.IO.Interfaces;

namespace BorderControl;
public class StartUp
{
    public static void Main()
    {
        IReader reader = new ConsoleReader();
        IWriter writer = new ConsoleWriter();

        IEngine engine = new Engine(reader, writer);
        engine.Run();
    }
}