using System;
using System.Collections.Generic;

using System.Text;
using System.Text.RegularExpressions;
namespace 公路养护工程能耗计算软件ECMS.Calculator
{
    //a.建立两个栈：第一个位数字栈，第二个位算符栈！(将栈定义为char类型)

    //b.对数字来说是无条件压入数字栈中.

    //c.而对符号来说，只有当前栈顶元素的优先值小于扫到的符号时（比如”+”小于”*”），此符号才压入栈;否则大于等于的情况是将当前栈顶元素弹出栈，与当前数字栈的前两个数字组成式子进行计算.计算结果当作数字压入数字栈作为栈顶元素（要舍弃已经弹出的两个数字），而那个扫描到的符号则将代替那个弹出的符号作为栈顶元素）。

    //d.最后说一下括号，原则是扫描到左括号时无条件压入符号栈，而扫到右括号时，则弹出离栈顶最近的一个左括号以上的全部符号与数字栈的数字做运算


    //不支持 -（）这种情况  比如 6*-(sdafafdfadsfadfd)
    public interface IAlgorithm
    {

        /// <summary>
        /// 计算出表达式expr的值
        /// </summary>
        /// <param name="expr">要计算的四则运算表达式</param>
        /// <returns></returns>
        double Calculate(string expr);
        /// <summary>
        /// 向外界返回算法的名字
        /// </summary>
        /// <returns></returns>

        string GetAlgorithmName();
    }
    class Algorithm : IAlgorithm
    {
        #region IAlgorithm 成员
        /// <summary>
        /// 计算数学表达式字符串的方法
        /// </summary>
        /// <param name="expr">字符串数学表达式</param>
        /// <returns></returns>

        double IAlgorithm.Calculate(string expr)
        {
            //分离操作数 和操作符 返回 字符串数组
            string[] strlist = PreConvertStrList(ref expr);

            string result = CalCulateExpresionList(strlist);

            return Convert.ToDouble(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strlist"></param>
        /// <returns></returns>
        private static string CalCulateExpresionList(string[] strlist)
        {
            #region  //计算表达式
            Stack<string> OperatorStack = new Stack<string>();//操作符栈
            Stack<string> operandStack = new Stack<string>();//操作数栈
            string result = "";
            for (int i = 0; i < strlist.Length; i++)
            {
                if (AlgorithmHelper.IsOperator(strlist[i]))//判断是否是操作符
                {
                    if (OperatorStack.Count == 0)
                    {
                        OperatorStack.Push(strlist[i]);//第一次操作运算符入栈
                    }
                    else if (AlgorithmHelper.IsASameOrHeigherThanB(OperatorStack.Peek(), strlist[i]))//计算 1
                    {
                        //1.if 当前操作符栈栈顶元素的优先值大于等于扫到的符号时 执行步骤2 else 执行步骤4
                        //2.当前操作符栈栈顶元素弹出栈与当前操作数栈栈的前两个数字组成式子进行计算 （操作数栈舍弃已经弹出的两个数字 操作符栈舍弃已经弹出的操作符）
                        //3.计算结果当作数字压入操作数栈作为栈顶元素 执行步骤1
                        //4.扫到的符号压入操作符栈
                        do
                        {
                            result = AlgorithmHelper.Evaluate(operandStack.Pop(), operandStack.Pop(), OperatorStack.Pop()).ToString();//2
                            operandStack.Push(result);//3
                        }
                        while (OperatorStack.Count > 0 && AlgorithmHelper.IsASameOrHeigherThanB(OperatorStack.Peek(), strlist[i]));//1
                        OperatorStack.Push(strlist[i]);//4
                    }
                    else if (strlist[i] == ")")//扫到右括号时，则弹出离栈顶最近的一个左括号以上的全部符号与数字栈的数字做运算
                    {
                        //1.if 当前操作符栈栈顶元素不等于"("时 执行步骤2 else 执行步骤4
                        //2.当前操作符栈栈顶元素弹出栈与当前操作数栈栈的前两个数字组成式子进行计算 （操作数栈舍弃已经弹出的两个数字 操作符栈舍弃已经弹出的操作符）
                        //3.计算结果当作数字压入操作数栈作为栈顶元素 执行步骤1
                        //4.舍弃操作符栈栈顶的操作符（实际上此元素为"("）
                        do
                        {
                            result = AlgorithmHelper.Evaluate(operandStack.Pop(), operandStack.Pop(), OperatorStack.Pop()).ToString();//2
                            operandStack.Push(result);//计算结果压入数据栈3
                        }
                        while (OperatorStack.Peek() != "(");//1
                        OperatorStack.Pop();//4

                    }
                    else
                    {
                        OperatorStack.Push(strlist[i]);//操作符入栈
                    }
                }
                else
                {
                    operandStack.Push(strlist[i]);//操作数入栈
                }
            }

            while (OperatorStack.Count > 0)
            {
                result = AlgorithmHelper.Evaluate(operandStack.Pop(), operandStack.Pop(), OperatorStack.Pop()).ToString();
                operandStack.Push(result);//计算结果压入数据栈
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 分离操作数 和操作符 返回 字符串数组
        /// </summary>
        /// <param name="expr">表达式字符串</param>
        /// <returns>返回分离后的字符串数组</returns>
        private static string[] PreConvertStrList(ref string expr)
        {
            //            捕获 
            //(exp) 匹配exp并且在一个自动计数的分组中捕获它 
            //(?<name>exp) 匹配exp并且在一个命名的分组中捕获它

            //(?:exp) 匹配exp并且不捕获它 
            //察看 
            //(?=exp) 匹配任何后缀exp之前的位置 
            //(?<=exp) 匹配任何前缀exp之后的位置 
            //(?!exp) 匹配任何未找到的后缀exp之后的位置 
            //(?<!exp) 匹配任何未找到的前缀exp之前的位置 
            #region //消除空白字符  后面有替换符号的情况  需要对负号和正号 进行预处理
            expr = Regex.Replace(expr, "\\s", "");//消除空白字符
            //把负号-替换为&
            expr = Regex.Replace(expr, "(?<=[+,-,\\*,/]{1})[-]{1}(?=\\d+)", "&");
            //把正号+替换为@
            expr = Regex.Replace(expr, "(?<=[+,-,\\*,/]{1})[+]{1}(?=\\d+)", "@", RegexOptions.RightToLeft);
            #endregion

            StringBuilder sb = new StringBuilder();
            sb.Append(expr);
            #region //begin处理第一种情况为负号或正号的情况
            if (sb[0] == '-' && !AlgorithmHelper.IsOperator(sb[2].ToString()))
            {
                sb[0] = '&';
            }
            else if (sb[0] == '+' && !AlgorithmHelper.IsOperator(sb[2].ToString()))
            {
                sb[0] = '@';
            }
            #endregion
            #region//为了把操作数和操作符分离 需添加"$" 后面辅助处理
            sb.Replace("+", "$+$");
            sb.Replace("-", "$-$");
            sb.Replace("*", "$*$");
            sb.Replace("/", "$/$");
            sb.Replace("(", "$($");
            sb.Replace(")", "$)$");
            #endregion
            #region//还原负号 正号  分离操作数和操作符 返回字符数组
            sb.Replace("&", "-");//替换为负数符号-
            sb.Replace("@", "+");//替换为正数符号+
            //用分隔符$ 把操作数和操作符分离 返回字符串数组
            string[] strlist = sb.ToString().Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
            #endregion
            return strlist;
        }

        string IAlgorithm.GetAlgorithmName()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
