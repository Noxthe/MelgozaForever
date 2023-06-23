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

		public int? id { get; set; }
		public string? name { get; set; }
		public string? category { get; set; }
		public string? description { get; set; }
		public float? price { get; set; }
		public int? stock { get; set; }
		public float? size { get; set; }
		public int? target { get; set; }
		public int? brandId { get; set; }

		public Product()
		{

		}
	}
}
