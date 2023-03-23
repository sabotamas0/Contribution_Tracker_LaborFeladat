using Microsoft.AspNetCore.Mvc;

namespace ContributionTracker.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
