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
        private QueryManager _qm;

        public StudentController()
        {
            _qm = new QueryManager();
        }

        [HttpGet]
        public ActionResult<List<Student>> GetAll()
        {
            return _qm.GetStudents();
        }

        [HttpGet("range", Name = "GetRange")]
        public ActionResult<float[]> Get()
        {
            return _qm.Range();
        }
       
        [HttpGet("{id}", Name = "GetStudent")]
        public ActionResult<Student> GetById(string id)
        {
            var student = _qm.GetStudentByID(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }
        
        [HttpPost]
        public IActionResult Create(Student student)
        {
            _qm.CreateStudent(student);

            return CreatedAtRoute("GetStudent", new { id = student.Student_Id }, student);
        }
       
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var student = _qm.GetStudentByID(id);
            if(student == null)
            {
                return NotFound();
            }

            _qm.Delete(id);
            return NoContent();
        }

        [HttpGet("inv")]
        public ActionResult<List<Invoice>> getInvoices()
        {
            return _qm.listInvoice();
        }
    }
}
