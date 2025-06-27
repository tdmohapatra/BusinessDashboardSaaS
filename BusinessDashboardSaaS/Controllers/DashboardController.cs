using BusinessDashboardSaaS.Data.Interfaces;
using BusinessDashboardSaaS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusinessDashboardSaaS.Controllers
{
    //[Authorize]
    //[Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(
            IProductRepository productRepo,
            UserManager<IdentityUser> userManager,
            ILogger<DashboardController> logger)
        {
            _productRepo = productRepo;
            _userManager = userManager;
            _logger = logger;
        }

        // 🏠 Dashboard Page
        public IActionResult Index()
        {
            return View();
        }

        // 📊 API Endpoint to fetch Dashboard Stats
        [HttpGet]
        public async Task<IActionResult> GetDashboardStats()
        {
            try
            {
                var products = (await _productRepo.GetAllAsync()).ToList();
                var users = _userManager.Users.ToList();

                var byCategory = products
                    .GroupBy(p => string.IsNullOrEmpty(p.Category) ? "Uncategorized" : p.Category)
                    .Select(g => new
                    {
                        Category = g.Key,
                        Count = g.Count()
                    })
                    .ToList();

                var response = new
                {
                    Success = true,
                    TotalUsers = users.Count,
                    TotalProducts = products.Count,
                    TotalStock = products.Sum(p => p.StockQty),
                    AvgPrice = products.Any() ? products.Average(p => p.Price) : 0,
                    ByCategory = byCategory,
                    AllProducts = products.Select(p => new
                    {
                        p.Name,
                        Category = p.Category ?? "Uncategorized",
                        p.Price,
                        p.StockQty
                    })
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching dashboard stats.");

                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error retrieving dashboard data.",
                    Error = ex.Message
                });
            }
        }
    }
}
