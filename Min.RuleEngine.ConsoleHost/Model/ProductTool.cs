using System;
using System.IO;
using System.Collections.Generic;

namespace Min.RuleEngine.ConsoleHost.Model
{
	internal class ProductTool
	{
		public static List<Product> BuildProducts()
		{
			var produts = new List<Product>();

			produts.Add(BuildProduct(1, 4));
			produts.Add(BuildProduct(2, 4));
			produts.Add(BuildProduct(3, 3));
			produts.Add(BuildProduct(4, 4));
			produts.Add(BuildProduct(5, 4));
			produts.Add(BuildProduct(6, 1));
			produts.Add(BuildProduct(7, 1));

			return produts;
		}

		private static Product BuildProduct(int productId, int optionCount)
		{
			var product = new Product()
			{
				ProductId = $"Product{productId.ToString().PadLeft(2, '0')}",
				ProductOptions = new List<ProductOption>()
			};

			var eligibility = File.ReadAllText($"Rules\\{product.ProductId}_Eligibility.rule");

			for (int i = 1; i <= optionCount; i++)
			{
				product.ProductOptions.Add(new ProductOption()
				{
					ProductOptionId = $"{product.ProductId}_Option_{i.ToString().PadLeft(2, '0')}",
					Eligibility = eligibility,
					Pricing = File.ReadAllText($"Rules\\{product.ProductId}_Option{i.ToString().PadLeft(2, '0')}_Pricing.rule")
				});
			}

			return product;
		}
	}
}
