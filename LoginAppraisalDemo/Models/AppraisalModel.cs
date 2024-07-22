using System.ComponentModel.DataAnnotations;

namespace LoginAppraisalDemo.Models
{
    public class AppraisalModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }

        public int ManagerId { get; set; }
        public EmployeeModel Manager { get; set; }

        public string? ManagerComments { get; set; }

        public string? EmployeeComments { get; set; }

        public int? ManagerRating { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }
    }
}
