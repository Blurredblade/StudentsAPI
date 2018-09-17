using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentsAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
/*
            if (_context.Students.Count() == 0)
            {
                // Create a new Student if collection is empty,
                // which means you can't delete all Students.
                _context.Students.Add(new Student { Name = "Test Student" });
                _context.SaveChanges();
            }
            */
        }

        [HttpGet]
        public ActionResult<List<Student>> GetAll()
        {
            return _context.Students.ToList();
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public ActionResult<Student> GetById(long id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpGet("/range")]
        public ActionResult<float[]> Get()
        {
            /*
            List<float> range = new List<float>();
            range.Add(_context.Students.Min(g => g.Gpa));
            range.Add(_context.Students.Max(g => g.Gpa));
            return range;
            */

            float[] range = new float[]
            {
                _context.Students.Min(g => g.Gpa),
                _context.Students.Max(g => g.Gpa)
            };
            return range;
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();

            return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
        }

        /* This method wasn't specified in the req
        [HttpPut("{id}")]
        public IActionResult Update(long id, Student student)
        {
            var stu = _context.Students.Find(id);
            if(stu == null)
            {
                return NotFound();
            }

            stu.Id = student.Id;
            stu.Name = student.Name;
            stu.Gpa = student.Gpa;

            _context.Students.Update(stu);
            _context.SaveChanges();
            return NoContent();
        }
        */

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var stu = _context.Students.Find(id);
            if(stu == null)
            {
                return NotFound();
            }

            _context.Students.Remove(stu);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
