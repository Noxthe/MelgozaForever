using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
	public class ProductsWSProvider : IProductsProvider
	{
		private static string ApiUrlBase
		{
			get
			{
				return "http://localhost:8080/api";
			}
		}

		public (Error, List<Product>?) GetProducts(string token)
		{
			var error = new Error();
			try
			{
				using (var client = new HttpClient())
				{
					client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
					var productsListUrl = string.Format("{0}/products", ApiUrlBase);
					var productsListResponseMessage = client.GetAsync(productsListUrl).Result;

					if (productsListResponseMessage != null)
					{
						if (productsListResponseMessage.IsSuccessStatusCode)
						{
							var response = productsListResponseMessage.Content.ReadFromJsonAsync<List<Product>>().Result;
							if (response != null)
							{
								error.Code = ErrorCode.Success;
								return (error, response);
							}
							else
							{
								error.Code = ErrorCode.ServerInternalError;
								return (error, null);
							}
						}
						else
						{
							var errorResponse = productsListResponseMessage.Content.ReadFromJsonAsync<Error>().Result;
							if (errorResponse != null)
							{
								if (productsListResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
								{
									errorResponse.Code = ErrorCode.InvalidCredentials;
								}
								else
								{
									errorResponse.Code = ErrorCode.ServerInternalError;
								}
							}
							else
							{
								errorResponse = new Error();
								errorResponse.Code = ErrorCode.ServerInternalError;
							}
							return (errorResponse, null);
						}
					}
					else
					{
						error.Code = ErrorCode.NoResponse;
						return (error, null);
					}
				}
			}
			catch (Exception e)
			{
				error.Code = ErrorCode.ClientError;
				error.Message = e.Message;
				return (error, null);
			}
		}

		public (Error, Brand?) GetBrand(string token, int id)
		{
			var error = new Error();
			try
			{
				using (var client = new HttpClient())
				{
					client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
					var brandUrl = string.Format("{0}/brands/{1}", ApiUrlBase, id);
					var brandResponseMessage = client.GetAsync(brandUrl).Result;

					if (brandResponseMessage != null)
					{
						if (brandResponseMessage.IsSuccessStatusCode)
						{
							var response = brandResponseMessage.Content.ReadFromJsonAsync<Brand>().Result;
							if (response != null)
							{
								error.Code = ErrorCode.Success;
								return (error, response);
							}
							else
							{
								error.Code = ErrorCode.ServerInternalError;
								return (error, null);
							}
						}
						else
						{
							var errorResponse = brandResponseMessage.Content.ReadFromJsonAsync<Error>().Result;
							if (errorResponse != null)
							{
								if (brandResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
								{
									errorResponse.Code = ErrorCode.InvalidCredentials;
								}
								else
								{
									errorResponse.Code = ErrorCode.ServerInternalError;
								}
							}
							else
							{
								errorResponse = new Error();
								errorResponse.Code = ErrorCode.ServerInternalError;
							}
							return (errorResponse, null);
						}
					}
					else
					{
						error.Code = ErrorCode.NoResponse;
						return (error, null);
					}
				}
			}
			catch (Exception e)
			{
				error.Code = ErrorCode.ClientError;
				error.Message = e.Message;
				return (error, null);
			}
		}

		public Product GetProduct(string name)
		{
			throw new NotImplementedException();
		}

		public void AddProduct(Product product)
		{
			throw new NotImplementedException();
		}

		public void UpdateProduct(Product product)
		{
			throw new NotImplementedException();
		}

		public void DeleteProduct(string name)
		{
			throw new NotImplementedException();
		}
	}
}
