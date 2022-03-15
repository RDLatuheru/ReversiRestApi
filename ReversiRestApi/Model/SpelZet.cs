using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.Model
{
    public class SpelZet
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string SpelerToken { get; set; }
        public string SpelToken { get; set; }
        public bool HasPassed { get; set; }
    }
}
