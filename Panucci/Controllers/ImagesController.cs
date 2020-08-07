using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Panucci.Models;
using Microsoft.AspNetCore.Hosting;
using Panucci.Repositories.UnitOfWork;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Panucci.Controllers
{
    public class ImagesController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IHostingEnvironment hostingEnvironment;

        public ImagesController(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Images
        public IActionResult Index()
        {
            return View(unitOfWork.Images.AllData());
        }

        // GET: Images/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = unitOfWork.Images.Find(id.Value);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Description")] Image image, IFormFile Photo)
        {
            if (ModelState.IsValid && Photo != null)
            {
                unitOfWork.Images.AddOrUpdate(image);
                unitOfWork.Save();
                Photo.CopyTo(new FileStream(hostingEnvironment.WebRootPath + "/Uploads/Images/" + image.ID.ToString() + ".jpg", FileMode.Create));
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        // GET: Images/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = unitOfWork.Images.Find(id.Value);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Description")] Image image, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Images.AddOrUpdate(image);
                unitOfWork.Save();
                if (Photo != null)
                {
                    Photo.CopyTo(new FileStream(hostingEnvironment.WebRootPath + "/Uploads/Images/" + image.ID.ToString() + ".jpg", FileMode.Create));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        // GET: Images/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = unitOfWork.Images.Find(id.Value);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var image = unitOfWork.Images.Find(id);
            unitOfWork.Images.Delete(image);
            unitOfWork.Save();
            FileInfo Photo = new FileInfo(hostingEnvironment.WebRootPath + "/Uploads/Images/" + id.ToString() + ".jpg");
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
