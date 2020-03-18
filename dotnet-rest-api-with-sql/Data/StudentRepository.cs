using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnet_rest_api_with_sql.Models;

namespace dotnet_rest_api_with_sql.Data
{
    internal class StudentRepository : IStudentRepository
    {
        
         private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<List<Student>> GetStudentDetail()
        {
               return await _context.Student.ToListAsync();
        }
    }
}