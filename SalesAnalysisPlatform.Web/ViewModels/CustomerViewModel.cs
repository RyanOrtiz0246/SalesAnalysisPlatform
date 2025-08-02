using SalesAnalysisPlatform.Domain.DTOs;
using System.ComponentModel.DataAnnotations;

namespace SalesAnalysisPlatform.Web.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public static CustomerViewModel FromDTO(CustomerDTO dto)
        {
            return new CustomerViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };
        }

        public CustomerDTO ToDTO()
        {
            return new CustomerDTO
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Phone = Phone,
                Address = Address
            };
        }
    }
}
