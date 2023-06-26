using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayCelebrations.Models.Interfaces
{
    public interface IBuyer
    {
        int TotalFood { get; }
        void BuyFood();

    }
}
