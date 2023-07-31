using Heroes.IO.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Heroes.IO
{
    public class MyWriter : IWriter
    {
        string path = "../../../output.txt";
        public void Write(string message)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.Write(message);
            }
        }

        public void WriteLine(string message)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
            }
        }
    }
}
