using System;
using System.Collections.Generic;
using System.Linq;

namespace Interface
{
    class Program
    {
        static void Main()
        {
            WriteCode(new CSharpCoder());
            WriteCode(new JavaCoder());
            Console.ReadKey();
        }

        static private void WriteCode(ICoder coder)
        {
            coder.WriteCode();
        }
    }
}
