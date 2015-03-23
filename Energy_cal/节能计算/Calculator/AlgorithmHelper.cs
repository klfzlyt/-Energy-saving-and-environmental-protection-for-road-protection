using System;
using System.Collections.Generic;
using System.Text;

namespace 公路养护工程能耗计算软件ECMS.Calculator
{
    /// <summary>
    /// 此类用于存放在算法中用到的一些公用函数
    /// </summary>
    public class AlgorithmHelper
    {

        /// <summary>
        /// 判断某字符是否是运算符,是则返回true，其余非法字符返回false
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsOperator(string c)
        {
            InitializeOperands();
            return operands.ContainsKey(c);
        }
        private static Dictionary<string, int> operands = new Dictionary<string, int>();

        /// <summary>
        /// 判断两运算符A和B的优先级谁高谁低，A优先于B，返回true，其余情况（优先级相等或低），返回false
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool IsAHeigherThanB(string A, string B)
        {
            if (AlgorithmHelper.IsOperator(A) == false || AlgorithmHelper.IsOperator(B) == false)
                throw new Exception("传入的字符不是合法的运算符。");

            InitializeOperands();

            return operands[A] > operands[B];

        }
        /// <summary>
        /// 初始化运算符优先级集合
        /// </summary>
        private static void InitializeOperands()
        {
            if (operands.Count == 0)
            {
                operands.Add("+", 0);
                operands.Add("-", 0);
                operands.Add("*", 1);
                operands.Add("/", 1);
                operands.Add("(", 2);
                operands.Add(")", 2);
            }
        }

        /// <summary>
        /// 判断两运算符A和B的优先级谁高谁低，A优先于B或与B相等，返回true
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool IsASameOrHeigherThanB(string A, string B)
        {
            if (A == "(")
            {
                return false;
            }
            if (AlgorithmHelper.IsOperator(A) == false || AlgorithmHelper.IsOperator(B) == false)
                throw new Exception("传入的字符不是合法的运算符。");
            InitializeOperands();
            return operands[A] >= operands[B];
        }


        /// <summary>
        /// 根据两个操作数和运算符,计算其结果
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="Operators"></param>
        /// <returns></returns>
        public static double Evaluate(string yStr, string xStr, string Operators)
        {
            double x = Convert.ToDouble(xStr);
            double y = Convert.ToDouble(yStr);
            double result = 0;
            switch (Operators)
            {
                case "+":
                    result = x + y;
                    break;
                case "-":
                    result = x - y;
                    break;
                case "*":
                    result = x * y;
                    break;
                case "/":
                    result = x / y;
                    break;

            }

            return result;
        }

    }

}