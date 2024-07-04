using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tariff_Provider_API.Model;

namespace Tariff_Provider.Pages
{
    public class ConsumptionModel : PageModel
    {
        [BindProperty]
        public double Consumption { get; set; }



        public List<cl_Product> Products { get; set; } = new List<cl_Product>();
        [BindProperty]
        public List<(cl_Product_Type product_Type, double AnnualCost)> ProductsWithAnnualCost { get; set; } = new List<(cl_Product_Type, double)>();

        public void OnGet()
        {

            var tariffProvider1 = new cl_Product_Type { Id = 1, Name = "basic electricity tariff" };
            var tariffProvider2 = new cl_Product_Type { Id = 2, Name = "Packaged tariff" };
            Products = new List<cl_Product>
            {
                new cl_Product { Id = 1, Name = "Product A",Product_Type=tariffProvider1,
                    Base_Cost=5,AdditionaKwhCost=0.22 },
                new cl_Product { Id = 2, Name = "Product B",Product_Type=tariffProvider2,
                    Base_Cost=800,AdditionaKwhCost=0.30, IncludedKwh=4000 }
            };
        }

        public void Clculate_Annual_costs()
        {
            ProductsWithAnnualCost.Clear();
            OnGet();
            foreach (var product in Products)
            {
                if (Consumption < product.IncludedKwh && product.IncludedKwh != 0)
                {
                    ProductsWithAnnualCost.Add((product.Product_Type, product.Base_Cost));
                    continue;
                }
                else if (Consumption > product.IncludedKwh && product.IncludedKwh != 0)
                {
                    ProductsWithAnnualCost.Add((product.Product_Type, product.Base_Cost + ((Consumption - product.IncludedKwh) * product.AdditionaKwhCost)));
                    continue;
                }
                else
                {
                    ProductsWithAnnualCost.Add((product.Product_Type, (product.Base_Cost * 12) + (Consumption * product.AdditionaKwhCost)));
                    continue;
                }
            }
            ProductsWithAnnualCost= ProductsWithAnnualCost.OrderBy(x => x.AnnualCost).ToList();
        }
        public void OnPost()
        {

            Clculate_Annual_costs();
            Products = Products.ToList();
        }


    }
}
