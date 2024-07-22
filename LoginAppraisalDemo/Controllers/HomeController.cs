using LoginAppraisalDemo.Data;
using LoginAppraisalDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LoginAppraisalDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly EmployeeContext _context;

        private int _id;

        public HomeController(ILogger<HomeController> logger, EmployeeContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Employees
                                    .SingleOrDefaultAsync(e => e.Email == model.Username && e.Password == model.Password);
                HttpContext.Session.SetInt32("UserId", user.Id);

                if (user != null)
                {
                    switch (user.Role)
                    {
                        case "HR":
                            return RedirectToAction("HRWelcome", "Home", new { username = user.Name });
                        case "Employee":
                            return RedirectToAction("EmployeeWelcome", "Home", new { username = user.Name });
                        case "Manager":
                            return RedirectToAction("ManagerWelcome", "Home", new { username = user.Name });
                        default:
                            ModelState.AddModelError(string.Empty, "Invalid role.");
                            break;
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        public IActionResult Welcome(string username)
        {
            ViewBag.Username = username;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {

                _context.Employees.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Employee successfully added.";
                return RedirectToAction("HRWelcome", new { username = User.Identity.Name });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddCompetency()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCompetency(CompetencyModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Competencies.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Competency successfully added.";
                return RedirectToAction("HRWelcome", new { username = User.Identity.Name });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAppraisal(LoginModel model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            var managerEmail = model.Username;
            var manager = await _context.Employees.SingleOrDefaultAsync(e => e.Email == managerEmail);
            ViewBag.Employees = await _context.Employees.Where(e => e.ManagerId == userId).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppraisal(AppraisalModel model)
        {
                model.ManagerId = (int)HttpContext.Session.GetInt32("UserId");
                model.Status = "New";
                _context.Appraisals.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("ManagerWelcome", new { username = User.Identity.Name });
        }

        [HttpGet]
        public async Task<IActionResult> ViewAppraisals()
        {
            var eId = (int)HttpContext.Session.GetInt32("UserId");

            var currentUser = await _context.Employees
        .SingleOrDefaultAsync(u => u.Id == eId);

            if (currentUser == null)
            {
                return NotFound("User not found");
            }

            var role = currentUser?.Role;
            ViewBag.UserRole = role;

            var managerId = currentUser.ManagerId;

            var appraisals = await _context.Appraisals
                .Where(a => a.ManagerId == managerId && a.EmployeeId == eId || a.ManagerId == eId)
                .Select(a => new AppraisalModel
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    Employee = a.Employee,
                    ManagerId = a.ManagerId,
                    Manager = a.Manager,
                    ManagerComments = a.ManagerComments ?? string.Empty,
                    EmployeeComments = a.EmployeeComments ?? string.Empty,
                    ManagerRating = a.ManagerRating,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    Status = a.Status
                })
                .ToListAsync();

            if (role == "HR")
            {
                            var viewappraisals = await _context.Appraisals.ToListAsync();

                return View(viewappraisals);

            }

            return View(appraisals);
        }


        [HttpGet]
        public async Task<IActionResult> ViewEmployeeAppraisals()
        {
            var employeeEmail = User.Identity.Name;
            var employeeId = (await _context.Employees.SingleOrDefaultAsync(e => e.Email == employeeEmail)).Id;

            var appraisals = await _context.Appraisals
                .Where(a => a.EmployeeId == employeeId)
                .Include(a => a.Manager)
                .ToListAsync();

            return View(appraisals);
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployeeAppraisal(int id)
        {
            // Fetch the appraisal record from the database using the provided id
            var appraisal = await _context.Appraisals
                .SingleOrDefaultAsync(a => a.Id == id);

            // Check if the appraisal record was found
            if (appraisal == null)
            {
                // If not found, return a 404 Not Found result
                return NotFound();
            }

/*            if (appraisal.ManagerId == (int)HttpContext.Session.GetInt32("UserId"))
            {
                return RedirectToAction("ViewAppraisals");

            }*/

            // If found, return the view for editing the appraisal, passing the appraisal model to the view
            return View(appraisal);
        }


        [HttpPost]
        public async Task<IActionResult> EditEmployeeAppraisal(AppraisalModel model)
        {
            if (model.EmployeeComments!= null)
            {
                var appraisal = await _context.Appraisals.FindAsync(model.Id);
                if (appraisal == null)
                {
                    return NotFound();
                }

                appraisal.EmployeeComments = model.EmployeeComments;
                appraisal.Status = "Pending";
                await _context.SaveChangesAsync();
                return RedirectToAction("EmployeeWelcome", new { username = User.Identity.Name });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> FinalizeAppraisal(int id)
        {
            var appraisal = await _context.Appraisals.FindAsync(id);
            if (appraisal == null)
            {
                return NotFound();
            }
            return View(appraisal);
        }

        [HttpPost]
        public async Task<IActionResult> FinalizeAppraisal(AppraisalModel model)
        {
            if (model.ManagerRating!= null)
            {
                var appraisal = await _context.Appraisals.FindAsync(model.Id);
                if (appraisal == null)
                {
                    return NotFound();
                }

                appraisal.ManagerRating = model.ManagerRating;
                appraisal.Status = "Completed";
                await _context.SaveChangesAsync();
                return RedirectToAction("ManagerWelcome", new { username = User.Identity.Name });
            }
            return View(model);
        }
    }
}
