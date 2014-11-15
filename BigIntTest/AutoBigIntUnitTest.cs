//#define ONLY_TARGET 加入此句则只测试目标库
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uly.Numerics;
using MSBigInt = System.Numerics.BigInteger;
using System.Diagnostics;
using System.IO;

namespace BigIntTest
{
    /// <summary>
    /// 批量单元测试
    /// </summary>
    [TestClass]
    public class AutoBigIntUnitTest
    {
        BigInteger op1,op2;
        MSBigInt cop1,cop2;
        //List<BigInteger> op1s = new List<BigInteger>();
        //List<BigInteger> op2s = new List<BigInteger>();
        //List<MSBigInt> cop1s = new List<MSBigInt>();
        //List<MSBigInt> cop2s = new List<MSBigInt>();
        private List<string> info = new List<string>();
        private List<string> iop1s = new List<string>();
        private List<string> iop2s = new List<string>(); 
        private string ans1, ans2;

        private Stopwatch _timer = new Stopwatch();
        private long _nanoSecPerTick;
        [TestInitialize]
        public void Init()
        {
            string[] inputs = File.ReadAllLines("TestInput.txt");
            for (int i = 0; i < inputs.Length; i=i+3)
            {
                if (inputs[i].StartsWith("//"))
                {
                    continue;
                }
                info.Add(inputs[i]);
                //op1s.Add(new BigInteger(inputs[i + 1]));
                //cop1s.Add(MSBigInt.Parse(inputs[i + 1]));
                iop1s.Add(inputs[i + 1]);
                //op2s.Add(new BigInteger(inputs[i + 2]));
                //cop2s.Add(MSBigInt.Parse(inputs[i + 2]));
                iop2s.Add(inputs[i + 2]);
            }
            _timer.Reset();
            _nanoSecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            Console.WriteLine("时间单位：Tick（每Tick为 " + _nanoSecPerTick + " 纳秒）");
            Console.WriteLine("1毫秒 = " + 1000000/_nanoSecPerTick + " Tick");
        }

        /// <summary>
        /// 检查构造阶段是否能抛出正确的异常
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int CheckInput(int i)
        {
            bool msFailed = false;
            bool ulyFailed = false;
            Exception msException = null;
            Exception ulyException = null;
            Console.WriteLine("op1:" + iop1s[i]);
            Console.WriteLine("op2:" + iop2s[i]);
            try
            {
                cop1 = MSBigInt.Parse(iop1s[i]);
                cop2 = MSBigInt.Parse(iop2s[i]);
            }
            catch (Exception e)
            {
                msException = e;
                msFailed = true;
                Console.WriteLine("System.Numerics.BigInteger抛出解析异常:{0}", e.Message);
            }
            try
            {
                op1 = new BigInteger(iop1s[i]);
                op2 = new BigInteger(iop2s[i]);
            }
            catch (Exception e)
            {
                ulyException = e;
                ulyFailed = true;
                Console.WriteLine("Uly.Numerics.BigInteger抛出解析异常:{0}", e.Message);
            }
            if (ulyFailed)
            {
                if (!msFailed)
                {
                    //微软能正常解析而本库不能,直接失败
                    Assert.Fail("目标库的行为与对照组不一致，于第{0}组测试失败:{1}", i, info[i]);
                }
                else
                {
                    //两种库都不能解析,必须抛出相同的异常
                    //Assert.AreSame(msException, ulyException);
                    Assert.AreSame(msException.GetType(), ulyException.GetType(), string.Format("目标库的抛出的异常与对照组不一致，于第{0}组测试失败:{1}", i, info[i]));
                    if (msException.GetType() == ulyException.GetType())
                    {
                        Console.WriteLine("目标库与对照组抛出了相同的异常，测试成功:{0} VS {1}", ulyException.Message, msException.Message);
                        return -1;  //跳过此组测试的剩余内容
                    }
                    Assert.Fail("目标库的行为与对照组不一致，于第{0}组测试失败:{1}", i, info[i]);
                }
            }
            else
            {
                if (msFailed)
                {
                    //本库能解析而微软库不能
                    Console.WriteLine("对照组无法解析此组数据！尝试将目标库解析后的数据赋值给对照组...");
                    try
                    {
                        cop1 = MSBigInt.Parse(op1.ToString());
                        cop2 = MSBigInt.Parse(op2.ToString());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("对照组已经无法解析此组数据！缺乏对照组，本轮测试将跳过!");
                        return 0;//只使用目标库运算
                    }
                }
            }
#if ONLY_TARGET
            return 0; //只测试目标库
#endif
            return 1; //两组数据均没有问题
        }

        [TestMethod]
        public void TestAdd()
        {
            for (int i = 0; i < info.Count; i++)
            {
                Console.WriteLine("[正在执行加法测试]" + info[i]);
                int flag = CheckInput(i);
                if (flag>=0)
                {
                    _timer.Start();
                    ans1 = (op1 + op2).ToString();
                    _timer.Stop();
                    Console.WriteLine("目标库用时:" + _timer.ElapsedTicks);
                    _timer.Reset();
                }
                if (flag > 0)
                {
                    _timer.Start();
                    ans2 = (cop1 + cop2).ToString();
                    _timer.Stop();
                    Console.WriteLine("对照组用时:" + _timer.ElapsedTicks);
                    _timer.Reset();
                    Assert.AreEqual(ans2, ans1);
                }
                if (flag == 0)
                {
                    Console.WriteLine("对照组表示看不懂这组数据");
                }
            }
        }

        [TestMethod]
        public void TestSub()
        {
            for (int i = 0; i < info.Count; i++)
            {
                Console.WriteLine("[正在执行减法测试]" + info[i]);
                int flag = CheckInput(i);
                if (flag >= 0)
                {
                    _timer.Start();
                    ans1 = (op1 - op2).ToString();
                    _timer.Stop();
                    Console.WriteLine("目标库用时:" + _timer.ElapsedTicks);
                    _timer.Reset();
                }
                if (flag > 0)
                {
                    _timer.Start();
                    ans2 = (cop1 - cop2).ToString();
                    _timer.Stop();
                    Console.WriteLine("对照组用时:" + _timer.ElapsedTicks);
                    _timer.Reset();
                    Assert.AreEqual(ans2, ans1);
                }
                if (flag == 0)
                {
                    Console.WriteLine("对照组表示看不懂这组数据");
                }
            }
        }

        [TestMethod]
        public void TestMul()
        {
            for (int i = 0; i < info.Count; i++)
            {
                Console.WriteLine("[正在执行乘法测试]" + info[i]);
                int flag = CheckInput(i);
                if (flag >= 0)
                {
                    _timer.Start();
                    ans1 = (op1 * op2).ToString();
                    _timer.Stop();
                    Console.WriteLine("目标库用时:" + _timer.ElapsedTicks);
                    _timer.Reset();
                }
                if (flag > 0)
                {
                    _timer.Start();
                    ans2 = (cop1 * cop2).ToString();
                    _timer.Stop();
                    Console.WriteLine("对照组用时:" + _timer.ElapsedTicks);
                    _timer.Reset();
                    Assert.AreEqual(ans2, ans1);
                }
                if (flag == 0)
                {
                    Console.WriteLine("对照组表示看不懂这组数据");
                }
            }
        }

        [TestMethod]
        public void TestDiv()
        {
            Exception ulyException;
            for (int i = 0; i < info.Count; i++)
            {
                ulyException = null;
                Console.WriteLine("[正在执行除法测试]" + info[i]);
                int flag = CheckInput(i);
                try
                {
                    if (flag >= 0)
                    {
                        _timer.Start();
                        ans1 = (op1 / op2).ToString();
                        _timer.Stop();
                        Console.WriteLine("目标库用时:" + _timer.ElapsedTicks);
                        _timer.Reset();
                    }
                }
                catch (Exception e)
                {
                    ulyException = e;
                    Console.WriteLine("Uly.Numerics.BigInteger抛出了异常:{0}", e.Message);
                    Assert.IsInstanceOfType(e,typeof(DivideByZeroException),"抛出的异常不是除以零异常");
                }
                try
                {
                    if (flag > 0)
                    {
                        _timer.Start();
                        ans2 = (cop1 / cop2).ToString();
                        _timer.Stop();
                        Console.WriteLine("对照组用时:" + _timer.ElapsedTicks);
                        _timer.Reset();
                        Assert.AreEqual(ans2, ans1);
                    }
                }
                catch (Exception e)
                {
                    if (e is AssertFailedException)
                    {
                        throw e;
                    }
                    if (ulyException != null)
                    {
                        Assert.AreSame(e.GetType(),ulyException.GetType());//必须抛出一样的异常
                    }
                    Console.WriteLine("System.Numerics.BigInteger抛出了异常:{0}", e.Message);
                }
                if (flag == 0)
                {
                    Console.WriteLine("对照组表示看不懂这组数据");
                }
            }
        }

        [TestMethod]
        public void TestEqual()
        {
            for (int i = 0; i < info.Count; i++)
            {
                Console.WriteLine("[正在执行相等测试]" + info[i]);
                int flag = CheckInput(i);
                if (flag > 0)
                {
                    _timer.Start();
                    Assert.IsTrue(op1 == cop1);
                    _timer.Stop();
                    Console.WriteLine("第一组用时:" + _timer.ElapsedTicks);
                    _timer.Reset();

                    _timer.Start();
                    Assert.IsTrue(op2 == cop2);
                    _timer.Stop();
                    Console.WriteLine("第二组用时:" + _timer.ElapsedTicks);
                    _timer.Reset();
                }
                else
                {
                    Console.WriteLine("本组已经跳过");
                }
            }
        }

        [TestMethod]
        public void TestGreaterLess()
        {
            for (int i = 0; i < info.Count; i++)
            {
                Console.WriteLine("[正在执行大于小于测试]" + info[i]);
                int flag = CheckInput(i);
                if (flag > 0)
                {
                    _timer.Start();
                    Assert.IsFalse((op1 > op2) ^ (cop1 > cop2)); //异或，两者相同则为False
                    _timer.Stop();
                    Console.WriteLine("第一组用时:" + _timer.ElapsedTicks);
                    _timer.Reset();

                    _timer.Start();
                    Assert.IsFalse((op1 < op2) ^ (cop1 < cop2)); //异或，两者相同则为False
                    _timer.Stop();
                    Console.WriteLine("第二组用时:" + _timer.ElapsedTicks);
                    _timer.Reset();
                }
                else
                {
                    Console.WriteLine("本组已经跳过");
                }
            }
        }
    }
}
