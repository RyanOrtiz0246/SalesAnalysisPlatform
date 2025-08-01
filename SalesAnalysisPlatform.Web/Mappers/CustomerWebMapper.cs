using SalesAnalysisPlatform.Domain.DTOs;
using SalesAnalysisPlatform.Domain.Entities;
using SalesAnalysisPlatform.Web.ViewModels;

namespace SalesAnalysisPlatform.Web.Mappers
{
    public static class CustomerWebMapper
    {
        public static CustomerViewModel ToViewModel(CustomerDTO dto)
        {
            return new CustomerViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address
            };
        }

        public static CustomerDTO ToDTO(CustomerViewModel vm)
        {
            return new CustomerDTO
            {
                Id = vm.Id,
                Name = vm.Name,
                Phone = vm.Phone,
                Email = vm.Email,
                Address = vm.Address
            };
        }
    }
}
