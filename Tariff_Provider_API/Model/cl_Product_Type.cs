using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tariff_Provider_API.Model
{
    public class cl_Product_Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Type_Name")]
        public string Name { get; set; }

    }
}
