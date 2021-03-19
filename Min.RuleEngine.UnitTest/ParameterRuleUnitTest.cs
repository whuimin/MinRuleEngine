using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Min.RuleEngine;
using Min.RuleEngine.UnitTest.Model;

namespace Min.RuleEngine.UnitTest
{
    [TestClass]
    public class ParameterRuleUnitTest
    {
        [DataTestMethod]
        [DataRow("when(int0 > 10) then(int0 + decimal1);when(int0 < 10) then(int0 * decimal1);else(int0 - decimal1);", 12, 34.56, 12 + 34.56)]
        [DataRow("when(int0 > 10) then(int0 + decimal1);when(int0 < 10) then(int0 * decimal1);else(int0 - decimal1);", 2, 34.56, 2 * 34.56)]
        [DataRow("when(int0 > 10) then(int0 + decimal1);when(int0 < 10) then(int0 * decimal1);else(int0 - decimal1);", 10, 34.56, 10 - 34.56)]
        [DataRow("when(int0 > 20) then(int0 / decimal1);when(int0 <= 20 && int0 > 10) then(int0 + decimal1);when(int0 < 10) then(int0 * decimal1);else(int0 - decimal1);", 345, 34.5, 345 / 34.5)]
        public void TestParameter(string expression, int p1, double p2, double result)
        {
            var jRuleExpression = new RuleExpression<Func<int, decimal, decimal>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function(p1, (decimal)p2);

            Assert.AreEqual((decimal)result, result1);
        }

        [DataTestMethod]
        [DataRow("var categories = [\"EMP\", \"DIR\", \"EXE\"];categories.Contains(employee0.Category) && (employee0.FlexAgeLastBirthday >= 18 && employee0.FlexAgeLastBirthday <= 70) && (employee0.Bool1 != null && employee0.Bool1 == true);", false)]
        [DataRow("var categories = [\"EMP\", \"DIR\", \"EXE\"];categories.Contains(employee0.Category) && (employee0.FlexAgeLastBirthday >= 18 && employee0.FlexAgeLastBirthday <= 70) && (employee0.Bool1 == null || employee0.Bool1 == false);", true)]
        public void TestParameterVariableBool(string expression, bool result)
        {
            var employee = new Employee() { FlexDate = new DateTime(2020, 6, 1), EmployeeNo = "100001", FullName = "张三", BirthDate = new DateTime(1979, 5, 21), FlexSalary = 9690, Category = "EMP", Nationality = "中国", Bool1 = false };
            var jRuleExpression = new RuleExpression<Func<Employee, bool>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function(employee);

            Assert.AreEqual(result, result1);
        }

        [DataTestMethod]
        [DataRow("when(employee0.FlexSalary * 8.7 * 36 / 10000 + 10 > 271) then(271);else(employee0.FlexSalary * 8.7 * 36 / 10000 + 10);", 271)]
        [DataRow("when(employee0.FlexSalary * 8.7 * 36 / 10000 + 10 < 271) then(271);else(employee0.FlexSalary * 8.7 * 36 / 10000 + 10);", 313.4908)]
        public void TestParameterVariableDecimal(string expression, double result)
        {
            var employee = new Employee() { FlexDate = new DateTime(2020, 6, 1), EmployeeNo = "100001", FullName = "张三", BirthDate = new DateTime(1979, 5, 21), FlexSalary = 9690, Category = "EMP", Nationality = "中国", Bool1 = false };
            var jRuleExpression = new RuleExpression<Func<Employee, decimal>>();
            var function = jRuleExpression.Function(expression);

            var result1 = function(employee);

            Assert.AreEqual((decimal)result, result1);
        }
    }
}
