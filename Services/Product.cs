using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class Product
	{
		public enum Target
		{
			Men = 1,
			Women,
			Unisex
		}

		public string? name { get; set; }
		public string? category { get; set; }
		public string? description { get; set; }
		public int price { get; set; }
		public string? stock { get; set; }
		public string? size { get; set; }
		public string? target { get; set; }

		public Product()
		{

		}
	}
}
