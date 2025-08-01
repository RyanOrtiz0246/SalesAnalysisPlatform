using Microsoft.AspNetCore.Mvc;
using SalesAnalysisPlatform.Domain.DTOs;
using SalesAnalysisPlatform.Domain.Entities;
using SalesAnalysisPlatform.Web.Services;
using SalesAnalysisPlatform.Web.ViewModels;

namespace SalesAnalysisPlatform.Web.Controllers
{
    public class CustomersController : Controller
    {
            private readonly CustomerApiService _customerApiService;
            private readonly ILogger<CustomersController> _logger;

            public CustomersController(CustomerApiService customerApiService, ILogger<CustomersController> logger)
            {
                _customerApiService = customerApiService;
                _logger = logger;
            }
            private CustomerViewModel MapToViewModel(CustomerDTO customer)
            {
                return new CustomerViewModel
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address,
                    Phone = customer.Phone,
                    Email = customer.Email
                };
            }

            private CustomerDTO MapToDTO(CustomerViewModel viewModel)
            {
                return new CustomerDTO
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    Phone = viewModel.Phone,
                    Email = viewModel.Email
                };
            }

            public async Task<IActionResult> Index()
            {
                try
                {
                    var customer = await _customerApiService.GetAllCustomerAsync();
                    var viewModels = customer.Select(MapToViewModel).ToList();
                    return View(viewModels);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al cargar clientes");
                    TempData["ErrorMessage"] = "Error al cargar los clientes. Intente más tarde.";
                    return View(new List<CustomerViewModel>());
                }
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(CustomerViewModel viewModel)
            {
                if (!ModelState.IsValid)
                    return View(viewModel);

                try
                {
                    var customer = MapToDTO(viewModel);
                    if (await _customerApiService.AddCustomerAsync(customer))
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", "No se pudo crear el cliente.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear cliente");
                    ModelState.AddModelError("", "Error interno al crear el cliente.");
                }

                return View(viewModel);
            }

            public async Task<IActionResult> Edit(int id)
            {
                var customer = await _customerApiService.GetCustomerByIdAsync(id);
                return customer == null ? NotFound() : View(MapToViewModel(customer));
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(CustomerViewModel viewModel)
            {
                if (!ModelState.IsValid)
                    return View(viewModel);

                try
                {
                    var customer = MapToDTO(viewModel);
                    if (await _customerApiService.UpdateCustomerAsync(customer))
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", "No se pudo actualizar el cliente.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al actualizar cliente");
                    ModelState.AddModelError("", "Error interno al actualizar el cliente.");
                }

                return View(viewModel);
            }

            public async Task<IActionResult> Delete(int id)
            {
                var customer = await _customerApiService.GetCustomerByIdAsync(id);
                return customer == null ? NotFound() : View(MapToViewModel(customer));
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                try
                {
                    if (await _customerApiService.DeleteCustomerAsync(id))
                        return RedirectToAction(nameof(Index));

                    TempData["ErrorMessage"] = "No se pudo eliminar el cliente.";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al eliminar cliente");
                    TempData["ErrorMessage"] = "Error interno al eliminar el cliente.";
                }

                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Details(int id)
            {
                var customer = await _customerApiService.GetCustomerByIdAsync(id);
                return customer == null ? NotFound() : View(MapToViewModel(customer));
            }
        }
    }
