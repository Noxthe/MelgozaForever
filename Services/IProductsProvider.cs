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
		public Product GetProduct(string name);
		public void AddProduct(Product product);
		public void UpdateProduct(Product product);
		public void DeleteProduct(string name);
	}
}
