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
            BigInteger a = new BigInteger("--1234567890");
            BigInteger b = new BigInteger("---9876543210");
            Console.WriteLine(string.Format("op1:{0}   op2:{1}",a,b));
            Console.WriteLine(a + b);
            Console.WriteLine(a - b);
            Console.WriteLine(a * b);
            Console.WriteLine(a / b);
            Console.ReadLine();
        }
    }
}
