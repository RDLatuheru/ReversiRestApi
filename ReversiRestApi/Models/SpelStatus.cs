using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.Model
{
    public class SpelStatus
    {
        private Spel _spel;

        public string Bord => JsonConvert.SerializeObject(_spel.Bord);
        public string OverwegendeSpeler => _spel.OverwegendeKleur().ToString();


        public SpelStatus(Spel spel)
        {
            _spel = spel;
        }
    }
}
