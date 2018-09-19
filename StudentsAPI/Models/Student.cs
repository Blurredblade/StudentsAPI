//Zachary Cobb and Trent Bradburry
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float Gpa { get; set; }

        public override string ToString()
        {
            return Id + "," + Name + "," + Gpa;
        }
    }
}
