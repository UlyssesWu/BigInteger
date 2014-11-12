using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uly.Numerics;
using MSBigInt = System.Numerics.BigInteger;
using System.Diagnostics;

namespace BigIntTest
{

    [TestClass]
    public class SingleBigIntUnitTest
    {
        BigInteger op1,op2;
        MSBigInt cop1,cop2;
        private string ans1, ans2;

        private Stopwatch _timer = new Stopwatch();
        private long _nanoSecPerTick;
        [TestInitialize]
        public void Init()
        {
            op1 = new BigInteger("999999999999999999");
            op2 = new BigInteger("0");
            cop1 = MSBigInt.Parse(op1.ToString());
            cop2 = MSBigInt.Parse(op2.ToString());
            _timer.Reset();
            _nanoSecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            Console.WriteLine("时间单位：Tick（每Tick为 " + _nanoSecPerTick + " 纳秒）");
            Console.WriteLine("1毫秒 = " + 1000000/_nanoSecPerTick + " Tick");
        }


        [TestMethod]
        public void TestAdd()
        {
            _timer.Start();
            ans1 = (op1 + op2).ToString();
            _timer.Stop();
            Console.WriteLine(_timer.ElapsedTicks);
            _timer.Reset();

            _timer.Start();
            ans2 = (cop1 + cop2).ToString();
            _timer.Stop();
            Console.WriteLine(_timer.ElapsedTicks);
            _timer.Reset();
            Assert.AreEqual(ans1,ans2);
        }

        [TestMethod]
        public void TestSub()
        {
            _timer.Start();
            ans1 = (op1 - op2).ToString();
            _timer.Stop();
            Console.WriteLine(_timer.ElapsedTicks);
            _timer.Reset();

            _timer.Start();
            ans2 = (cop1 - cop2).ToString();
            _timer.Stop();
            Console.WriteLine(_timer.ElapsedTicks);
            _timer.Reset();
            Assert.AreEqual(ans1, ans2);
        }

        [TestMethod]
        public void TestMul()
        {
            _timer.Start();
            ans1 = (op1 * op2).ToString();
            _timer.Stop();
            Console.WriteLine(_timer.ElapsedTicks);
            _timer.Reset();

            _timer.Start();
            ans2 = (cop1 * cop2).ToString();
            _timer.Stop();
            Console.WriteLine(_timer.ElapsedTicks);
            _timer.Reset();
            Assert.AreEqual(ans1, ans2);
        }

        [TestMethod]
        public void TestDiv()
        {
            _timer.Start();
            ans1 = (op1 / op2).ToString();
            _timer.Stop();
            Console.WriteLine(_timer.ElapsedTicks);
            _timer.Reset();
            
            _timer.Start();
            ans2 = (cop1 / cop2).ToString();
            _timer.Stop();
            Console.WriteLine(_timer.ElapsedTicks);
            _timer.Reset();
            Assert.AreEqual(ans1, ans2);
        }

        [TestMethod]
        public void TestEqual()
        {
            _timer.Start();
            Assert.IsTrue(op1 == cop1);
            _timer.Stop();
            Console.WriteLine(_timer.ElapsedTicks);
            _timer.Reset();

            _timer.Start();
            Assert.IsTrue(op2 == cop2);
            _timer.Stop();
            Console.WriteLine(_timer.ElapsedTicks);
            _timer.Reset();
            
        }
    }
}
