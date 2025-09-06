using LeaveManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagmentSystem.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {

            var model = new AboutViewModel { BirthDate = new DateTime(1998,12,19) };
            return View(model);
        }
    }
}
