using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uly.Numerics
{
    /// <summary>
    /// 长整数
    /// </summary>
    public class BigInteger
    {
        //private const int DIVIDE_LENGTH = 4;
        //private int _blockMaxNumber = (int)Math.Pow(10, DIVIDE_LENGTH);
        private List<int> _value;

        //private static BigInteger _one = new BigInteger("1");

        public List<int> RawValue {
            get { return _value; }
            set { _value = value; }
        }

        public BigInteger(string val)
        {
            string va = val.Trim().Replace(" ","").Replace(",",""); //去掉空格和逗号
            string v = (va[0] == '-') ? va.Substring(1) : va;
            //每四位取为一个int
            _value = new List<int>();
            try
            {
                for (int i = v.Length - 4; i > -4; i -= 4)
                {
                    if (i<0)
                    {
                        _value.Add(int.Parse(v.Substring(Math.Max(i, 0), 4 + i)));
                    }
                    else
                    {
                        _value.Add(int.Parse(v.Substring(Math.Max(i, 0), 4)));
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            //将位数补为8的整数倍
            int valueLength = (_value.Count/8 + 1)*8; //Count相当于Java中的size()
            for (int i = _value.Count; i < valueLength; i++)
            {
                _value.Add(0);
            }
            //负数转为补码表示
            if (va[0] == '-')   //注意
            {
                _value = ToComplement(_value);
            }
        }

        private BigInteger(List<int> value)
        {
            this._value = value;
            this.Shorten();
        }

        /// <summary>
        /// 长整数 加法
        /// </summary>
        /// <param name="that">加数</param>
        /// <returns></returns>
        public BigInteger Add(BigInteger that)
        {
            if (IsNegative(that.RawValue))
            {
                //转为减法
                return Substract(new BigInteger(ToComplement(that.RawValue)));
            }
            //对齐位数
            this.Shorten();
            that.Shorten();
            int length = Math.Max(_value.Count, that.RawValue.Count); //最大长度
            List<int> op1 = new List<int>(_value);
            List<int> op2 = new List<int>(that.RawValue);
            op1.AddRange(new int[length - op1.Count]);
            op2.AddRange(new int[length - op2.Count]);
            List<int> result = new List<int>();
            
            int carry = 0; //进位
            for (int i = 0; i < length - 1; i++)
            {
                int c = op1[i] + op2[i] + carry;
                if (c < 10000)
                {
                    carry = 0;
                }
                else
                {
                    c -= 10000;
                    carry = 1;
                }
                result.Add(c);
            }
            if (carry == 1) //进位
            {
                if (IsPositive(op1))
                {
                    result.Add(1);
                }
                else
                {
                    result.Clear(); //负数加法运算溢位得零
                }
                for (int i = 0; i < 8; i++)
                {
                    result.Add(0);
                }
            }
            else
            {
                //补位，正数补0，负数补9999
                result.Add(IsPositive(op1) ? 0 : 9999);
            }
            return new BigInteger(result);
        }

        /// <summary>
        /// 长整数 减法
        /// </summary>
        /// <param name="that">减数</param>
        /// <returns></returns>
        public BigInteger Substract(BigInteger that)
        {
            if (IsNegative(that.RawValue))
            {
                return Add(new BigInteger(ToComplement(that.RawValue)));
            }
            //对齐位数
            this.Shorten();
            that.Shorten();
            int length = Math.Max(_value.Count, that.RawValue.Count);
            List<int> op1 = new List<int>(_value);
            List<int> op2 = new List<int>(that.RawValue);
            op1.AddRange(new int[length - op1.Count]);
            op2.AddRange(new int[length - op2.Count]);
            List<int> result = new List<int>();

            int borrow = 0; //借位
            for (int i = 0; i < length - 1; i++)
            {
                int c = op1[i] - op2[i] - borrow;
                if (c >= 0)
                {
                    borrow = 0;
                }
                else
                {
                    c += 10000;//负值转正
                    borrow = 1;//有借位
                }
                result.Add(c);
            }
            if (borrow == 1) //借位处理
            {
                if (IsNegative(op1))
                {
                    result.Add(9998); //MARK
                }
                else
                {
                    result.Clear(); //正数减法溢位为0
                }
                for (int i = 0; i < 8; i++)
                {
                    result.Add(9999); //增加8位
                }
            }
            else
            {
                //补位，负数补9999,正数补0
                result.Add(IsNegative(op1) ? 9999 : 0);
            }

            return new BigInteger(result);
        }
        
        /// <summary>
        /// 长整数 块乘法 中间运算使用
        /// </summary>
        /// <param name="val">乘数</param>
        /// <param name="shift">移位</param>
        /// <returns></returns>
        private BigInteger Multiply(int val, int shift) //内部使用，两个参数都应为正数
        {
            List<int> result = new List<int>();
            for (int i = 0; i < shift; i++)
            {
                result.Add(0); //移位补0
            }
            int carry = 0;
            for (int i = 0; i < _value.Count - 1; i++)
            {
                int tmp = _value[i]*val + carry;
                result.Add(tmp % 10000);
                carry = tmp/10000;
            }
            if (carry != 0)
            {
                result.Add(carry);
                for (int i = 0; i < 8; i++)
                {
                    result.Add(0);
                }
            }
            else
            {
                result.Add(0);
            }
            return new BigInteger(result);
        }

        /// <summary>
        /// 长整数 乘法
        /// </summary>
        /// <param name="that">乘数</param>
        /// <returns></returns>
        public BigInteger Multiply(BigInteger that)
        {
            //负数 转为补码表示
            BigInteger op1 = IsNegative(_value) ? new BigInteger(ToComplement(_value)) : this;
            List<int> op2 = IsNegative(that.RawValue) ? ToComplement(that.RawValue) : that.RawValue;
            //逐位运算
            List<BigInteger> rs = new List<BigInteger>(); //中间结果
            for (int i = 0; i < op2.Count - 1; i++)
            {
                rs.Add(op1.Multiply(op2[i],i));
            }
            //对逐位运算的结果进行加和
            BigInteger result = rs[0];
            for (int i = 1; i < rs.Count; i++)
            {
                result = result.Add(rs[i]);
            }
            //判断正负数
            return (GetLast(_value) + GetLast(that.RawValue)) == 9999
                ? new BigInteger(ToComplement(result.RawValue))
                : result;
        }

        private bool GreaterOrEqual(BigInteger that)
        {
            return IsNegative(Substract(that).RawValue) ? false : true;
        }

        private bool IsLessOrEqualToQuotient(BigInteger op1, BigInteger op2)
        {
            return op1.GreaterOrEqual(Multiply(op2));
        }

        private BigInteger Divide(int that)
        {
            List<int> result = new List<int>();
            int remain = 0; //余数
            for (int i = _value.Count -1; i > -1; i--)
            {
                int tmp = _value[i] + remain;
                result.Add(tmp / that);
                remain = (tmp%that) * 10000;
            }
            result.Reverse();
            for (int i = 0; i < 8 - (result.Count) % 8; i++)
            {
                result.Add(0);
            }
            return new BigInteger(result);
        }

        /// <summary>
        /// 长整数 除法
        /// </summary>
        /// <param name="that">除数</param>
        /// <returns></returns>
        public BigInteger Divide(BigInteger that)
        {
            //一律先以正数表示
            BigInteger op1 = IsNegative(_value) ? new BigInteger(ToComplement(_value)) : this;
            BigInteger op2 = IsNegative(that.RawValue) ? new BigInteger(ToComplement(that.RawValue)) : that;
            BigInteger one = new BigInteger("1");
            BigInteger left = new BigInteger("0");
            BigInteger right = op1;

            //二分法搜索
            while (right.GreaterOrEqual(left))
            {
                BigInteger x = left.Add(right).Divide(2);
                if (x.IsLessOrEqualToQuotient(op1,op2))
                {
                    left = x.Add(one);
                }
                else
                {
                    right = x.Substract(one);
                }
            }
            right = left.Substract(one);
            return (GetLast(_value) + GetLast(that.RawValue) == 9999)
                ? new BigInteger(ToComplement(right.RawValue))
                : right;
        }

        public override string ToString()
        {
            List<int> v = IsNegative(_value) ? ToComplement(_value) : _value;
            StringBuilder bigIntBuilder = new StringBuilder();
            for (int i = v.Count - 1; i > -1; i--)
            {
                bigIntBuilder.Append(v[i].ToString("D4"));
            }
            //移去最前端的0，负数加负号
            while (bigIntBuilder.Length > 0 && bigIntBuilder[0] == '0')
            {
                bigIntBuilder.Remove(0, 1);
            }
            if (bigIntBuilder.Length == 0)
            {
                return "0";
            }
            if (IsNegative(_value))
            {
                return bigIntBuilder.Insert(0, '-').ToString();
            }
            return bigIntBuilder.ToString();
        }

        private static int GetLast(List<int> value)
        {
            return value[value.Count - 1];
        }

        private static bool IsPositive(List<int> list)
        {
            return (GetLast(list) == 0);
        }

        private static bool IsNegative(List<int> list)
        {
            return (GetLast(list) == 9999);
        }

        public bool IsNegative()
        {
            return (GetLast(_value) == 9999);
        }
        
        /// <summary>
        /// 转换为补码表示
        /// </summary>
        /// <param name="value">源组</param>
        /// <returns></returns>
        private static List<int> ToComplement(List<int> value)
        {
            List<int> comp = new List<int>();
            foreach (var i in value)
            {
                comp.Add(9999 - i);
            }
            comp[0] = comp[0] + 1;
            return comp;
        }

        /// <summary>
        /// 收缩源组
        /// </summary>
        private void Shorten()
        {
            int validCount = _value.Count;
            for (int i = _value.Count -1; i >= 0; i--)
            {
                if (_value[i]==0)
                {
                    validCount--;
                }
                else
                {
                    break;
                }
            }
            int valueLength = (validCount / 8 + 1) * 8;
            if (valueLength < _value.Count)
            {
                _value.RemoveRange(valueLength,_value.Count - valueLength);
            }
        }

        public static BigInteger operator +(BigInteger op1, BigInteger op2)
        {
            return op1.Add(op2);
        }

        public static BigInteger operator -(BigInteger op1, BigInteger op2)
        {
            return op1.Substract(op2);
        }

        public static BigInteger operator *(BigInteger op1, BigInteger op2)
        {
            return op1.Multiply(op2);
        }

        public static BigInteger operator /(BigInteger op1, BigInteger op2)
        {
            return op1.Divide(op2);
        }

        public override bool Equals(object obj)
        {
            return (this.ToString() == obj.ToString());
            //return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public static bool operator ==(BigInteger op1, object op2)
        {
            if (op1.RawValue != null)
            {return op1.Equals(op2);}
            return op2 == null;
        }

        public static bool operator !=(BigInteger op1, object op2)
        {
            return !(op1 == op2);
        }
    }
}
