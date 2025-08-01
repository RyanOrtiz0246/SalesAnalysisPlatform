using System.ComponentModel.DataAnnotations;

namespace SalesAnalysisPlatform.Web.ViewModels
{
    public class SaleViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Display(Name = "Sale Date")]
        [DataType(DataType.Date)]
        public DateTime SaleDate { get; set; } = DateTime.Now;
    }
}
