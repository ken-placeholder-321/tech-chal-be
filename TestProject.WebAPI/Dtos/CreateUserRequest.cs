namespace TestProject.WebAPI.Dtos
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int MonthlySalary { get; set; }
        public int MonthlyExpense { get; set; }

    }
}
