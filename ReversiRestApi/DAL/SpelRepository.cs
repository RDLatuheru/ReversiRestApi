using ReversiRestApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.DAL
{
    public class SpelRepository : ISpelRepository
    {
        public List<Spel> Spellen { get; set; }
        public static int Token { get; set; }

        public SpelRepository()
        {
            Spel spel1 = new();
            Spel spel2 = new();
            Spel spel3 = new();

            spel1.Speler1Token = "abcdef";
            spel1.Omschrijving = "Potje snel reveri, dus niet lang nadenken";
            spel1.Token = "token";

            spel2.Speler1Token = "ghijkl";
            spel2.Speler2Token = "mnopqr";
            spel2.Omschrijving = "Ik zoek een gevorderde tegenspeler!";
            spel2.Token = "token2";

            spel3.Speler1Token = "stuvwx";
            spel3.Omschrijving = "Na dit spel wil ik er nog een paar spelen tegen zelfde tegenstander";

            Spellen = new List<Spel> { spel1, spel2, spel3 };
        }

        //Get all games
        public List<Spel> GetSpellen()
        {
            return Spellen;
        }

        //Get game by Token
        public Spel GetSpel(string spelToken)
        {
            return Spellen.Find(x => x.Token == spelToken);
        }

        //Get game by Speler1Token
        public Spel GetSpelBySpeler1(string speler1Token)
        {
            return Spellen.Find(x => x.Speler1Token == speler1Token);
        }

        //Add new game
        public void AddSpel(Spel spel)
        {
            Spellen.Add(spel);
        }

        //Update game
        public void UpdateSpel(Spel spel)
        {
            Spel s = Spellen.Find(x => x == spel);
            s = spel;
        }

        //Delete game
        public void VerwijderSpel(Spel spel)
        {
            throw new NotImplementedException();
        }
    }
}
