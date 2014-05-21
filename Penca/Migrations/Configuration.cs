namespace Penca.Migrations
{
    using Penca.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Penca.Models.MatchContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Penca.Models.MatchContext context)
        {

            foreach (var m in context.Matches)
                context.Matches.Remove(m);

            context.Matches.Add(new Match { MatchId = 1, MatchDate = new DateTime(2014, 6, 12, 17, 0, 0), Group = "A", MatchStadium = "S�o Paulo", HomeTeam = "Brasil", AwayTeam = "Croacia" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 2, MatchDate = new DateTime(2014, 6, 13, 13, 0, 0), Group = "A", MatchStadium = "Natal", HomeTeam = "M�xico", AwayTeam = "Camer�n" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 3, MatchDate = new DateTime(2014, 6, 13, 16, 0, 0), Group = "B", MatchStadium = "Salvador", HomeTeam = "Espa�a", AwayTeam = "Pa�ses Bajos" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 4, MatchDate = new DateTime(2014, 6, 13, 19, 0, 0), Group = "B", MatchStadium = "Cuiab�", HomeTeam = "Chile", AwayTeam = "Australia" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 5, MatchDate = new DateTime(2014, 6, 14, 13, 0, 0), Group = "C", MatchStadium = "Belo Horizonte", HomeTeam = "Colombia", AwayTeam = "Grecia" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 6, MatchDate = new DateTime(2014, 6, 14, 16, 0, 0), Group = "D", MatchStadium = "Fortaleza", HomeTeam = "Uruguay", AwayTeam = "Costa Rica" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 7, MatchDate = new DateTime(2014, 6, 14, 19, 0, 0), Group = "D", MatchStadium = "Manaos", HomeTeam = "Inglaterra", AwayTeam = "Italia" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 8, MatchDate = new DateTime(2014, 6, 14, 22, 0, 0), Group = "C", MatchStadium = "Recife", HomeTeam = "Costa de Marfil", AwayTeam = "Jap�n" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 9, MatchDate = new DateTime(2014, 6, 15, 13, 0, 0), Group = "E", MatchStadium = "Brasilia", HomeTeam = "Suiza", AwayTeam = "Ecuador" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 10, MatchDate = new DateTime(2014, 6, 15, 16, 0, 0), Group = "E", MatchStadium = "Porto Alegre", HomeTeam = "Francia", AwayTeam = "Honduras" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 11, MatchDate = new DateTime(2014, 6, 15, 19, 0, 0), Group = "F", MatchStadium = "R�o de Janeiro", HomeTeam = "Argentina", AwayTeam = "Bosnia y Herzegovina" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 12, MatchDate = new DateTime(2014, 6, 16, 13, 0, 0), Group = "G", MatchStadium = "Salvador", HomeTeam = "Alemania", AwayTeam = "Portugal" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 13, MatchDate = new DateTime(2014, 6, 16, 16, 0, 0), Group = "F", MatchStadium = "Curitiba", HomeTeam = "Ir�n", AwayTeam = "Nigeria" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 14, MatchDate = new DateTime(2014, 6, 16, 19, 0, 0), Group = "G", MatchStadium = "Natal", HomeTeam = "Ghana", AwayTeam = "EEUU" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 15, MatchDate = new DateTime(2014, 6, 17, 13, 0, 0), Group = "H", MatchStadium = "Belo Horizonte", HomeTeam = "B�lgica", AwayTeam = "Argelia" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 16, MatchDate = new DateTime(2014, 6, 17, 16, 0, 0), Group = "A", MatchStadium = "Fortaleza", HomeTeam = "Brasil", AwayTeam = "M�xico" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 17, MatchDate = new DateTime(2014, 6, 17, 19, 0, 0), Group = "H", MatchStadium = "Cuiab�", HomeTeam = "Rusia", AwayTeam = "Rep�blica de Corea" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 18, MatchDate = new DateTime(2014, 6, 18, 13, 0, 0), Group = "B", MatchStadium = "Porto Alegre", HomeTeam = "Australia", AwayTeam = "Pa�ses Bajos" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 19, MatchDate = new DateTime(2014, 6, 18, 16, 0, 0), Group = "B", MatchStadium = "R�o de Janeiro", HomeTeam = "Espa�a", AwayTeam = "Chile" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 20, MatchDate = new DateTime(2014, 6, 18, 19, 0, 0), Group = "A", MatchStadium = "Manaos", HomeTeam = "Camer�n", AwayTeam = "Croacia" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 21, MatchDate = new DateTime(2014, 6, 19, 13, 0, 0), Group = "C", MatchStadium = "Brasilia", HomeTeam = "Colombia", AwayTeam = "Costa de Marfil" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 22, MatchDate = new DateTime(2014, 6, 19, 16, 0, 0), Group = "D", MatchStadium = "S�o Paulo", HomeTeam = "Uruguay", AwayTeam = "Inglaterra" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 23, MatchDate = new DateTime(2014, 6, 19, 19, 0, 0), Group = "C", MatchStadium = "Natal", HomeTeam = "Jap�n", AwayTeam = "Grecia" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 24, MatchDate = new DateTime(2014, 6, 20, 13, 0, 0), Group = "D", MatchStadium = "Recife", HomeTeam = "Italia", AwayTeam = "Costa Rica" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 25, MatchDate = new DateTime(2014, 6, 20, 16, 0, 0), Group = "E", MatchStadium = "Salvador", HomeTeam = "Suiza", AwayTeam = "Francia" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 26, MatchDate = new DateTime(2014, 6, 20, 19, 0, 0), Group = "E", MatchStadium = "Curitiba", HomeTeam = "Honduras", AwayTeam = "Ecuador" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 27, MatchDate = new DateTime(2014, 6, 21, 13, 0, 0), Group = "F", MatchStadium = "Belo Horizonte", HomeTeam = "Argentina", AwayTeam = "Ir�n" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 28, MatchDate = new DateTime(2014, 6, 21, 16, 0, 0), Group = "G", MatchStadium = "Fortaleza", HomeTeam = "Alemania", AwayTeam = "Ghana" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 29, MatchDate = new DateTime(2014, 6, 21, 19, 0, 0), Group = "F", MatchStadium = "Cuiab�", HomeTeam = "Nigeria", AwayTeam = "Bosnia y Herzegovina" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 30, MatchDate = new DateTime(2014, 6, 22, 13, 0, 0), Group = "H", MatchStadium = "R�o de Janeiro", HomeTeam = "B�lgica", AwayTeam = "Rusia" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 31, MatchDate = new DateTime(2014, 6, 22, 16, 0, 0), Group = "H", MatchStadium = "Porto Alegre", HomeTeam = "Rep�blica de Corea", AwayTeam = "Argelia" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 32, MatchDate = new DateTime(2014, 6, 22, 19, 0, 0), Group = "G", MatchStadium = "Manaos", HomeTeam = "EEUU", AwayTeam = "Portugal" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 33, MatchDate = new DateTime(2014, 6, 23, 13, 0, 0), Group = "B", MatchStadium = "S�o Paulo", HomeTeam = "Pa�ses Bajos", AwayTeam = "Chile" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 34, MatchDate = new DateTime(2014, 6, 23, 13, 0, 0), Group = "B", MatchStadium = "Curitiba", HomeTeam = "Australia", AwayTeam = "Espa�a" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 35, MatchDate = new DateTime(2014, 6, 23, 17, 0, 0), Group = "A", MatchStadium = "Brasilia", HomeTeam = "Camer�n", AwayTeam = "Brasil" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 36, MatchDate = new DateTime(2014, 6, 23, 17, 0, 0), Group = "A", MatchStadium = "Recife", HomeTeam = "Croacia", AwayTeam = "M�xico" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 37, MatchDate = new DateTime(2014, 6, 24, 13, 0, 0), Group = "D", MatchStadium = "Natal", HomeTeam = "Italia", AwayTeam = "Uruguay" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 38, MatchDate = new DateTime(2014, 6, 24, 13, 0, 0), Group = "D", MatchStadium = "Belo Horizonte", HomeTeam = "Costa Rica", AwayTeam = "Inglaterra" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 39, MatchDate = new DateTime(2014, 6, 24, 17, 0, 0), Group = "C", MatchStadium = "Cuiab�", HomeTeam = "Jap�n", AwayTeam = "Colombia" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 40, MatchDate = new DateTime(2014, 6, 24, 17, 0, 0), Group = "C", MatchStadium = "Fortaleza", HomeTeam = "Grecia", AwayTeam = "Costa de Marfil" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 41, MatchDate = new DateTime(2014, 6, 25, 13, 0, 0), Group = "F", MatchStadium = "Porto Alegre", HomeTeam = "Nigeria", AwayTeam = "Argentina" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 42, MatchDate = new DateTime(2014, 6, 25, 13, 0, 0), Group = "F", MatchStadium = "Salvador", HomeTeam = "Bosnia y Herzegovina", AwayTeam = "Ir�n" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 43, MatchDate = new DateTime(2014, 6, 25, 17, 0, 0), Group = "E", MatchStadium = "Manaos", HomeTeam = "Honduras", AwayTeam = "Suiza" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 44, MatchDate = new DateTime(2014, 6, 25, 17, 0, 0), Group = "E", MatchStadium = "R�o de Janeiro", HomeTeam = "Ecuador", AwayTeam = "Francia" , HomeScore = -1, AwayScore = -1 });

            context.Matches.Add(new Match { MatchId = 45, MatchDate = new DateTime(2014, 6, 26, 13, 0, 0), Group = "F", MatchStadium = "Brasilia", HomeTeam = "Portugal", AwayTeam = "Ghana" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 46, MatchDate = new DateTime(2014, 6, 26, 13, 0, 0), Group = "F", MatchStadium = "Recife", HomeTeam = "EEUU", AwayTeam = "Alemania" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 47, MatchDate = new DateTime(2014, 6, 26, 17, 0, 0), Group = "E", MatchStadium = "S�o Paulo", HomeTeam = "Rep�blica de Corea", AwayTeam = "B�lgica" , HomeScore = -1, AwayScore = -1 });
            context.Matches.Add(new Match { MatchId = 48, MatchDate = new DateTime(2014, 6, 26, 17, 0, 0), Group = "E", MatchStadium = "Curitiba", HomeTeam = "Argelia", AwayTeam = "Rusia" , HomeScore = -1, AwayScore = -1 });

            /*
                                    Argelia Camer�n Costa de Marfil Ghana Nigeria Australia Ir�n Jap�n Rep�blica de Corea Alemania B�lgica Bosnia y Herzegovina
                                    Croacia Espa�a Francia Grecia Inglaterra Italia Pa�ses Bajos Portugal Rusia Suiza Costa Rica EEUU Honduras M�xico
                                    Argentina Brasil Chile Colombia Ecuador Uruguay
             */
        }
    }
}
