using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n0145401_Neil_Moras_Assignment3.Models;
using System.Diagnostics;



namespace n0145401_Neil_Moras_Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        
        //GET: Teacher/List
        public ActionResult List(string Searchkey =  null)
        {
            Debug.WriteLine("The Search key the is inputted is ");
            Debug.WriteLine(Searchkey);
            // get accesss from the Teachers dataController
            TeacherDataController controller = new TeacherDataController();
           IEnumerable<Teacher> Teachers = controller.ListTeachers(Searchkey);
            return View(Teachers);
        }

        //GET: Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            
            return View(NewTeacher);
        }
    }
}