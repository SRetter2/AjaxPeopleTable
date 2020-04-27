using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AjaxPeopleTable.Models;
using AjaxPeopleTable.Data;

namespace AjaxPeopleTable.Controllers
{
    public class HomeController : Controller
    {
        private string _conn = "Data Source=.\\sqlexpress;Initial Catalog=MySecondDb;Integrated Security=True;";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPeople()
        {
            PersonDb db = new PersonDb(_conn);
            return Json(db.GetAllPeople());
        }
        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            PersonDb db = new PersonDb(_conn);
            db.AddPerson(person);
            return Json(person);
        }
        [HttpPost]
        public IActionResult DeletePerson(int id)
        {
            PersonDb db = new PersonDb(_conn);
            db.DeletePerson(id);
            return Json(id);
        }
        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
            PersonDb db = new PersonDb(_conn);
            db.EditPerson(person);
            return Json(person);
        }

    }
}
