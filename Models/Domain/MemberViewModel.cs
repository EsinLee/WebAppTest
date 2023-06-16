namespace WebAppTest.Models.Domain
{
    public class AddMember
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class UpdateMember
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class AddEmployee
    {
        public string Company { get; set; }
        public string Department { get; set; }
        public string JobCategory { get; set; }
        public string JobName { get; set; }
        public string Salary { get; set; }
        public double Seniority { get; set; }
    }
    public class UpdateEmployee
    {
        public string Company { get; set; }
        public string Department { get; set; }
        public string JobCategory { get; set; }
        public string JobName { get; set; }
        public string Salary { get; set; }
        public double Seniority { get; set; }
    }

    public class AddSecurity
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
    public class UpdateSecurity
    {
        public string Password { get; set; }
    }
    public class UpdateSecurityAdmin
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string SecurityLevel { get; set; }
    }

    public class AddMemberAccount
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        // -------------------------------------------------
        public bool BoolDoAlert { get; set; }
        public string AlertString { get; set; }
    }

    public class LoginMemberAccount
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }

        // -------------------------------------------------
        public bool BoolDoAlert { get; set; }
        public string AlertString { get; set; }
    }
}
