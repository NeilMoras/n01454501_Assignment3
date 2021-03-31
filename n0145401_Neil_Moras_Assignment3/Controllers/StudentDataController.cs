﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using n0145401_Neil_Moras_Assignment3.Models; // using the models folder inorder to access the student defining feilds and the database
using MySql.Data.MySqlClient; // connects to the mySql database NuGet package


namespace n0145401_Neil_Moras_Assignment3.Controllers
{
    public class StudentDataController : ApiController
    {   // ShoolDsContext api controller which will connect and get query from the schoolDb database in MAMP 
        private SchoolDbContext School = new SchoolDbContext();

        //This controller will access the students table from the schooldb database in MAMP
        /// <summary>
        /// this code will retrive list of students and  their fields from the database to be used accordingly
        /// </summary>
        /// <example> GET api/StudentData/ListStudents </example>
        /// <returns>List of students with their related  columns from the students table</returns>

        [HttpGet]
        public IEnumerable<Student> ListStudents() // since its a list of students, we have to use IEinumerable
        {
            //Links and creates a connection to mySql database
            MySqlConnection Connection = School.AccessDatabase();
            //Connection linked and opens between the database and the web server
            Connection.Open();
            //creates a new command to run the query from the database
            MySqlCommand Command = Connection.CreateCommand();
            // allows to write a query and send it to the database to retrive the information from students table
            Command.CommandText = "Select * from Students";
            //Converts the query and stores it in a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();
            // creates an empty array to store the list of students

            List<Student> Students = new List<Student> { };
            // using while loop to itirate the list information fromt the students table 
            while (ResultSet.Read())
            {
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];

                //creating a new variable and lining it to the models controller 
                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;
                //adding the variables to the empty list.
                Students.Add(NewStudent);
             }
            //Closing the connection once the information is retrieved from the database
            Connection.Close();
            // outputs the lists of students to the web browser
            return Students;
        }

        /// <summary>
        /// this code will retreives single row of  student and  their fields information from the database to be used accordingly
        /// </summary>
        /// <example>GET/StudentData/FindStudents/{id}</example>
        /// <param name="id"> interger as an input as studentId</param>
        /// <returns> A single row of teacher information from the students table</returns>

        [HttpGet]
          public Student FindStudent(int id)
        {
            Student NewStudent = new Student();
            //Links and creates a connection to mySql database
            MySqlConnection Connection = School.AccessDatabase();
            //Connection linked and opens between the database and the web server
            Connection.Open();
            //creates a new command to run the query from the database
            MySqlCommand Command = Connection.CreateCommand();
            // allows to write a query and send it to the database to retrive the information with the help of id as a parameter input.This will retrive only one row of student data from the students table as its looking up through each teacher id
            Command.CommandText = "Select * from Students where studentid = " + id;
            //Converts the query and  stores it in a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            // using while loop to itirate the list information fromt the teachers table.


            while (ResultSet.Read())
            {
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];
                //creating a new variable and lining it to the models controller 
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;




            }
            // outputs a row of one student data from the database to the web browser
            return NewStudent;
        }

    }
}
