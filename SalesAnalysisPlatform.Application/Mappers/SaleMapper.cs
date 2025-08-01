using SalesAnalysisPlatform.Domain.DTOs;
using SalesAnalysisPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAnalysisPlatform.Application.Mappers
{
    public static class SaleMapper
    {
        public static SaleDTO ToDTO(Sale sale)
        {
            return new SaleDTO
            {
                Id = sale.Id,
                ProductName = sale.ProductName,
                Price = sale.Price,
                Quantity = sale.Quantity,
                SaleDate = sale.SaleDate
            };
        }

        public static Sale ToEntity(SaleDTO dto)
        {
            return new Sale
            {
                Id = dto.Id,
                ProductName = dto.ProductName,
                Price = dto.Price,
                Quantity = dto.Quantity,
                SaleDate = dto.SaleDate
            };
        }
    }
}
