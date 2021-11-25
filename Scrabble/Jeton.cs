using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    class Jeton
    {
        private string lettre;
        private int score;
        private int nombreJ;

        public Jeton(string lettre, int score, int nombreJ)
        {
            this.lettre = lettre;
            this.score = score;
            this.nombreJ = nombreJ;
        }
    }
}
