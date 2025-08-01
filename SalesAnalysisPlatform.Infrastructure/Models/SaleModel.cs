using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAnalysisPlatform.Infrastructure.Models
{
    internal class SaleModel
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
