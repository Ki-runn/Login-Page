using LoginAppraisalDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginAppraisalDemo.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public DbSet<UsersRole> Users { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<CompetencyModel> Competencies { get; set; }

        public DbSet<AppraisalModel> Appraisals { get; set; }
    }

    public class UsersRole
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
    }

}
