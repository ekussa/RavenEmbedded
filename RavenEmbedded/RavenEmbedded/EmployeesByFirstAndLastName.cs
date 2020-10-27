using System.Linq;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Linq.Indexing;

namespace RavenEmbedded
{
    public class EmployeesByFirstAndLastName : AbstractIndexCreationTask<Employee>
    {
        public EmployeesByFirstAndLastName()
        {
            Map =
                employees =>
                    from employee in employees
                    select new
                    {
                        FirstName = employee.FirstName.Boost(10),
                        LastName = employee.LastName
                    };
        }
    }
}
