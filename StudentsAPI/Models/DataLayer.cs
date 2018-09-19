//Zachary Cobb and Trent Bradburry
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace StudentsAPI.Models
{
    public class DataLayer
    {
        private List<Student> _students = new List<Student>();
        string path = @"StudentData.csv";

        //Loads csv into _students List
        public void LoadFile()
        {
            string[] lines = File.ReadAllLines(path);
            for(int i = 0; i < lines.Length; i++)
            {
                string[] value = lines[i].Split(",");
                _students.Add(new Student { Id = value[0], Name = value[1], Gpa = float.Parse(value[2]) });
            }
        }

        //Writes list of students to csv file
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

        //Returns the List of all students
        public List<Student> GetStudents()
        {
            return _students;
        }

        //Returns a student in _students List using the given ID
        public Student GetStudentByID(string id)
        {
            return _students.Find(s => s.Id == id);
        }

        //Adds a new student to _students List using attributes from given student
        public void CreateStudent(Student student)
        {
            _students.Add(new Student { Id = student.Id, Name = student.Name, Gpa = student.Gpa });
        }

        //Removes given student from _students List
        public void Delete(Student student)
        {
            _students.Remove(student);
        }

        //Returns the minimum and maximum GPA for all students in _students List
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
