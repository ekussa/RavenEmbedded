namespace RavenEmbedded
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Employee()
        {
            FirstName = LastName = string.Empty;
        }
    }
}
