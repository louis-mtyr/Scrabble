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
        public string Lettre
        {
            get { return this.lettre; }
        }
        public int Score
        {
            get { return this.score; }
        }
        public int NombreJ
        {
            get { return this.nombreJ; }
        }
        public void Retire_un_nombre()
        {
            if (this.nombreJ > 0) nombreJ--;
            else Console.WriteLine("Il n'y a plus de jetons dans le sac");
        }
        public override string ToString()
        {
            return "Lettre : " + this.lettre + " ,Score : " + this.score + " ,Lettre " + this.lettre + " restantes : " + this.nombreJ;
        }
    }
}
