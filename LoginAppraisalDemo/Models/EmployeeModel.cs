namespace LoginAppraisalDemo.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public int? ManagerId { get; set; } // Assuming ManagerId can be nullable
        public string Password { get; set; }
    }
}
