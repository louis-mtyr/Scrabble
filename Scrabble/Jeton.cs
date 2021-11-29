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
            set { this.nombreJ = value; }
        }
        /// <summary>
        /// Simule le fait qu’un joueur ait tiré au hasard une lettre, on soustrait une unité à nombreJ
        /// </summary>
        public void Retire_un_nombre()
        {
            if (this.nombreJ > 0) this.nombreJ--;
            else Console.WriteLine("Il n'y a plus de jetons dans le sac");
        }
        /// <summary>
        /// Retourne une chaîne de caractères qui décrit un jeton
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Lettre : " + this.lettre + "\nScore : " + this.score + "\nLettres " + this.lettre + " restantes : " + this.nombreJ;
        }
    }
}
