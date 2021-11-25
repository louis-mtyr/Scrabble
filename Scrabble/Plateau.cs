using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    class Plateau
    {
        private string[,] matrice;

        public Plateau(string[,] matrice = null) //jcomprends pas ce qu'ils demandent
        {
            this.matrice = matrice;
        }

        public Plateau(string fichier)
        {
            StreamReader sr = new StreamReader(fichier);
            string mot = sr.ReadLine();
            string[] ligne;
            for(int i=0; i<15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    ligne = mot.Split(';');
                    this.matrice[i, j] = ligne[j];
                }
            }
        }
    }
}
