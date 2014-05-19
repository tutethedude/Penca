using Penca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Penca.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new MainModel();
            using (MatchContext db = new MatchContext())
            {
                model.Matches = db.Matches.Where(m=>m.MatchId <= 48).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(string data)
        {

            return RedirectToAction("Index");
        }
    }
}
