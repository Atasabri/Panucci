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
    public class MealsController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IHostingEnvironment hostingEnvironment;

        public MealsController(IUnitOfWork unitOfWork,IHostingEnvironment hostingEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Meals
        public IActionResult Index()
        {
            return View(unitOfWork.Meals.AllData());
        }

        // GET: Meals/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = unitOfWork.Meals.Find(id.Value); 
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meals/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Name,Price")] Meal meal,IFormFile Photo)
        {
            if (ModelState.IsValid&&Photo!=null)
            {
                unitOfWork.Meals.AddOrUpdate(meal);
                unitOfWork.Save();
                Photo.CopyTo(new FileStream(hostingEnvironment.WebRootPath+"/Uploads/Meals/"+meal.ID.ToString()+".jpg", FileMode.Create));
                return RedirectToAction(nameof(Index));
            }
            return View(meal);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = unitOfWork.Meals.Find(id.Value);
            if (meal == null)
            {
                return NotFound();
            }
            return View(meal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ID,Name,Price")] Meal meal, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Meals.AddOrUpdate(meal);
                unitOfWork.Save();
                if(Photo!=null)
                {
                    Photo.CopyTo(new FileStream(hostingEnvironment.WebRootPath + "/Uploads/Meals/" + meal.ID.ToString() + ".jpg", FileMode.Create));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meal);
        }

        // GET: Meals/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = unitOfWork.Meals.Find(id.Value);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var meal = unitOfWork.Meals.Find(id);
            unitOfWork.Meals.Delete(meal);
            unitOfWork.Save();
            FileInfo Photo = new FileInfo(hostingEnvironment.WebRootPath + "/Uploads/Meals/" + id.ToString() + ".jpg");
            if(Photo.Exists)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Photo.Delete();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
