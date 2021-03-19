using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Min.RuleEngine;

namespace Min.RuleEngine.UnitTest
{
    [TestClass]
    public class StaticMultiValueUnitTest
    {
        [DataTestMethod]
        [DataRow("true;", true)]
        [DataRow("false;", false)]
        [DataRow("true && true;", true)]
        [DataRow("true && false;", false)]
        [DataRow("false && false;", false)]
        [DataRow("1 > 2;", false)]
        [DataRow("1 < 2;", true)]
        [DataRow("1 > 2 && 3 > 4;", false)]
        [DataRow("1 > 2 || 3 > 4;", false)]
        [DataRow("1 > 2 && 3 < 4;", false)]
        [DataRow("1 > 2 || 3 < 4;", true)]
        [DataRow("1 > 2 && !(3 > 4);", false)]
        [DataRow("1 > 2 || !(3 > 4);", true)]
        [DataRow("1 > 2 && !(3 < 4);", false)]
        [DataRow("1 > 2 || !(3 < 4);", false)]
        [DataRow("(1 + 2) * 3 + 4 > 5 + 6 * 7;", false)]
        [DataRow("((1 + 2) * 3 + 4) > (5 + 6 * 7);", false)]
        [DataRow("(1 + 2) * 3 + 4 < 5 + 6 * 7;", true)]
        [DataRow("((1 + 2) * 3 + 4) < (5 + 6 * 7);", true)]
        [DataRow("(1 + 2) * 3 + 4 < 5 + 6 * 7 && 8 < 9;", true)]
        [DataRow("((1 + 2) * 3 + 4) < (5 + 6 * 7) || 8 > 9;", true)]
        public void TestBool(string expression, bool result)
        {
            var jRuleExpression = new RuleExpression<Func<bool>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function();

            Assert.AreEqual(result, result1);
        }

        [DataTestMethod]
        [DataRow("0 + 1;", 1)]
        [DataRow("0 - 1;", -1)]
        [DataRow("0 * 1;", 0)]
        [DataRow("0 / 1;", 0)]
        [DataRow("1 + 2 * 3 - 4 / 6;", 7)]
        [DataRow("1 + 2 * 3 - 4 / 6.0;", 6)]
        public void TestInt32(string expression, int result)
        {
            var jRuleExpression = new RuleExpression<Func<int>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function();

            Assert.AreEqual(result, result1);
        }

        [DataTestMethod]
        [DataRow("0 + 1.0;", 1)]
        [DataRow("0 - 1.0;", -1)]
        [DataRow("0 * 1.0;", 0)]
        [DataRow("0 / 1.0;", 0)]
        [DataRow("1 + 2 * 3 - 4 / 6;", 7)]
        [DataRow("1 + 2 * 3 - 3 / 6.0;", 7 - 3 / 6.0)]
        public void TestDecimal(string expression, double result)
        {
            var jRuleExpression = new RuleExpression<Func<decimal>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function();

            Assert.AreEqual((decimal)result, result1);
        }
    }
}
