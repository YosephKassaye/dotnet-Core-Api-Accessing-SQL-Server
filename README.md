# Dtonet Core Web Api With SQL Server Database

This sample project contains examples to implement dotnet Core Web Api using SQL Srever database. You can learn how to build ASP.NET Core REST API consuming SQL Server database 

### Contents

[About this sample](#about-this-sample)<br/>
[Before you begin](#before-you-begin)<br/>
[Run this sample](#run-this-sample)<br/>
[Sample details](#sample-details)<br/>
[Disclaimers](#disclaimers)<br/>
[Related links](#related-links)<br/>

<a name=about-this-sample></a>

## About this sample

- **Applies to:** SQL Server 2016 (or higher), Azure SQL Database
- **Key features:**  Develop ASP.NET Rest API with SQL Srever at the back
- **Programming Language:** C#
- **Authors:** Yoseph A. Kassaye

<a name=before-you-begin></a>

## Before you begin

To run this sample, you need the following prerequisites.

**Software prerequisites:**

1. SQL Server 2016 (or higher)  
2. Visual Studio Code Editor

<a name=run-this-sample></a>

## Run this sample

### Setup

1. Use SQL Server management Studio and Cretae your database with the following table:....
```
Create Database StudentsDB --This is to create a database called StudentsDB
Use StudentsDB

CREATE TABLE [dbo].[Student](
	[StudentId] [int] primary key,
	[FName] [varchar](50) ,
	[LName] [varchar](50) ,
	[DOB] [datetime] ,
	[DepartmentId] [int] ,
	[Gender] [char](1) 
)
```

2. Create a folder and make this the current folder (for this case it is dotnetCoreApi-Accessing-SQL-Server)
3. Open the folder in Visual Studio Code and type the folowing dotnet new webapi -n dotnet-rest-api-with-sql 
4. cd to  dotnet-rest-api-with-sql folder 
5. Remove the https://localhost:5001 from launchSettings.json
6. Comment  // app.UseHttpsRedirection(); from Startup.cs 
7. Add a connection string in appsettings.json as follows:

```
"ConnectionStrings":
	{
  "StudentConnection": "Server=.;Database=SMS_NEW;Trusted_Connection=True"
    },
```
8. Create the the following two Folders:
    Data- this is to hold DataContext and Repository
    Models- this is to hold Models 
9. OPen Models folder and create a student  class with properties as follows:

```
 using System;
 namespace dotnet_rest_api_with_sql.Models
{
    public class Student {
       public	int	StudentId	{ get; set; }	
        public	string	FName	{ get; set; }	
        public	string	LName	{ get; set; }	
        public	DateTime	DOB	{ get; set; }	
        public	int	DepartmentId	{ get; set; }	
        public	string	Gender	{ get; set; }	

    }
    
}
```
10. Open Data Folder and cretae DataContext class inherits from DbContext as:

```    
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
```

11. Include IStudentRepository Interface under Dtaa folder as follows:

```
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
```
12. Include StudentRepository class under Data Folder and implement IStudentRepository Interface  as follows:

```
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

```
13. Open Startup.cs file and modify ConfigureServices methodto  add DataContext and Scope as follows: 

```
//Make sure to include the following namespaces
//using dotnet_rest_api_with_sql.Data;
//using Microsoft.EntityFrameworkCore;
    public void ConfigureServices(IServiceCollection services)
        {

             services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("StudentConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddCors();
            services.AddScoped<IStudentRepository, StudentRepository>();
             services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }
```
14. Now the last Setp is to add StudentsController under Controllers folder

```
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
```

### Build and run the  services

    1. To build the service, open a new terminal from Terminal Menu and type  **dotnet build** command .
    2. Run sample app using  **dotnet watch run** command from terminal window 

<a name=sample-details></a>

## Sample details

This sample application shows how to create dotnet Core Web Api, cretea components, configure to fetch data from SQL Server database.

<a name=disclaimers></a>

## Disclaimers
The code included in this sample is not intended demonstrate some general guidance and architectural patterns for web development. It contains minimal code required to create REST API, and it uses some repository pattern. Sample uses built-in ASP.NET Core Dependency Injection mechanism; however, this is not prerequisite.
You can easily modify this code to fit the architecture of your application.

<a name=related-links></a>


## Code of Conduct
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). 



## Questions
Email questions to: [yadugna@gmail.com](mailto: yadugna@gmail.com).