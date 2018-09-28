//Zachary Cobb and Trent Bradburry
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Dapper;

namespace StudentsAPI.Models
{
    public class QueryManager
    {
        private const String _dbCredentials = "Server=localhost;Database=studentdb;UID=ZCobb;Password=J`@$Kyx]2F^cpF`W";

        //Returns the List of all students
        public List<Student> GetStudents()
        {
            string sql = "SELECT * FROM Student;";
            List<Student> student = new List<Student>();
            using (var con = new MySqlConnection(_dbCredentials))
            {
                student = con.Query<Student>(sql).ToList();
            }
                return student;
        }

        //Returns a student using the given ID
        public Student GetStudentByID(string ID)
        {
            string sql = "SELECT * FROM Student WHERE Student_ID = @StuID;";
            Student student;
            using (var con = new MySqlConnection(_dbCredentials))
            {
                try {
                    student = con.QuerySingle<Student>(sql, new { StuID = ID });
                }
                catch
                {
                    student = null;
                }
                
            }
            return student;
        }

        //Adds a new student using attributes from given student
        public void CreateStudent(Student student)
        {
            string sql = "INSERT INTO Student Values (@Student_ID, @Student_name, @GPA);";
            using (var con = new MySqlConnection(_dbCredentials))
            {
                con.Execute(sql, new { Student_ID = student.Student_Id,
                                        Student_name = student.Student_Name,
                                        GPA = student.Gpa });
            }
        }

        //Removes given student from table
        public void Delete(string ID)
        {
            string sql = "DELETE FROM Student WHERE Student_ID = @StuID;";
            using (var con = new MySqlConnection(_dbCredentials))
            {
                con.Execute(sql, new { StuID = ID });
            }
        }

        //Returns min and max GPA from table in a float array
        public float[] Range()
        {
            float[] range = new float[2];
            
            /*
             * Alternate Method for range
            string sql = "SELECT MIN(GPA) FROM Student;";
            using (var con = new MySqlConnection(_dbCredentials))
            {
                range[0] = con.QuerySingle<float>(sql);
            }
            sql = "SELECT MAX(GPA) FROM Student;";
            using (var con = new MySqlConnection(_dbCredentials))
            {
                range[1] = con.QuerySingle<float>(sql);
            }
             *
             */

            string sql = "SELECT MIN(GPA) FROM Student;SELECT MAX(GPA) FROM Student;";
            using (var con = new MySqlConnection(_dbCredentials))
            {
                con.Open();
                using(var multi = con.QueryMultiple(sql))
                {
                    range[0] = multi.Read<float>().First();
                    range[1] = multi.Read<float>().First();
                }
                
            }
            return range;
        }

    }
}

