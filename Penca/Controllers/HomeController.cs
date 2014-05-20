using Penca.Filters;
using Penca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Penca.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        private static DateTime FIRST_ROUND_LIMIT = new DateTime(2014, 6, 11);
        public ActionResult Index()
        {
            var model = new MainModel();
            var user = CurrentUser();
            model.FirstRoundEnabled = DateTime.Now.Date < FIRST_ROUND_LIMIT;
            using (MatchContext db = new MatchContext())
            {
                model.Matches = db.Matches.Where(m => m.MatchId <= 48).ToList();
                model.MyResults = db.Results.Where(r => r.MatchId <= 48 && r.UserId == user.UserId).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveFirstRound(string data)
        {
            if (User.Identity.IsAuthenticated && DateTime.Now.Date < FIRST_ROUND_LIMIT)
            {
                var user = CurrentUser();
                var array = data.Split('|');
                if (array.Length == (48 * 2) + 1)
                {
                    ClearResultsFirstRound(user.UserId);
                    using (MatchContext db = new MatchContext())
                    {
                        var matchId = 1;
                        for (int i = 0; i < 48 * 2; i = i + 2)
                        {
                            var result = new Result { UserId = user.UserId, MatchId = matchId, HomeScore = GetScore(array[i]), AwayScore = GetScore(array[i + 1]) };
                            db.Results.Add(result);
                            matchId++;
                        }
                        db.SaveChanges();
                    }

                }
            }
            return Json("OK");
        }

        private UserProfile CurrentUser()
        {
            using (UsersContext db = new UsersContext())
            {
                return db.UserProfiles.FirstOrDefault(u => u.UserName == User.Identity.Name);
            }
        }

        private void ClearResultsFirstRound(int userId)
        {
            using (MatchContext db = new MatchContext())
            {
                foreach (var r in db.Results.Where(r => r.UserId == userId && r.MatchId <= 48))
                    db.Results.Remove(r);
                db.SaveChanges();
            }
        }

        private int GetScore(string score)
        {
            var result = 0;
            int.TryParse(score, out result);
            return result;
        }
    }
}
