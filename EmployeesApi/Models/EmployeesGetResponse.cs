using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApi.Models
{
    public class EmployeesGetResponse
    {
        public List<EmployeesGetResponseItem> Data { get; set; }
    }

    public class EmployeesGetResponseItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
    }
}
