using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Min.RuleEngine;

namespace Min.RuleEngine.UnitTest
{
    [TestClass]
    public class StaticSingleValueUnitTest
    {
        [DataTestMethod]
        [DataRow("true;", true)]
        [DataRow("false;", false)]
        public void TestBool(string expression, bool result)
        {
            var jRuleExpression = new RuleExpression<Func<bool>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function();

            Assert.AreEqual(result, result1);
        }

        [DataTestMethod]
        [DataRow("\"abcde\";", "abcde")]
        [DataRow("\"规则表达式\";", "规则表达式")]
        public void TestString(string expression, string result)
        {
            var jRuleExpression = new RuleExpression<Func<string>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function();

            Assert.AreEqual(result, result1);
        }

        [DataTestMethod]
        [DataRow("0;", (short)0)]
        [DataRow("1;", (short)1)]
        [DataRow("-1;", (short)-1)]
        [DataRow("32767;", (short)32767)]
        [DataRow("-32768;", (short)-32768)]
        public void TestInt16(string expression, short result)
        {
            var jRuleExpression = new RuleExpression<Func<short>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function();

            Assert.AreEqual(result, result1);
        }

        [DataTestMethod]
        [DataRow("0;", (int)0)]
        [DataRow("1;", (int)1)]
        [DataRow("-1;", (int)-1)]
        [DataRow("2147483647;", (int)2147483647)]
        [DataRow("-2147483648;", (int)-2147483648)]
        public void TestInt32(string expression, int result)
        {
            var jRuleExpression = new RuleExpression<Func<int>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function();

            Assert.AreEqual(result, result1);
        }

        [DataTestMethod]
        [DataRow("0;", (long)0)]
        [DataRow("1;", (long)1)]
        [DataRow("-1;", (long)-1)]
        [DataRow("9223372036854775807;", (long)9223372036854775807)]
        [DataRow("-9223372036854775808;", (long)-9223372036854775808)]
        public void TestInt64(string expression, long result)
        {
            var jRuleExpression = new RuleExpression<Func<long>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function();

            Assert.AreEqual(result, result1);
        }

        [DataTestMethod]
        [DataRow("0;", (float)0)]
        [DataRow("1;", (float)1)]
        [DataRow("-1;", (float)-1)]
        [DataRow("1.23;", (float)1.23)]
        [DataRow("-1.23;", (float)-1.23)]
        public void TestSingle(string expression, float result)
        {
            var jRuleExpression = new RuleExpression<Func<float>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function();

            Assert.AreEqual(result, result1);
        }

        [DataTestMethod]
        [DataRow("0;", (double)0)]
        [DataRow("1;", (double)1)]
        [DataRow("-1;", (double)-1)]
        [DataRow("1.23;", (double)1.23)]
        [DataRow("-1.23;", (double)-1.23)]
        public void TestDouble(string expression, double result)
        {
            var jRuleExpression = new RuleExpression<Func<double>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function();

            Assert.AreEqual(result, result1);
        }

        [DataTestMethod]
        [DataRow("0;", 0)]
        [DataRow("1;", 1.0)]
        [DataRow("-1;", -1.0)]
        [DataRow("1.23;", 1.23)]
        [DataRow("-1.23;", -1.23)]
        public void TestDecimal(string expression, double result)
        {
            var jRuleExpression = new RuleExpression<Func<decimal>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function();

            Assert.AreEqual((decimal)result, result1);
        }
    }
}
