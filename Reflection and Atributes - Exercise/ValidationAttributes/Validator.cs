using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ValidationAttributes
{
    public class Validator
    {
        public static bool IsValid(object obj)
        {
            Type type = obj.GetType();

            PropertyInfo[] properties = type.GetProperties()
                .Where(p => p.CustomAttributes
            .Any(a => typeof(MyValidationAttribute)
            .IsAssignableFrom(a.AttributeType))).ToArray();


            foreach (var item in properties)
            {
                IEnumerable<MyValidationAttribute> atributes = item.GetCustomAttributes()
                    .Where(ca => typeof(MyValidationAttribute)
                .IsAssignableFrom(ca.GetType()))
                    .Cast<MyValidationAttribute>();

                foreach (MyValidationAttribute atr in atributes)
                {
                    if (!atr.IsValid(item.GetValue(obj)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
