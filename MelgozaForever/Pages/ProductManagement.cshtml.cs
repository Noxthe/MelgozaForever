using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace MelgozaForever.Pages
{
    public class ProductManagementModel : PageModel
    {
		[BindProperty]
        public string search { get; set; } = "";
		[BindProperty]
        public int currentPage { get; set; } = 1;
		public string? Username { get; set; }
        public List<Product>? productsData { get; set; }
        public List<Brand>? brands { get; set; }
        public int totalPages { get; set; }
        public Error? error { get; set; }
		private readonly ILogger<ProductManagementModel> _logger;
		public IProductsProvider _products { get; set; }

        public ProductManagementModel(ILogger<ProductManagementModel> logger, IProductsProvider products)
        {
			_logger = logger;
			_products = products;
		}

		private void FetchProducts()
		{
			var token = HttpContext.Session.GetString("token");
			var result = _products.GetProducts(token);
			error = result.Item1;
			if (error.Code == ErrorCode.Success)
			{
				productsData = result.Item2;
			}
		}

		private void FilterProducts()
		{
			if(search == null || search.Length == 0)
			{
				return;
			}

			var filteredProducts = new List<Product>();
			foreach (var product in productsData)
			{
				if (product.name.ToLower().Contains(search.ToLower()))
				{
					filteredProducts.Add(product);
				}
			}
			productsData = filteredProducts;
		}

		private void CalculateTotalPages()
		{
			totalPages = (int)Math.Ceiling((double)productsData.Count / 10);
		}

		private void FetchBrands()
		{
			var token = HttpContext.Session.GetString("token");
			brands = new List<Brand>();
			foreach (var product in productsData)
			{
				var brandId = product.id;

				// check if brand already exists
				if (brands.All(brand => brand.id != brandId))
				{
					var brand = _products.GetBrand(token, brandId);
					var error = brand.Item1;
					if (error.Code == ErrorCode.Success)
					{
						brands.Add(brand.Item2);
					}
					else
					{
						productsData = null;
						brands = null;
						break;
					}
				}
			}
		}

		private void PopulateProductsData()
        {
			Username = HttpContext.Session.GetString("username");
			if (Username == null)
			{
				Response.Redirect("LogIn");
			}
			else
			{
				FetchProducts();
				if(productsData != null)
				{
					FetchBrands();
					FilterProducts();
					CalculateTotalPages();
				}
			}
		}

		public void OnGet()
        {
			PopulateProductsData();
		}

		public void OnPostFilter()
		{
			PopulateProductsData();
		}

		public void OnGetLogout()
        {
			HttpContext.Session.Remove("username");
			HttpContext.Session.Remove("token");
			Response.Redirect("LogIn");
		}
    }
}
