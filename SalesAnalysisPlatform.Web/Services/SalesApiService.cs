using SalesAnalysisPlatform.Domain.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace SalesAnalysisPlatform.Web.Services
{
    public class SalesApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SalesApiService> _logger;

        public SalesApiService(HttpClient httpClient, ILogger<SalesApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SaleDTO>> GetAllSalesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<SaleDTO>>("api/sales");
                return response ?? new List<SaleDTO>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener ventas desde el API");
                throw new ApplicationException("No se pudieron cargar las ventas. Intente más tarde.");
            }
        }

        public async Task<SaleDTO?> GetSaleByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<SaleDTO>($"api/sales/{id}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<bool> AddSaleAsync(SaleDTO sale)
        {
            var response = await _httpClient.PostAsJsonAsync("api/sales", sale);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateSaleAsync(SaleDTO sale)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/sales/{sale.Id}", sale);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/sales/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
