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
		[BindProperty]
		public int? id { get; set; }
		[BindProperty]
		public string? name { get; set; }
		[BindProperty]
		public int? brand { get; set; }
		[BindProperty]
		public string? category { get; set; }
		[BindProperty]
		public string? description { get; set; }
		[BindProperty]
		public float? price { get; set; }
		[BindProperty]
		public int? stock { get; set; }
		[BindProperty]
		public float? size { get; set; }
		[BindProperty]
		public int? target { get; set; }
		public string? Username { get; set; }
        public List<Product>? productsData { get; set; }
        public List<Brand>? brands { get; set; }
        public int totalPages { get; set; }
        public Error? error { get; set; }
		public string? message { get; set; }
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
					var brand = _products.GetBrand(token, (int)brandId);
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

		public void OnPostModify()
		{
			Username = HttpContext.Session.GetString("username");
			if (Username == null)
			{
				Response.Redirect("LogIn");
			}
			else
			{
				var product = new Product();
				product.id = id;
				product.name = name;
				product.brandId = brand;
				product.description = description;
				product.price = price;
				product.stock = stock;
				product.size = size;
				product.target = target;

				var token = HttpContext.Session.GetString("token");
				var result = _products.UpdateProduct(token, product);

				switch(result.Code)
				{
					case ErrorCode.Success:
						message = "Producto modificado correctamente";
						break;

					case ErrorCode.InvalidData:
						message = "Datos inválidos";
						break;

					default:
						message = "Ha ocurrido un error, intentelo más tarde";
						break;
				}

				PopulateProductsData();
			}
		}

		public void OnPostRegister()
		{
			Username = HttpContext.Session.GetString("username");
			if (Username == null)
			{
				Response.Redirect("LogIn");
			}
			else
			{
				var product = new Product();
				product.name = name;
				product.brandId = brand;
				product.description = description;
				product.price = price;
				product.stock = stock;
				product.size = size;
				product.target = target;

				var token = HttpContext.Session.GetString("token");
				var result = _products.AddProduct(token, product);

				switch (result.Code)
				{
					case ErrorCode.Success:
						message = "Producto registrado correctamente";
						break;

					case ErrorCode.InvalidData:
						message = "Datos inválidos";
						break;

					default:
						message = "Ha ocurrido un error, intentelo más tarde";
						break;
				}

				PopulateProductsData();
			}
		}

		public void OnGetLogout()
        {
			HttpContext.Session.Remove("username");
			HttpContext.Session.Remove("token");
			Response.Redirect("LogIn");
		}
    }
}
