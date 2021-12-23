using DevelTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevelTest.Controllers
{
    [Authorize]
    public class PollController : Controller
    {
        public ActionResult Index()
        {
            return View(new PollViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(PollViewModel model)
        {
            return View();
        }
    }
}