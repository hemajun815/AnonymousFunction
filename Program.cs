using System;
using System.Collections.Generic;
using System.Linq;

namespace AnonymousFunction
{
    class Program
    {
        delegate void del(string s);
        static void Main()
        {
            del d = delegate(string str) { Console.WriteLine(str); };
            d("匿名函数");
            d += (str) => { Console.WriteLine(str.ToUpper()); };
            d("匿名函数的Lamda表达式");
            Console.ReadKey();
        }
    }
}
