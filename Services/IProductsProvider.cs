using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public interface IProductsProvider
	{
		public (Error, List<Product>?) GetProducts(string token);
		public (Error, Brand?) GetBrand(string token, int id);
		public Product GetProduct(string name);
		public Error AddProduct(string token, Product product);
		public Error UpdateProduct(string token, Product product);
		public Error DeleteProduct(string token, int id);
	}
}
