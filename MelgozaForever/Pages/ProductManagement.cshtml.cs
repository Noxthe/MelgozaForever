using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace MelgozaForever.Pages
{
    public class ProductManagementModel : PageModel
    {
        public string? Username { get; set; }
        public List<Product>? productsData { get; set; }
        public Error? error { get; set; }
		private readonly ILogger<ProductManagementModel> _logger;
		public IProductsProvider _products { get; set; }

        public ProductManagementModel(ILogger<ProductManagementModel> logger, IProductsProvider products)
        {
			_logger = logger;
			_products = products;
		}

		public void OnGet()
        {
            Username = HttpContext.Session.GetString("username");
            var token = HttpContext.Session.GetString("token");
            if (Username == null && token == null)
            {
                Response.Redirect("LogIn");
            }
            else
            {
                var result = _products.GetProducts(token);
                error = result.Item1;
                if(error.Code == ErrorCode.Success)
                {
                    productsData = result.Item2;
                }
            }
        }
    }
}
