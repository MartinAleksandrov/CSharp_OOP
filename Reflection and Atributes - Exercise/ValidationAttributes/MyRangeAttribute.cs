using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MyRangeAttribute : MyValidationAttribute
    {
        private int minValue;
        private int maxValue;

        public MyRangeAttribute(int minV , int maxV)
        {
            minV = minValue;
            maxV = maxValue;
        }

        public override bool IsValid(object obj)
            => (int)obj>=12 && (int)obj<=90;
    }
}
