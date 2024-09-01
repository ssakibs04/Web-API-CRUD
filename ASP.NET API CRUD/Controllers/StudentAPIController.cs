using ASP.NET_API_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.WebSockets;

namespace ASP.NET_API_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly DataBaseFirstEF context;

        public StudentAPIController(DataBaseFirstEF context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudent()
        {
          var data= await context.Students.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>>GetStudentByID(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student std)
        {
            await context.Students.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(std);

            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(Student std,int id)
        {
            if (id != std.Id) {
                return BadRequest();
            
            }
            context.Entry(std).State=EntityState.Modified;
            context.SaveChanges();
            return Ok(std);


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent( int id)
        {
            var std = await context.Students.FindAsync(id);
            if(std == null)
            {
                NotFound();
            }
           context.Students.Remove(std);   
            await context.SaveChangesAsync();
            return Ok(std);


        }


    }
}
