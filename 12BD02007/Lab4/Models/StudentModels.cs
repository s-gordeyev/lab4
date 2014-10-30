using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab4;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data;

namespace Lab4.Models
{
    #region Models
    public class Student
    {
        public int ID;
        public string Name;
        public decimal GPA;

        public Student(int id, string name) {
            this.ID = id;
            this.Name = name;
        }

        public Student(string name)
        {
            this.Name = name;
        }

        public Student(string name, decimal GPA)
        {
            this.Name = name;
            this.GPA = GPA;
        }

        public Student(int id, string name, decimal GPA)
        {
            this.ID = id;
            this.Name = name;
            this.GPA = GPA;
        }

        //public int ID {get; set;}
        //public string Name {get; set;}
    }
    #endregion

    #region ServiceLayer
    public class DAO 
    {
        private const string connectionString = "Data Source=LATITUDE;Initial Catalog=Lab4;Integrated Security=SSPI;User ID=sa;Password=1;";

        private List<Student> SelectStudents(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                using (SqlDataReader sqlReader = command.ExecuteReader())
                {
                    return (from IDataRecord r in sqlReader
                            select new Student((int)r["id"], r["Name"].ToString(), (decimal)r["GPA"])).ToList<Student>();
                }
            }
        }

        private void ExecuteDML(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public List<Student> GetStudents() {
            return this.SelectStudents("SELECT * FROM [dbo].[STUDENT]");
        }

        public void InsertStudent(Student std) {
            try
            {
                this.ExecuteDML("insert into [dbo].[STUDENT](NAME, GPA) values('" + std.Name + "', " + std.GPA + ")");
            }
            catch (Exception e) { 
            }
        }

        public void DeleteStudent(int studentID)
        {
            try
            {
                this.ExecuteDML("delete from [dbo].[STUDENT] where ID = " + studentID);
            }
            catch (Exception e)
            {
            }
        }

        public void UpdateStudent(Student std)
        {
            try
            {
                this.ExecuteDML("update [dbo].[STUDENT] set Name = '" + std.Name + "', GPA = " + std.GPA + " where ID = " + std.ID);
            }
            catch (Exception e)
            {
            }
        }


    }
    #endregion
}