using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LoginAppraisalDemo.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginInputModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // This is a placeholder for actual authentication logic.
                if (model.Username == "test" && model.Password == "password")
                {
                    // Simulate successful login by redirecting to home page.
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        public class LoginInputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
    }
}
