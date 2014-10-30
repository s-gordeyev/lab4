using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab4.Models;
using System.Data.Linq;

namespace Lab4.Controllers
{
    public class StudentController : Controller
    {
        DAO dao = new DAO();

        //
        // GET: /Student/

        public ActionResult Index()
        {
            return View(dao.GetStudents());
        }

        public ActionResult Create(FormCollection FormColl)
        {
            var cs = FormColl.GetValues("CreateStudent");
           
            dao.InsertStudent(new Student(cs[0], Decimal.Parse(cs[1].Replace(',', '.'))));
       
            return Redirect("/");
        }

        public ActionResult Edit(FormCollection FormColl)
        {
            var cs = FormColl.GetValues("Edited");

            dao.UpdateStudent(new Student(Int32.Parse(cs[0]), cs[1], Decimal.Parse(cs[2])));
            return Redirect("/");
        }

        public ActionResult Delete(FormCollection FormColl)
        {
            var cs = FormColl.GetValues("Deleted");
            dao.DeleteStudent(Int32.Parse(cs[0]));

            return Redirect("/");
        }


    }
}
