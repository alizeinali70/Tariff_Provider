using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tariff_Provider_API.Model;

namespace Tariff_Provider_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly List<cl_Product> Products = new List<cl_Product>
        {
            new cl_Product { Id = 1, Name = "Product A", AdditionaKwhCost = 0.22, Base_Cost = 5, IncludedKwh = 0,  Product_Type = new cl_Product_Type { Id = 1, Name = "TariffProvider1" } },
            new cl_Product { Id = 2, Name = "Product B", AdditionaKwhCost = 0.30, Base_Cost = 800, IncludedKwh = 4000, Product_Type = new cl_Product_Type { Id = 2, Name = "TariffProvider2" } }
        };
        [HttpGet("calculate-annual-cost")]
        public ActionResult<IEnumerable<ProductWithAnnualCost>> GetProductsWithAnnualCost([FromQuery] double consumption)
        {
            var productsWithAnnualCost = new List<ProductWithAnnualCost>();
            double annualCost=0;
            foreach (var product in Products)
            {
                

                if (consumption < product.IncludedKwh && product.IncludedKwh != 0)
                {
                    annualCost = product.Base_Cost;
                }
                else if (consumption > product.IncludedKwh && product.IncludedKwh != 0)
                {
                    annualCost = product.Base_Cost + (consumption - product.IncludedKwh) * product.AdditionaKwhCost;
                }
                else
                {
                    annualCost = product.Base_Cost * 12 + consumption * product.AdditionaKwhCost;
                }

                productsWithAnnualCost.Add(new ProductWithAnnualCost
                {
                    Product = product,
                    AnnualCost = annualCost
                });
            }

            var sortedProducts = productsWithAnnualCost.OrderBy(p => p.AnnualCost).ToList();
            return Ok(sortedProducts);
        }
    }

    public class ProductWithAnnualCost
    {
        public cl_Product Product { get; set; }
        public double AnnualCost { get; set; }
    }
}
