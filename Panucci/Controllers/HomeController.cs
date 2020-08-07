using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panucci.Models;
using Panucci.Repositories.UnitOfWork;

namespace Panucci.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
           return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Images()
        {
            return View(unitOfWork.Images.AllData());
        }

        public IActionResult Meals()
        {
            return View(unitOfWork.Meals.AllData());
        }
        public IActionResult Drinks()
        {
            return View(unitOfWork.Drinks.AllData());
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        

        [HttpPost]
        public JsonResult Subscribe(string Email)
        {
            unitOfWork.Subscripers.AddOrUpdate(new Subscriper { Email = Email});
            unitOfWork.Save();
            return Json(true);
        }

        [HttpPost]
        public ActionResult ContactUs(Contact contact)
        {
            unitOfWork.Contacts.AddOrUpdate(contact);
            unitOfWork.Save();
            ViewBag.Done = true;
            return View();
        }
        public string Error()
        {
            return "error";
        }
    }
}