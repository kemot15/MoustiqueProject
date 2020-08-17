using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moustique.Services.Interfaces;

namespace Moustique.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILoggerService _loggerService;

        public AdminController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> VisitReport()
        {

            return View(await _loggerService.GetVisitsStatisticsAsync());
        }
    }
}
