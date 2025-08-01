using Microsoft.AspNetCore.Mvc;
using SalesAnalysisPlatform.Application.Interfaces;
using SalesAnalysisPlatform.Domain.DTOs;
using SalesAnalysisPlatform.Application.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAnalysisPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetAllSales()
        {
            var sales = await _saleService.GetAllSalesAsync();
            var dtos = sales.Select(SaleMapper.ToDTO);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDTO>> GetSaleById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(SaleMapper.ToDTO(sale));
        }

        [HttpPost]
        public async Task<ActionResult> CreateSale([FromBody] SaleDTO dto)
        {
            var sale = SaleMapper.ToEntity(dto);
            await _saleService.AddSaleAsync(sale);
            return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, SaleMapper.ToDTO(sale));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSale(int id, [FromBody] SaleDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var sale = SaleMapper.ToEntity(dto);
            await _saleService.UpdateSaleAsync(sale);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSale(int id)
        {
            await _saleService.DeleteSaleAsync(id);
            return NoContent();
        }
    }
}
