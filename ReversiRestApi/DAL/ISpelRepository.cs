using ReversiRestApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.DAL
{
    public interface ISpelRepository
    {
        List<Spel> GetSpellen();
        Spel GetSpel(string spelToken);
        Spel GetSpelBySpeler1(string speler1Token);
        void AddSpel(Spel spel);
        void UpdateSpel(Spel spel);
        void VerwijderSpel(Spel spel);

    }
}
