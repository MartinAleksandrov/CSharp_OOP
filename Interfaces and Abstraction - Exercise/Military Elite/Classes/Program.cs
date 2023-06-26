 
using Military_Elite.Core;
using Military_Elite.Core.Interfaces;
using Military_Elite.IO;
using Military_Elite.IO.Interfaces;

namespace Military_Elite;
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