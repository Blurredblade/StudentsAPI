﻿using System;
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
        private DataLayer _data;

        public StudentController(StudentContext context)
        {
             _data = new DataLayer();
            _data.LoadFile();
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
            _data.WriteFile();

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
