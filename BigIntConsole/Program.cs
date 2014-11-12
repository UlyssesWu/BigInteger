using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uly.Numerics;

namespace BigIntConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger a = new BigInteger("9999999999999999999999999999");
            BigInteger b = new BigInteger("0");
            Console.WriteLine(string.Format("op1:{0}   op2:{1}",a,b));
            Console.WriteLine(a + b);
            Console.WriteLine(a - b);
            Console.WriteLine(a * b);
            Console.WriteLine(a / b);
            Console.ReadLine();
        }
    }
}
