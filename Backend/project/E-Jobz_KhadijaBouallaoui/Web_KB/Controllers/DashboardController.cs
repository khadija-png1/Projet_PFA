using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_KB.Models;
using Web_KB.Services;

namespace Web_KB.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IOffresEmploiService _offresemploiService;


        public DashboardController(ILogger<DashboardController> logger , IOffresEmploiService offresemploiservice)
        {
            _logger = logger;
            _offresemploiService = offresemploiservice;
        }
        // GET: Dashboard
        [Authorize] // Bloque tout utilisateur non authentifié

        public ActionResult DashboardCandidat()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();
            return View();
        }
        [Authorize] // Bloque tout utilisateur non authentifié

        public ActionResult DashboardRecruteur()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();
            return View();
        }






        [Authorize] // Bloque tout utilisateur non authentifié

        public async Task<IActionResult> index()
        {
            var offres = await _offresemploiService.GetAllAsync();
            return View(offres);
        }
       

    }
}
