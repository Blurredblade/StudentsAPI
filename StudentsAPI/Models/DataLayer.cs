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
        List<Student> students = new List<Student>();
        string path = @"StudentData.csv";
        public void LoadFile()
        {
            string[] lines = File.ReadAllLines(path);
            for(int i = 0; i < lines.Length; i++)
            {
                string[] value = lines[i].Split(",");
                students.Add(new Student { Id = value[0], Name = value[1], Gpa = float.Parse(value[2]) });

            }
        }

        public void WriteFile()
        {
            string[] write = new string[students.Count];
            int count = 0;
            foreach (Student student in students)
            {
                write[count] = student.ToString();
                count++;
            }
            File.WriteAllLines(path, write);
        }

        public List<Student> GetStudents()
        {
            return students;
        }

        public Student GetStudentByID(string id)
        {
            foreach(Student student in students)
            {
                if(student.Id == id)
                {
                    return student;
                }
            }

            return null;
        }

        public void CreateStudent(Student student)
        {
            students.Add(new Student { Id = student.Id, Name = student.Name, Gpa = student.Gpa });
        }

        public float[] Range()
        {
            float[] range = new float[]
{
                students.Min(g => g.Gpa),
                students.Max(g => g.Gpa)
            };
            return range;
        }
    }
}
