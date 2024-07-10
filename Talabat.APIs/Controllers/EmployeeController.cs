using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
   
    public class EmployeeController : BaseApiContoller
    {
        private readonly IGenericRepository<Employee> repository;

        public EmployeeController(IGenericRepository<Employee> repository  )
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            var spec = new EmployeeSpecifications();
            var employees= await repository.GetAllWithSpecAsync(spec);
                return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var spec = new EmployeeSpecifications(id);
            var employees = await repository.GetByIdWithSpec(spec);
            if (employees is null)
                return NotFound(new ApiResponse(404));
            return Ok(employees);
        }
    }
}
