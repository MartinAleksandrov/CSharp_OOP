using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stealer
{
    public class Spy
    {
        public string RevealPrivateMethods(string className)
        {

            Type type = Type.GetType(className)!;

            MethodInfo[] infos = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (MethodInfo info in infos.Where(f=> f.Name.StartsWith("get")))
            {
                stringBuilder.AppendLine($"{info.Name} will return {info.ReturnType}");
            }
            foreach (MethodInfo info in infos.Where(f => f.Name.StartsWith("s   et")))
            {
                stringBuilder.AppendLine($"{info.Name} will set field of {info.GetParameters().First()}");
            }

            return stringBuilder.ToString().Trim();
            //FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            //MethodInfo[] publicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            //MethodInfo[] privateMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            //StringBuilder stringBuilder = new StringBuilder();

            //foreach (FieldInfo fieldName in fields)
            //{
            //    stringBuilder.AppendLine($"{fieldName.Name} must be private!");
            //}
            //foreach (MethodInfo method in publicMethods)
            //{
            //    stringBuilder.AppendLine($"{method.Name} must be public!");

            //}
            //foreach (MethodInfo method in privateMethods)
            //{
            //    stringBuilder.AppendLine($"{method.Name} must be private!");

            //}
            //return stringBuilder.ToString().Trim();
        }
    }
}
