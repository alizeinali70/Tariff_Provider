using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tariff_Provider_API.Model
{
    public class cl_Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Add Product Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Add Base_Cost")]
        [Display(Name = "BaseCost")]
        public double Base_Cost { get; set; }

        [Required(ErrorMessage = "Please Add Add_Kwh_Cost")]
        [Display(Name = "AdditionaKwhCost")]
        public double AdditionaKwhCost { get; set; }

        [Required(ErrorMessage = "Please Add Included_Kwh")]
        [Display(Name = "IncludedKwh")]
        public double IncludedKwh { get; set; } = 0;
        //public List<double> AnnualCost { get; set; }
        public cl_Product_Type Product_Type { get; set; }
    }
}
