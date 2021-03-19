using System;
using System.Collections.Generic;

namespace Min.RuleEngine.ConsoleHost.Model
{
    internal class Product
    {
        public string ProductId { get; set; }

        public List<ProductOption> ProductOptions { get; set; }
    }
}
