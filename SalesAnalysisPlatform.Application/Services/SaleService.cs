using SalesAnalysisPlatform.Application.Interfaces;
using SalesAnalysisPlatform.Domain.Entities;
using SalesAnalysisPlatform.Infrastructure.Interfaces;

namespace SalesAnalysisPlatform.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _saleRepository.GetAllSalesAsync();
        }

        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            return await _saleRepository.GetSaleByIdAsync(id);
        }

        public async Task AddSaleAsync(Sale sale)
        {
            await _saleRepository.AddSaleAsync(sale);
        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            await _saleRepository.UpdateSaleAsync(sale);
        }

        public async Task DeleteSaleAsync(int id)
        {
            await _saleRepository.DeleteSaleAsync(id);
        }
    }
}
