using BusinessDashboardSaaS.Models;
using BusinessDashboardSaaS.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessDashboardSaaS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Product product)
        {
            var result = await _service.SaveAsync(product);
            return Json(new { success = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            return Json(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Json(new { success = result });
        }
    }

}
