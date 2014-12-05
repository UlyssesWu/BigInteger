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
        private List<int> _value;

        //private static BigInteger _one = new BigInteger("1");

        /// <summary>
        /// 零
        /// </summary>
        public static BigInteger Zero = new BigInteger("0");

        /// <summary>
        /// 原始数据表示
        /// </summary>
        public List<int> RawValue {
            get { return _value; }
        }

        #region 构造函数
        public BigInteger(string val = "0")
        {
            string va = Format(val);
            string v = (va[0] == '-') ? va.Substring(1) : va;
            //每四位取为一个int
            _value = new List<int>();
            try
            {
                for (int i = v.Length - 4; i > -4; i -= 4)
                {
                    if (i<0)
                    {
                        _value.Add(int.Parse(v.Substring(0, 4 + i)));
                    }
                    else
                    {
                        _value.Add(int.Parse(v.Substring(i, 4)));
                    }
                }
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(FormatException))
                {
                    throw new FormatException("未能分析该值。", e);
                }
                throw;
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
        #endregion

        #region 运算函数
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
            //op1.AddRange(new int[length - op1.Count]);
            //op2.AddRange(new int[length - op2.Count]);    //FIXED:修正补位错误：负数前补位补成了0而不是9999
            op1.AddRange(ArrayGenerator(length - op1.Count, this.IsNegative()));
            op2.AddRange(ArrayGenerator(length - op2.Count, that.IsNegative()));
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
                    //负数加法运算溢位得零 //FIXED:谁能告诉我我写的啥
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
            //op1.AddRange(new int[length - op1.Count]);
            //op2.AddRange(new int[length - op2.Count]);
            op1.AddRange(ArrayGenerator(length - op1.Count, this.IsNegative()));
            op2.AddRange(ArrayGenerator(length - op2.Count, that.IsNegative()));
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
                    result.Add(9998); //e.g. -200-900 = -1100
                }
                else
                {
                    //正数减法溢位为0 //FIXED:我这写的是啥
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

        /// <summary>
        /// 长整数 除法（int正数） 内部使用
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        private BigInteger Div(int that)
        {
            List<int> result = new List<int>();
            long remain = 0; //余数
            for (int i = _value.Count -1; i > -1; i--)
            {
                long tmp = _value[i] + remain;
                result.Add((int)(tmp / that));
                remain = (tmp%that) * 10000;    //为确保此步不会溢出，必须用long型
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
            //MARK:注意除法不会四舍五入，只会保留整数位
            if (that.IsZero())
            {
                throw new DivideByZeroException("除数不能为0。");
            }
            //如果除数为int型,直接除,效率高
            int parsed;
            //FIXED:除int之前要确保转被除数为正数
            if (int.TryParse(that.ToString(), out parsed))
            {
                if (this.IsNegative() || that.IsNegative())
                {
                    if (this.IsNegative() && !that.IsNegative())
                    {
                        return new BigInteger(
                            ToComplement(new BigInteger(ToComplement(this.RawValue)).Div(Math.Abs(parsed)).RawValue));
                    }
                    else if(!this.IsNegative() && that.IsNegative())
                    {
                        return new BigInteger(
                            ToComplement(Div(Math.Abs(parsed)).RawValue));
                    }
                    else
                    {
                        return new BigInteger(
                            new BigInteger(ToComplement(this.RawValue)).Div(Math.Abs(parsed)).RawValue);
                    }
                }
                return Div(Math.Abs(parsed));
            }
            
            //除数不是int型,暴力二分搜索
            //一律先以正数表示
            BigInteger op1 = IsNegative(_value) ? new BigInteger(ToComplement(_value)) : this;
            BigInteger op2 = IsNegative(that.RawValue) ? new BigInteger(ToComplement(that.RawValue)) : that;
            op1.Shorten();
            op2.Shorten();
            //暴力二分搜索之前的挣扎:除法转减法 //理论上根据Int64的最大值可以推算只要位数差距在17位以内均可以使用，但需要考虑哪种方案效率更高
            if (((op1.RawValue.Count > 127 || op2.RawValue.Count > 127) && (op1.ToString().Length - op2.ToString().Length < 10000 || op1.ToString().Length < op2.ToString().Length))) //只对超长数且位数接近者生效 对于被除数小于除数的情况均可用
            {
                long ans = 0;
                BigInteger remain = op1.Substract(op2);
                while (remain >= Zero)
                {
                    ans++;
                    remain = remain.Substract(op2);
                }

                return (GetLast(_value) + GetLast(that.RawValue) == 9999)
                ? new BigInteger("-" + ans.ToString())
                : new BigInteger(ans.ToString());
            }

            BigInteger one = new BigInteger("1");
            BigInteger left = new BigInteger("0");
            BigInteger right = op1;

            //二分法搜索
            while (right.GreaterOrEqual(left))
            {
                BigInteger x = left.Add(right).Div(2);
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

        public BigInteger Divide(int that)
        {
            if (that == 0)
            {
                throw new DivideByZeroException("除数不能为0。");
            }
            if ((that > 0 && this.IsNegative()) || (that < 0) && this.IsPositive())
            {
                return new BigInteger(ToComplement(Div(Math.Abs(that)).RawValue));
            }
            return Div(Math.Abs(that));
        }

        //TODO:MOD  ——是否可以实现取余数？
        //public BigInteger Divide(BigInteger that,out BigInteger remaining)
        //{
        //    if (that.IsZero())
        //    {
        //        throw new DivideByZeroException("除数不能为0。");
        //    }
        //    //一律先以正数表示
        //    BigInteger op1 = IsNegative(_value) ? new BigInteger(ToComplement(_value)) : this;
        //    BigInteger op2 = IsNegative(that.RawValue) ? new BigInteger(ToComplement(that.RawValue)) : that;
        //    op1.Shorten();
        //    op2.Shorten();
        //    long ans = 0;
        //    BigInteger remain = op1.Substract(op2);
        //    while (remain >= Zero)
        //    {
        //        ans++;
        //        remain = remain.Substract(op2);
        //    }
        //    remaining = remain; //BUG
        //    return (GetLast(_value) + GetLast(that.RawValue) == 9999)
        //    ? new BigInteger("-" + ans.ToString())
        //    : new BigInteger(ans.ToString());
        //}

        #endregion

        #region 辅助函数
        private bool GreaterOrEqual(BigInteger that)
        {
            return !IsNegative(Substract(that).RawValue);
        }

        /// <summary>
        /// 除法判断用
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        private bool IsLessOrEqualToQuotient(BigInteger op1, BigInteger op2)
        {
            return op1.GreaterOrEqual(Multiply(op2));
        }

        private static string Format(string input)
        {
            string preProcessed = input.Trim().Replace(" ", "").Replace(",", ""); //去掉空格和逗号
            if (preProcessed.Contains('.'))
            {
                for (int i = preProcessed.IndexOf('.') + 1; i < preProcessed.Length; i++)
                {
                    if (preProcessed[i] != '0')
                    {
                        throw new FormatException("无法处理小数。");
                    }
                }
                //小数位均为0，抹掉小数
            }
            bool isNegative = false;
            bool findValid = false;
            StringBuilder va = (preProcessed.Contains('.')) ? new StringBuilder(preProcessed.Split('.')[0]) : new StringBuilder(preProcessed);
            //去掉多余的运算符
            for (int i = 0; i < va.Length; i++)
            {
                switch (va[i])
                {
                    case '+':
                        va[i] = ' ';
                        break;
                    case '-':
                        isNegative = !isNegative;
                        va[i] = ' ';
                        break;
                    case ' ':
                        break;
                    default:
                        findValid = true;
                        break;
                }
                if (findValid)
                {
                    break;
                }
            }
            va = va.Replace(" ", "");
            if (isNegative)
            {
                va.Insert(0, '-');
            }
            return va.ToString();
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
            return IsNegative(_value) ? bigIntBuilder.Insert(0, '-').ToString() : bigIntBuilder.ToString();
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

        /// <summary>
        /// 是否为正值
        /// </summary>
        /// <returns></returns>
        private bool IsPositive()
        {
            return (GetLast(_value) == 0) && (!this.IsZero());
        }

        /// <summary>
        /// 是否为负值
        /// </summary>
        /// <returns></returns>
        public bool IsNegative()
        {
            return (GetLast(_value) == 9999);
        }

        /// <summary>
        /// 是否为零
        /// </summary>
        /// <returns></returns>
        public bool IsZero()
        {
            foreach (var i in _value)
            {
                if (i != 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 转换为补码表示
        /// </summary>
        /// <param name="value">源组</param>
        /// <returns></returns>
        private static List<int> ToComplement(List<int> value)
        {
            List<int> comp = new List<int>(value.Count);
            foreach (var i in value)
            {
                //tmp = 9999 - i;
                comp.Add(9999 - i);
                //((9999 - i)>=0)?comp.Add(9999 - i):comp.Add(19999 - i);
            }
            comp[0] = comp[0] + 1;
            int j = 0;
            //末位加1后的进位
            while (comp[j]>9999)
            {
                comp[j] = comp[j] - 10000;
                if (j + 1 < comp.Count)
                {
                    comp[j + 1] = comp[j + 1] + 1;
                    j++;
                }
            }
            return comp;
        }

        /// <summary>
        /// 生成填充用的数组
        /// </summary>
        /// <param name="length">数组长度</param>
        /// <param name="negative">是否为负数用</param>
        /// <returns></returns>
        private static int[] ArrayGenerator(int length, bool negative)
        {
            int[] ans = new int[length];
            for (int i = 0; i < length; i++)
            {
                ans[i] = negative ? 9999 : 0;
            }
            return ans;
        }


        /// <summary>
        /// 收缩源组
        /// </summary>
        private void Shorten()
        {
            int validCount = _value.Count;
            for (int i = _value.Count -1; i >= 0; i--)
            {
                if (_value[i] == 0 || _value[i] == 9999)
                {
                    validCount--;
                }
                else
                {
                    break;
                }
            }
            int valueLength = (validCount / 8 + 2) * 8;
            if (valueLength < _value.Count)
            {
                _value.RemoveRange(valueLength,_value.Count - valueLength);
            }
        }
        #endregion

        #region 运算符重载
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

        public static bool operator >(BigInteger op1, BigInteger op2)
        {
            return op1.Substract(op2).IsPositive();
        }

        public static bool operator >=(BigInteger op1, BigInteger op2)
        {
            var ans = op1.Substract(op2);
            return (ans.IsPositive() || ans.IsZero());
        }

        public static bool operator <(BigInteger op1, BigInteger op2)
        {
            return op1.Substract(op2).IsNegative();
        }

        public static bool operator <=(BigInteger op1, BigInteger op2)
        {
            var ans = op1.Substract(op2);
            return (ans.IsNegative() || ans.IsZero());
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
            //op1可能引发NullReferenceException.这里允许它抛出此异常
// ReSharper disable once PossibleNullReferenceException
            if (op1.RawValue != null)
            {return op1.Equals(op2);}
            return op2 == null;
        }

        public static bool operator !=(BigInteger op1, object op2)
        {
            return !(op1 == op2);
        }
        #endregion
    }
}
