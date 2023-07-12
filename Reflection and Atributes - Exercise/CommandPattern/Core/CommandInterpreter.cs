using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string[] arguments = args.Split(" ",StringSplitOptions.RemoveEmptyEntries);


            string commandName = arguments[0];

            string[] param = arguments.Skip(1).ToArray();

            Type assembly = Assembly.GetEntryAssembly().GetTypes().FirstOrDefault(n => n.Name == $"{commandName}Command");

            if (assembly==null)
            {
                throw new InvalidOperationException("Command not found");
            }

            ICommand instance = Activator.CreateInstance(assembly) as ICommand;

             return instance.Execute(param);

        }
    }
}
