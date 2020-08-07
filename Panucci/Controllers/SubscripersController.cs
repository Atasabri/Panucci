using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Panucci.Repositories.UnitOfWork;

namespace Panucci.Controllers
{
    public class SubscripersController : Controller
    {
        private IUnitOfWork unitOfWork;

        public SubscripersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(unitOfWork.Subscripers.AllData());
        }

        [HttpPost]
        public ActionResult SendMail(string Subject, IFormFile File, List<string> Emails)
        {
            unitOfWork.Emails.Send_Mail(Subject, File, Emails);
            return RedirectToAction(nameof(Index));
        }
    }
}