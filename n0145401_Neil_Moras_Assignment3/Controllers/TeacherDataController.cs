using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using n0145401_Neil_Moras_Assignment3.Models;
using MySql.Data.MySqlClient;

namespace n0145401_Neil_Moras_Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {     // ShoolDsContext api controller which will connect and get query from the schoolDb database in MAMP 
        private SchoolDbContext School = new SchoolDbContext();

           //This controller will access the teachers table from the schooldb database in MAMP
        /// <summary>
        /// this code will retrive list of authors and  their fields from the database to be used accordingly
        /// </summary>
        /// <example> GET api/TeacherData/ListTeachers </example>
        /// <returns>List of teachers with their related  columns from the teachers table</returns>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)  // since its a list of students, we have to use IEinumerable

        {     //Links and creates a connection to mySql database
            MySqlConnection Connection = School.AccessDatabase();
            //Connection linked and opens between the database and the web server
            Connection.Open();

            //creates a new command to run the query from the database
            MySqlCommand Command = Connection.CreateCommand();
            // allows to write a query and send it to the database to retrive the information from teachers table
            Command.CommandText = "select * from teachers where lower(teacherfname) like  lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";


            Command.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            Command.Prepare();
            //COnverts the query and stores it in a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            // creates an empty array to store the listo of teachers
            List<Teacher> Teachers = new List<Teacher> { };
            // using while loop o itirate the list information fromt the teachers table 
            while (ResultSet.Read())
            {
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                //creating a new variable and lining it to the models controller 
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //adding the variables to the empty list.
                Teachers.Add(NewTeacher);
            }

             //Closing the connection once the information is retrieved from the database
            Connection.Close();
            // outputs the lists of teachers to the web browser
            return Teachers;
        }


        /// <summary>
        /// this code will retreives single row of  author and  their fields information from the database to be used accordingly 
        /// Joining the Classes table inorder connect the course associated the respective teachers
        /// </summary>
        /// <example>GET/api/TeacherData/FindTeacher/{id}</example>
        /// <param name="id"> interger as an input as studentId</param>
        /// <returns> A single row of teacher information fromt the teachers table</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {    

            Teacher NewTeacher = new Teacher();
            //Links and creates a connection to mySql database
            MySqlConnection Connection = School.AccessDatabase();
            //Connection linked and opens between the database and the web server
            Connection.Open();
            //creates a new command to run the query from the database
            MySqlCommand Command = Connection.CreateCommand();
            // allows to write a query and send it to the database to retrive the information with the help of id as a parameter input.This will retrive only one row of teachers from the teachers table as its looking up hroug each teacher id
            Command.CommandText = "SELECT * FROM teachers,classes where teachers.teacherid = classes.teacherid and teachers.teacherid =" + id;
            //COnverts the query and  stores it in a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            // using while loop to itirate the list information fromt the teachers table.
            while (ResultSet.Read())
            {    
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];
                string ClassName = (string)ResultSet["classname"];  //including classname and classscode tot he teachers table
                string ClassCode = (string)ResultSet["classcode"];

                //creating a new variable and lining it to the models controller 
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
                NewTeacher.ClassCode = ClassCode;
                NewTeacher.ClassName = ClassName;


            }


            // outputs a row of one teacher fromt he database to the web browser
            return NewTeacher;
        }


        
    }
}
