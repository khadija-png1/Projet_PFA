using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        switch (statusCode)
        {
            case 401:
                ViewData["Message"] = "401 – Vous devez être connecté pour accéder à cette page.";
                break;

            case 403:
                ViewData["Message"] = "403 – Accès refusé.";
                break;

            case 404:
                ViewData["Message"] = "404 – Page non trouvée.";
                break;

            default:
                ViewData["Message"] = "Une erreur inattendue est survenue.";
                break;
        }

        Response.StatusCode = statusCode; // 🔥 important
        return View("Error");
    }

    [Route("Error")]
    public IActionResult Error()
    {
        ViewData["Message"] = "Une erreur est survenue.";
        return View("Error");
    }
}
