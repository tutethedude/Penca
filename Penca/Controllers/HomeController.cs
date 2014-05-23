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
        private DateTime FIRST_ROUND_LIMIT = new DateTime(2014, 6, 11);

        public ActionResult Index()
        {
            var model = new MainModel();
            var user = CurrentUser();
            var matches = new List<Match>();
            var results = new List<Result>();
            using (var db = new UsersContext())
            {
                model.Users = db.UserProfiles.ToList();
            }
            using (var db = new MatchContext())
            {
                matches = db.Matches.Where(m => m.MatchId <= 48).ToList();
                results = db.Results.Where(r => r.MatchId <= 48).ToList();
            }
            model.FirstRoundEnabled = FirstRoundEnabled();
            model.Matches = matches;
            if (user != null)
            {
                model.MyResults = results.Where(r => r.UserId == user.UserId).ToList();
                model.OtherResults = results.Where(r => r.UserId != user.UserId).ToList();
            }
            else
            {
                model.MyResults = new List<Result>();
                model.OtherResults = results.ToList();
            }
            model.Ranking = ComputeFirstRoundScores(matches, results);
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveFirstRound(string data)
        {
            if (FirstRoundEnabled())
            {
                var user = CurrentUser();
                var array = data.Split('|');
                if (array.Length == (48 * 2) + 1)
                {
                    ClearResultsFirstRound(user.UserId);
                    using (var db = new MatchContext())
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
            if (User.Identity.IsAuthenticated)
            {
                using (var db = new UsersContext())
                {
                    return db.UserProfiles.FirstOrDefault(u => u.UserName == User.Identity.Name);
                }
            }
            return null;
        }

        private void ClearResultsFirstRound(int userId)
        {
            using (var db = new MatchContext())
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

        private bool FirstRoundEnabled()
        {
            return User.Identity.IsAuthenticated && DateTime.Now.Date < FIRST_ROUND_LIMIT;
        }

        private IEnumerable<Score> ComputeFirstRoundScores(IEnumerable<Match> matches, IEnumerable<Result> results)
        {
            var ranking = new List<Score>();
            var users = new List<UserProfile>();
            using (var db = new UsersContext())
            {
                users = db.UserProfiles.ToList();
            }

            foreach (var u in users)
            {
                var score = new Score { User = u, ScoreByDate = new List<int>(), AccumScoreByDate = new List<int>() };
                var lastMatch = matches.Where(m => m.HomeScore >= 0).OrderBy(m => m.MatchId).LastOrDefault();
                if (lastMatch != null)
                {
                    var scoreDic = new SortedDictionary<DateTime, int>();
                    var accumScoreDic = new SortedDictionary<DateTime, int>();
                    var userResults = results.Where(r => r.MatchId <= lastMatch.MatchId && r.UserId == u.UserId).ToList();
                    foreach (var r in userResults)
                    {
                        var match = matches.Where(m => m.MatchId == r.MatchId).FirstOrDefault();
                        r.Match = match;
                        var points = r.ComputeFirstRoundScore();
                        score.Points += points;

                        if (scoreDic.ContainsKey(match.MatchDate.Date))
                            scoreDic[match.MatchDate.Date] += points;
                        else
                            scoreDic[match.MatchDate.Date] = points;

                        //if (accumScoreDic.ContainsKey(match.MatchDate.Date))
                        //    accumScoreDic[match.MatchDate.Date] += score.Points;
                        //else
                            accumScoreDic[match.MatchDate.Date] = score.Points;

                    }
                    foreach (var k in scoreDic.Keys)
                    {
                        score.ScoreByDate.Add(scoreDic[k]);
                        score.AccumScoreByDate.Add(accumScoreDic[k]);
                    }
                }
                ranking.Add(score);
            }
            return ranking.OrderByDescending(s => s.Points);
        }

    }
}
