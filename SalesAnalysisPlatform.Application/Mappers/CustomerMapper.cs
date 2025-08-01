using SalesAnalysisPlatform.Domain.DTOs;
using SalesAnalysisPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAnalysisPlatform.Application.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDTO ToDTO(Customer customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address
            };
        }

        public static Customer ToEntity(CustomerDTO dto)
        {
            return new Customer
            {
                Id = dto.Id,
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address
            };
        }
    }
}
