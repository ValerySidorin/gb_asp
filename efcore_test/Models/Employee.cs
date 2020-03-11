using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace efcore_test.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Employee()
        {

        }
        public Employee(int id)
        {
            Id = id;
        }
    }
}
