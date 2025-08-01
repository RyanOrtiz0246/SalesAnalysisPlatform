using Microsoft.AspNetCore.Mvc;
using SalesAnalysisPlatform.Domain.DTOs;
using SalesAnalysisPlatform.Web.Services;
using SalesAnalysisPlatform.Web.ViewModels;

namespace SalesAnalysisPlatform.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesApiService _salesApiService;
        private readonly ILogger<SalesController> _logger;

        public SalesController(SalesApiService salesApiService, ILogger<SalesController> logger)
        {
            _salesApiService = salesApiService;
            _logger = logger;
        }
        private SaleViewModel MapToViewModel(SaleDTO sale)
        {
            return new SaleViewModel
            {
                Id = sale.Id,
                ProductName = sale.ProductName,
                Price = sale.Price,
                Quantity = sale.Quantity,
                SaleDate = sale.SaleDate
            };
        }

        private SaleDTO MapToDTO(SaleViewModel viewModel)
        {
            return new SaleDTO
            {
                Id = viewModel.Id,
                ProductName = viewModel.ProductName,
                Price = viewModel.Price,
                Quantity = viewModel.Quantity,
                SaleDate = viewModel.SaleDate
            };
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var sales = await _salesApiService.GetAllSalesAsync();
                var viewModels = sales.Select(MapToViewModel).ToList();
                return View(viewModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar ventas");
                TempData["ErrorMessage"] = "Error al cargar las ventas. Intente más tarde.";
                return View(new List<SaleViewModel>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                var sale = MapToDTO(viewModel);
                if (await _salesApiService.AddSaleAsync(sale))
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "No se pudo crear la venta.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear venta");
                ModelState.AddModelError("", "Error interno al crear la venta.");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sale = await _salesApiService.GetSaleByIdAsync(id);
            return sale == null ? NotFound() : View(MapToViewModel(sale));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SaleViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                var sale = MapToDTO(viewModel);
                if (await _salesApiService.UpdateSaleAsync(sale))
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "No se pudo actualizar la venta.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar venta");
                ModelState.AddModelError("", "Error interno al actualizar la venta.");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sale = await _salesApiService.GetSaleByIdAsync(id);
            return sale == null ? NotFound() : View(MapToViewModel(sale));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (await _salesApiService.DeleteSaleAsync(id))
                    return RedirectToAction(nameof(Index));

                TempData["ErrorMessage"] = "No se pudo eliminar la venta.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar venta");
                TempData["ErrorMessage"] = "Error interno al eliminar la venta.";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var sale = await _salesApiService.GetSaleByIdAsync(id);
            return sale == null ? NotFound() : View(MapToViewModel(sale));
        }
    }
}
