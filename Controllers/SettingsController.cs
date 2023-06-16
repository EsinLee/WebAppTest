using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppTest.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeLanguage(string lang)
        {
            //Session["lang"] = lang;
            return RedirectToAction("Index", "Home", new { language = lang });
        }
    }
}
