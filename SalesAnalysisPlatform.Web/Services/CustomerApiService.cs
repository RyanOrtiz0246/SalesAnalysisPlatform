using SalesAnalysisPlatform.Domain.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace SalesAnalysisPlatform.Web.Services
{
    public class CustomerApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomerApiService> _logger;

        public CustomerApiService(HttpClient httpClient, ILogger<CustomerApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<CustomerDTO>> GetAllCustomerAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<CustomerDTO>>("api/customers");
                return response ?? new List<CustomerDTO>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener clientes desde el API");
                throw new ApplicationException("No se pudieron cargar los clientes. Intente más tarde.");
            }
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CustomerDTO>($"api/customers/{id}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<bool> AddCustomerAsync(CustomerDTO customer)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customers", customer);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCustomerAsync(CustomerDTO customer)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customers/{customer.Id}", customer);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/customers/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
