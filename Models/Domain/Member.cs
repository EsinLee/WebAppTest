namespace WebAppTest.Models.Domain
{
    public class Member
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class Employee
    {
        public Guid Id { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string JobCategory { get; set; }
        public string JobName { get; set; }
        public string Salary { get; set; }
        public double Seniority { get; set; }
    }

    public class Security
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string SecurityLevel { get; set; }
    }
}
