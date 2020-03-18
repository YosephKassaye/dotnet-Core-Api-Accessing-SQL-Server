using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnet_rest_api_with_sql.Data;
namespace dotnet_rest_api_with_sql.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
          private readonly IStudentRepository _repository;
        
        public StudentController(IStudentRepository repository)
        {
            
            _repository = repository;

        }
             [HttpGet("getStudent")]
          public async Task<IActionResult> GetUSAStatesAsync()
        {
          var model = await _repository.GetStudentDetail();
          return Ok(model);
        }
    }
}