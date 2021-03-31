using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n0145401_Neil_Moras_Assignment3.Models;

namespace n0145401_Neil_Moras_Assignment3.Controllers
{
    public class CLassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        //GET Class/List
        public ActionResult List()
        {

            ClassesDataController controller = new ClassesDataController();
            IEnumerable<Class> Classes = controller.ListClasses();
            

            return View(Classes);
        }
        //GET Class/Show/{id}
        public ActionResult Show(int id)
        {

            ClassesDataController controller = new ClassesDataController();
            Class NewClass = controller.FindClass(id);

            return View(NewClass);
        }
    }
}