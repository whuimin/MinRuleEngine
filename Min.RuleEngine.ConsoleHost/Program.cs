using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Min.RuleEngine.ConsoleHost.Model;

namespace Min.RuleEngine.ConsoleHost
{
	class Program
	{
		private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
		{
			PropertyNameCaseInsensitive = true
		};
		static void Main(string[] args)
		{
			TestEmployeeProduct();
		}

		public static void TestEmployeeProduct()
		{
			
			Console.WriteLine("Loading data...");

			var jsonText = File.ReadAllText("Data\\Employees.json");
			var employees = JsonSerializer.Deserialize<List<Employee>>(jsonText, jsonSerializerOptions);

			var products = ProductTool.BuildProducts();

			Console.WriteLine("Loading data finish.");
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey(true);
			Console.WriteLine("Processing...");

			var stopwatch = Stopwatch.StartNew();

			var index = 0;
			foreach (var employee in employees)
			{
				var eligibleProducts = EligibleProducts(products, employee);

				index++;
			}

			stopwatch.Stop();

			Console.WriteLine($"{index} employees processed. Elapsed in {stopwatch.ElapsedMilliseconds}ms");

			Console.WriteLine("Press any key to exit.");
			Console.ReadKey(true);
		}

		private static List<Product> EligibleProducts(List<Product> products, Employee employee)
		{
			var eligibleProducts = new List<Product>();
			foreach (var product in products)
			{
				var newProduct = new Product()
				{
					ProductId = product.ProductId,
					ProductOptions = new List<ProductOption>()
				};

				foreach (var productOption in product.ProductOptions)
				{
					if (HasEligibility(employee, productOption.Eligibility))
					{
						var newProductOption = new ProductOption()
						{
							ProductOptionId = productOption.ProductOptionId,
							Eligibility = productOption.Eligibility,
							Pricing = productOption.Pricing,
							Price = CalculatePricing(employee, productOption.Pricing)
						};
						newProduct.ProductOptions.Add(newProductOption);
					}
				}

				if (newProduct.ProductOptions.Count > 0)
				{
					eligibleProducts.Add(newProduct);
				}
			}

			return eligibleProducts;
		}

		private static bool HasEligibility(Employee employee, string eligibility)
		{
			var ruleExpression = new RuleExpression<Func<Employee, bool>>();
			var function = ruleExpression.Function(eligibility);

			var result = function(employee);

			return result;
		}

		private static decimal CalculatePricing(Employee employee, string pricing)
		{
			var ruleExpression = new RuleExpression<Func<Employee, decimal>>();
			var function = ruleExpression.Function(pricing);

			var result = function(employee);

			return result;
		}
	}
}
