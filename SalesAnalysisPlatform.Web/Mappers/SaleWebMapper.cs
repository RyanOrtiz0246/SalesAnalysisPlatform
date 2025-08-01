using SalesAnalysisPlatform.Domain.DTOs;
using SalesAnalysisPlatform.Web.ViewModels;

namespace SalesAnalysisPlatform.Web.Mappers
{
    public static class SaleWebMapper
    {
        public static SaleViewModel ToViewModel(SaleDTO dto)
        {
            return new SaleViewModel
            {
                Id = dto.Id,
                ProductName = dto.ProductName,
                Price = dto.Price,
                Quantity = dto.Quantity,
                SaleDate = dto.SaleDate
            };
        }

        public static SaleDTO ToDTO(SaleViewModel vm)
        {
            return new SaleDTO
            {
                Id = vm.Id,
                ProductName = vm.ProductName,
                Price = vm.Price,
                Quantity = vm.Quantity,
                SaleDate = vm.SaleDate
            };
        }
    }
}
