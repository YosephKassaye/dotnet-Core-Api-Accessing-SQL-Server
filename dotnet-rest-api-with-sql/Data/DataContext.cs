using Microsoft.EntityFrameworkCore;
 

using dotnet_rest_api_with_sql.Models;
namespace dotnet_rest_api_with_sql.Data
{
    public class DataContext:DbContext 
    {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Student>  Student { get; set; }
 
    }
}