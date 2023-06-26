using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfo
{
    public interface IPerson : IBirthable,IIdentifiable
    {
        string Name { get; }
        int Age { get; }
    }
}
