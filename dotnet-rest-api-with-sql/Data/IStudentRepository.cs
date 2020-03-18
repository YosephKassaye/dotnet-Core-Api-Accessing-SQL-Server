using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rest_api_with_sql.Models;
namespace dotnet_rest_api_with_sql.Data
{
   public  interface IStudentRepository
    {
        Task<List<Student>> GetStudentDetail();
    }
}