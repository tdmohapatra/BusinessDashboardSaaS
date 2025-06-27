using BusinessDashboardSaaS.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace BusinessDashboardSaaS.Controllers
{


    //[Authorize]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IProductRepository _repo;
       
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(IProductRepository repo, UserManager<IdentityUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetDashboardStats()
        //{
        //    var products = await _repo.GetAllAsync();

        //    var summary = new
        //    {
        //        TotalProducts = products.Count(),
        //        TotalStock = products.Sum(p => p.StockQty),
        //        AvgPrice = products.Average(p => p.Price),
        //        ByCategory = products
        //            .GroupBy(p => p.Category ?? "Uncategorized")
        //            .Select(g => new { Category = g.Key, Count = g.Count() })
        //    };

        //    return Json(summary);
        //}

        [HttpGet]
        public async Task<IActionResult> GetDashboardStats()
        {
            var products = (await _repo.GetAllAsync()).ToList();
            var users = _userManager.Users.ToList();

            var summary = new
            {
                TotalProducts = products.Count,
                TotalStock = products.Sum(p => p.StockQty),
                AvgPrice = products.Any() ? products.Average(p => p.Price) : 0,
                TotalUsers = users.Count,
                ByCategory = products
                    .GroupBy(p => p.Category ?? "Uncategorized")
                    .Select(g => new { Category = g.Key, Count = g.Count() })
                    .ToList(),
                AllProducts = products
            };

            return Json(summary);
        }

    }
}

