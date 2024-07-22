using Microsoft.AspNetCore.Mvc;

namespace LoginAppraisalDemo.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult HRWelcome(string username)
        {
            ViewBag.Username = username;
            return View();
        }

        public IActionResult EmployeeWelcome(string username)
        {
            ViewBag.Username = username;
            return View();
        }

        public IActionResult ManagerWelcome(string username)
        {
            ViewBag.Username = username;
            return View();
        }
    }
}
