using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Linq;

namespace Penca.Models
{
    public class MatchContext : DbContext
    {
        public MatchContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Result> Results { get; set; }
    }

    [Table("Match")]
    public class Match
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int MatchId { get; set; }
        public string Group { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime MatchDate { get; set; }
        public string MatchStadium { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public virtual IEnumerable<Result> Results { get; set; }

        public string HomeFlag() { return Flag(HomeTeam); }
        public string AwayFlag() { return Flag(AwayTeam); }

        private string Flag(string country)
        {
            switch (country)
            {
                case "Argelia":
                    return "alg.png";
                case "Camerún":
                    return "cmr.png";
                case "Costa de Marfil":
                    return "civ.png";
                case "Ghana":
                    return "gha.png";
                case "Nigeria":
                    return "nga.png";
                case "Australia":
                    return "aus.png";
                case "Irán":
                    return "irn.png";
                case "Japón":
                    return "jpn.png";
                case "República de Corea":
                    return "kor.png";
                case "Alemania":
                    return "ger.png";
                case "Bosnia y Herzegovina":
                    return "bih.png";
                case "Croacia":
                    return "cro.png";
                case "Bélgica":
                    return "bel.png";
                case "España":
                    return "esp.png";
                case "Francia":
                    return "fra.png";
                case "Grecia":
                    return "gre.png";
                case "Inglaterra":
                    return "eng.png";
                case "Italia":
                    return "ita.png";
                case "Países Bajos":
                    return "ned.png";
                case "Portugal":
                    return "por.png";
                case "Rusia":
                    return "rus.png";
                case "Suiza":
                    return "sui.png";
                case "Costa Rica":
                    return "crc.png";
                case "EEUU":
                    return "usa.png";
                case "Honduras":
                    return "hon.png";
                case "México":
                    return "mex.png";
                case "Argentina":
                    return "arg.png";
                case "Brasil":
                    return "bra.png";
                case "Chile":
                    return "chi.png";
                case "Colombia":
                    return "col.png";
                case "Ecuador":
                    return "ecu.png";
                case "Uruguay":
                    return "uru.png";
            }
            return "galera.png";
        }

    }

    [Table("Result")]
    public class Result
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ResultId { get; set; }
        public int MatchId { get; set; }
        [ForeignKey("MatchId")]
        public virtual Match Match { get; set; }
        public int UserId { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public string UserName(IEnumerable<UserProfile> users)
        {
            return users.FirstOrDefault(u => u.UserId == UserId).UserName;
        }

        public int ComputeFirstRoundScore()
        {
            if (Match.HomeScore >= 0 && Match.AwayScore >= 0)
            {
                if (Match.HomeScore == this.HomeScore && Match.AwayScore == this.AwayScore)
                    return FIRST_ROUND_EXACT_SCORE;
                else if (Match.HomeScore > Match.AwayScore && this.HomeScore > this.AwayScore && (Match.HomeScore == this.HomeScore || Match.AwayScore == this.AwayScore))
                    return FIRST_ROUND_PARTIAL_SCORE;
                else if (Match.HomeScore == Match.AwayScore && this.HomeScore == this.AwayScore && (Match.HomeScore == this.HomeScore || Match.AwayScore == this.AwayScore))
                    return FIRST_ROUND_PARTIAL_SCORE;
                else if (Match.AwayScore > Match.HomeScore && this.AwayScore > this.HomeScore && (Match.HomeScore == this.HomeScore || Match.AwayScore == this.AwayScore))
                    return FIRST_ROUND_PARTIAL_SCORE;
                else if (Match.HomeScore > Match.AwayScore && this.HomeScore > this.AwayScore)
                    return FIRST_ROUND_WINNER_SCORE;
                else if (Match.HomeScore == Match.AwayScore && this.HomeScore == this.AwayScore)
                    return FIRST_ROUND_WINNER_SCORE;
                else if (Match.AwayScore > Match.HomeScore && this.AwayScore > this.HomeScore)
                    return FIRST_ROUND_WINNER_SCORE;
            }
            return 0;
        }


        private const int FIRST_ROUND_EXACT_SCORE = 3;
        private const int FIRST_ROUND_PARTIAL_SCORE = 2;
        private const int FIRST_ROUND_WINNER_SCORE = 1;
    }

    public class MainModel
    {
        public IEnumerable<Match> Matches { get; set; }
        public IEnumerable<Result> MyResults { get; set; }
        public IEnumerable<Result> OtherResults { get; set; }
        public IEnumerable<Score> Ranking { get; set; }
        public IEnumerable<UserProfile> Users { get; set; }
        public bool FirstRoundEnabled { get; set; }

        public string CategoriesJS
        {
            get
            {
                var js = "";
                for (var i = 1; i <= DateCount; i++)
                {
                    js += "'" + i + "',";
                }

                return js.Length > 0 ? js.Remove(js.Length - 1) : js;
            }
        }

        public int DateCount
        {
            get { return Ranking.FirstOrDefault().ScoreByDate.Count; }
        }
    }

    public class Score
    {
        public UserProfile User { get; set; }
        public int Points { get; set; }
        public List<int> ScoreByDate { get; set; }
        public List<int> AccumScoreByDate { get; set; }

        public string ScoreByDateJS
        {
            get
            {
                var js = "";
                foreach (var s in ScoreByDate)
                {
                    js += s + ",";
                }
                return js.Length > 0 ? js.Remove(js.Length - 1) : js;
            }
        }

        public string AccumScoreByDateJS
        {
            get
            {
                var js = "";
                foreach (var s in AccumScoreByDate)
                {
                    js += s + ",";
                }
                return js.Length > 0 ? js.Remove(js.Length - 1) : js;
            }
        }
    }
}