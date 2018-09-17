using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace StudentsAPI.Models
{
    public class DataLayer
    {
        private List<Student> _students = new List<Student>();
        string path = @"StudentData.csv";
        public void LoadFile()
        {
            string[] lines = File.ReadAllLines(path);
            for(int i = 0; i < lines.Length; i++)
            {
                string[] value = lines[i].Split(",");
                _students.Add(new Student { Id = value[0], Name = value[1], Gpa = float.Parse(value[2]) });

            }
        }

        public void SaveChanges()
        {
            string[] write = new string[_students.Count];
            int count = 0;
            foreach (Student student in _students)
            {
                write[count] = student.ToString();
                count++;
            }
            File.WriteAllLines(path, write);
        }

        public List<Student> GetStudents()
        {
            return _students;
        }

        public Student GetStudentByID(string id)
        {
            return _students.Find(s => s.Id == id);
        }

        public void CreateStudent(Student student)
        {
            _students.Add(new Student { Id = student.Id, Name = student.Name, Gpa = student.Gpa });
        }

        public void Delete(Student student)
        {
            _students.Remove(student);
        }

        public float[] Range()
        {
            float[] range = new float[]
{
                _students.Min(g => g.Gpa),
                _students.Max(g => g.Gpa)
            };
            return range;
        }
    }
}
