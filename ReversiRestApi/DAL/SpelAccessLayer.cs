using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReversiRestApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.DAL
{
    public class SpelAccessLayer : ISpelRepository
    {
        private SpelContext _spelContext;

        public SpelAccessLayer(SpelContext spelContext)
        {
            _spelContext = spelContext;
        }

        public List<Spel> Spellen { get; set; }


        //Get all games
        public List<Spel> GetSpellen() => _spelContext.Spel.ToList();

        //Get game by Token
        public Spel GetSpel(string spelToken) => _spelContext.Spel.FirstOrDefault(x => x.Token == spelToken);

        //Get game by Speler1Token
        public Spel GetSpelBySpeler1(string speler1Token) => _spelContext.Spel.FirstOrDefault(x => x.Speler1Token == speler1Token);

        //Add new game
        public void AddSpel(Spel spel)
        {
            _spelContext.Spel.Add(spel);
            _spelContext.SaveChanges();
        }
            

        //Update game
        public void UpdateSpel(Spel spel) => _spelContext.Spel.Update(spel);

        //Delete game
        public void VerwijderSpel(Spel spel) => _spelContext.Spel.Remove(spel);
    }
}
