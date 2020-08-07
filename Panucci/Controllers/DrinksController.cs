using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Panucci.Models;
using Panucci.Repositories.UnitOfWork;

namespace Panucci.Controllers
{
    public class DrinksController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IHostingEnvironment hostingEnvironment;

        public DrinksController(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Drinks
        public IActionResult Index()
        {
            return View(unitOfWork.Drinks.AllData());
        }

        // GET: Drinks/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = unitOfWork.Drinks.Find(id.Value);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // GET: Drinks/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Name,Price")] Drink drink, IFormFile Photo)
        {
            if (ModelState.IsValid && Photo != null)
            {
                unitOfWork.Drinks.AddOrUpdate(drink);
                unitOfWork.Save();
                Photo.CopyTo(new FileStream(hostingEnvironment.WebRootPath + "/Uploads/Drinks/" + drink.ID.ToString() + ".jpg", FileMode.Create));
                return RedirectToAction(nameof(Index));
            }
            return View(drink);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = unitOfWork.Drinks.Find(id.Value);
            if (drink == null)
            {
                return NotFound();
            }
            return View(drink);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ID,Name,Price")] Drink drink, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Drinks.AddOrUpdate(drink);
                unitOfWork.Save();
                if (Photo != null)
                {
                    Photo.CopyTo(new FileStream(hostingEnvironment.WebRootPath + "/Uploads/Drinks/" + drink.ID.ToString() + ".jpg", FileMode.Create));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(drink);
        }

        // GET: Drinks/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = unitOfWork.Drinks.Find(id.Value);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var drink = unitOfWork.Drinks.Find(id);
            unitOfWork.Drinks.Delete(drink);
            unitOfWork.Save();
            FileInfo Photo = new FileInfo(hostingEnvironment.WebRootPath + "/Uploads/Drinks/" + id.ToString() + ".jpg");
            if (Photo.Exists)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Photo.Delete();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
