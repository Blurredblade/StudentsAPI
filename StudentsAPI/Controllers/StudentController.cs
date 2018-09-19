//Zachary Cobb and Trent Bradburry
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models;

namespace StudentsAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : Controller
    {
        private DataLayer _data;

        public StudentController()
        {
            _data = new DataLayer();
            _data.LoadFile();
        }

        [HttpGet]
        public ActionResult<List<Student>> GetAll()
        {
            return _data.GetStudents();
        }

        [HttpGet("range", Name = "GetRange")]
        public ActionResult<float[]> Get()
        {
            return _data.Range();
        }
       
        [HttpGet("{id}", Name = "GetStudent")]
        public ActionResult<Student> GetById(string id)
        {
            var student = _data.GetStudentByID(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }
        
        [HttpPost]
        public IActionResult Create(Student student)
        {
            _data.CreateStudent(student);
            _data.SaveChanges();

            return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
        }
       
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var student = _data.GetStudentByID(id);
            if(student == null)
            {
                return NotFound();
            }

            _data.Delete(student);
            _data.SaveChanges();
            return NoContent();
        }
    }
}
