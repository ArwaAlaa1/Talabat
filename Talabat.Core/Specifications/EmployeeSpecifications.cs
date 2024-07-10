using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class EmployeeSpecifications:BaseSpecification<Employee>
    {
        public EmployeeSpecifications()
        {
            includes.Add(e => e.department);
        }
        public EmployeeSpecifications(int id):base(e=>e.Id==id)
        {
            includes.Add(e => e.department);
        }
    }
}
