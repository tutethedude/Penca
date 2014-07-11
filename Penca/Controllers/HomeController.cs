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
        private DateTime FIRST_ROUND_LIMIT = new DateTime(2014, 6, 11, 5, 0, 0, DateTimeKind.Utc);
        private DateTime EIGTH_ROUND_LIMIT = new DateTime(2014, 6, 28, 5, 0, 0, DateTimeKind.Utc);
        private DateTime QUARTER_ROUND_LIMIT = new DateTime(2014, 7, 4, 8, 0, 0, DateTimeKind.Utc);
        private DateTime SEMI_ROUND_LIMIT = new DateTime(2014, 7, 8, 8, 0, 0, DateTimeKind.Utc);
        private DateTime FINAL_ROUND_LIMIT = new DateTime(2014, 7, 12, 8, 0, 0, DateTimeKind.Utc);

        public ActionResult Index(string orderBy)
        {
            orderBy = orderBy == "date" ? "date" : "group";
            var model = new MainModel { OrderBy = orderBy };
            var user = CurrentUser();
            var matches = new List<Match>();
            var results = new List<Result>();
            var matchesEigth = new List<Match>();
            var resultsEigth = new List<Result>();
            using (var db = new UsersContext())
            {
                model.Users = db.UserProfiles.ToList();
            }
            using (var db = new MatchContext())
            {
                matches = db.Matches.Where(m => m.MatchId <= 62).ToList();
                results = db.Results.Where(r => r.MatchId <= 62).ToList();
            }
            model.FirstRoundEnabled = FirstRoundEnabled();
            model.EigthRoundEnabled = EigthRoundEnabled();
            model.QuarterRoundEnabled = QuarterRoundEnabled();
            model.SemiRoundEnabled = SemiRoundEnabled();
            model.FinalRoundEnabled = FinalRoundEnabled();
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
                        for (int i = 0; i < 48 * 2; i = i + 2)
                        {
                            var homeData = array[i].Split('!');
                            var awayData = array[i+1].Split('!');
                            var result = new Result { UserId = user.UserId, MatchId = int.Parse(homeData[0]), HomeScore = GetScore(homeData[1]), AwayScore = GetScore(awayData[1]) };
                            db.Results.Add(result);
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
            return User.Identity.IsAuthenticated && DateTime.Now < FIRST_ROUND_LIMIT;
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
                var score = new Score { User = u, ScoreByDate = new List<int>(), AccumScoreByDate = new List<int>(), ScoreByGroup = new List<int>() };
                var lastMatch = matches.Where(m => m.HomeScore >= 0).OrderBy(m => m.MatchId).LastOrDefault();
                if (lastMatch != null)
                {
                    var scoreDic = new SortedDictionary<DateTime, int>();
                    var accumScoreDic = new SortedDictionary<DateTime, int>();
                    var groupDic = new SortedDictionary<string, int>();
                    var userResults = results.Where(r => r.MatchId <= lastMatch.MatchId && r.UserId == u.UserId).OrderBy(r => r.MatchId).ToList();
                    foreach (var r in userResults)
                    {
                        var match = matches.Where(m => m.MatchId == r.MatchId).FirstOrDefault();
                        r.Match = match;
                        var points = r.ComputeScore();
                        score.Points += points;

                        if (scoreDic.ContainsKey(match.MatchDate.Date))
                            scoreDic[match.MatchDate.Date] += points;
                        else
                            scoreDic[match.MatchDate.Date] = points;

                        accumScoreDic[match.MatchDate.Date] = score.Points;

                        if (match.MatchId <= 48)
                        {
                            if (groupDic.ContainsKey(match.Group))
                                groupDic[match.Group] += points;
                            else
                                groupDic[match.Group] = points;
                        }
                    }
                    foreach (var k in scoreDic.Keys)
                    {
                        score.ScoreByDate.Add(scoreDic[k]);
                        score.AccumScoreByDate.Add(accumScoreDic[k]);
                    }
                    foreach (var g in groupDic.Keys)
                    {
                        score.ScoreByGroup.Add(groupDic[g]);
                    }
                }
                ranking.Add(score);
            }
            return ranking.OrderByDescending(s => s.Points);
        }

        [HttpPost]
        public ActionResult SaveEigthRound(string data)
        {
            if (EigthRoundEnabled())
            {
                var user = CurrentUser();
                var array = data.Split('|');
                if (array.Length == (8 * 2) + 1)
                {
                    ClearResultsEigthRound(user.UserId);
                    using (var db = new MatchContext())
                    {
                        for (int i = 0; i < 8 * 2; i = i + 2)
                        {
                            var homeData = array[i].Split('!');
                            var awayData = array[i + 1].Split('!');
                            var result = new Result { UserId = user.UserId, MatchId = int.Parse(homeData[0]), HomeScore = GetScore(homeData[1]), AwayScore = GetScore(awayData[1]) };
                            db.Results.Add(result);
                        }
                        db.SaveChanges();
                    }

                }
            }
            return Json("OK");
        }

        private void ClearResultsEigthRound(int userId)
        {
            using (var db = new MatchContext())
            {
                foreach (var r in db.Results.Where(r => r.UserId == userId && r.MatchId >= 49 && r.MatchId <= 56))
                    db.Results.Remove(r);
                db.SaveChanges();
            }
        }

        private bool EigthRoundEnabled()
        {
            return User.Identity.IsAuthenticated && DateTime.Now < EIGTH_ROUND_LIMIT;
        }

        [HttpPost]
        public ActionResult SaveQuarterRound(string data)
        {
            if (QuarterRoundEnabled())
            {
                var user = CurrentUser();
                var array = data.Split('|');
                if (array.Length == (4 * 2) + 1)
                {
                    ClearResultsQuarterRound(user.UserId);
                    using (var db = new MatchContext())
                    {
                        for (int i = 0; i < 4 * 2; i = i + 2)
                        {
                            var homeData = array[i].Split('!');
                            var awayData = array[i + 1].Split('!');
                            var result = new Result { UserId = user.UserId, MatchId = int.Parse(homeData[0]), HomeScore = GetScore(homeData[1]), AwayScore = GetScore(awayData[1]) };
                            db.Results.Add(result);
                        }
                        db.SaveChanges();
                    }

                }
            }
            return Json("OK");
        }

        private void ClearResultsQuarterRound(int userId)
        {
            using (var db = new MatchContext())
            {
                foreach (var r in db.Results.Where(r => r.UserId == userId && r.MatchId >= 57 && r.MatchId <= 60))
                    db.Results.Remove(r);
                db.SaveChanges();
            }
        }

        private bool QuarterRoundEnabled()
        {
            return User.Identity.IsAuthenticated && DateTime.Now < QUARTER_ROUND_LIMIT;
        }

        [HttpPost]
        public ActionResult SaveSemiRound(string data)
        {
            if (SemiRoundEnabled())
            {
                var user = CurrentUser();
                var array = data.Split('|');
                if (array.Length == (2 * 2) + 1)
                {
                    ClearResultsSemiRound(user.UserId);
                    using (var db = new MatchContext())
                    {
                        for (int i = 0; i < 2 * 2; i = i + 2)
                        {
                            var homeData = array[i].Split('!');
                            var awayData = array[i + 1].Split('!');
                            var result = new Result { UserId = user.UserId, MatchId = int.Parse(homeData[0]), HomeScore = GetScore(homeData[1]), AwayScore = GetScore(awayData[1]) };
                            db.Results.Add(result);
                        }
                        db.SaveChanges();
                    }

                }
            }
            return Json("OK");
        }

        private void ClearResultsSemiRound(int userId)
        {
            using (var db = new MatchContext())
            {
                foreach (var r in db.Results.Where(r => r.UserId == userId && r.MatchId >= 61 && r.MatchId <= 62))
                    db.Results.Remove(r);
                db.SaveChanges();
            }
        }

        private bool SemiRoundEnabled()
        {
            return User.Identity.IsAuthenticated && DateTime.Now < SEMI_ROUND_LIMIT;
        }

        [HttpPost]
        public ActionResult SaveFinalRound(string data)
        {
            if (FinalRoundEnabled())
            {
                var user = CurrentUser();
                var array = data.Split('|');
                if (array.Length == (2 * 2) + 1)
                {
                    ClearResultsFinalRound(user.UserId);
                    using (var db = new MatchContext())
                    {
                        for (int i = 0; i < 2 * 2; i = i + 2)
                        {
                            var homeData = array[i].Split('!');
                            var awayData = array[i + 1].Split('!');
                            var result = new Result { UserId = user.UserId, MatchId = int.Parse(homeData[0]), HomeScore = GetScore(homeData[1]), AwayScore = GetScore(awayData[1]) };
                            db.Results.Add(result);
                        }
                        db.SaveChanges();
                    }

                }
            }
            return Json("OK");
        }

        private void ClearResultsFinalRound(int userId)
        {
            using (var db = new MatchContext())
            {
                foreach (var r in db.Results.Where(r => r.UserId == userId && r.MatchId >= 63 && r.MatchId <= 64))
                    db.Results.Remove(r);
                db.SaveChanges();
            }
        }

        private bool FinalRoundEnabled()
        {
            return User.Identity.IsAuthenticated && DateTime.Now < FINAL_ROUND_LIMIT;
        }
    }
}
