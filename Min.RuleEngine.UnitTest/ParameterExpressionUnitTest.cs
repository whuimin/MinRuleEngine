using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Min.RuleEngine;

namespace Min.RuleEngine.UnitTest
{
    [TestClass]
    public class ParameterExpressionUnitTest
    {
        [DataTestMethod]
        [DataRow("int0 * decimal1;", 12, 34.56, 12 * 34.56)]
        public void TestParameter(string expression, int p1, double p2, double result)
        {
            var jRuleExpression = new RuleExpression<Func<int, decimal, decimal>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function(p1, (decimal)p2);

            Assert.AreEqual((decimal)result, result1);
        }

    }
}
