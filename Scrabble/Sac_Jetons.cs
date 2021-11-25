using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    class Sac_Jetons
    {
        private List<Jeton> sac;

        public Sac_Jetons(string fichier)
        {
            StreamReader sr = new StreamReader(fichier);
            string mot = sr.ReadLine();
            string[] ligne;
            Jeton jeton = null;
            while (mot!=null)
            {
                ligne = mot.Split(';');
                jeton = new Jeton(ligne[0], Convert.ToInt32(ligne[1]), Convert.ToInt32(ligne[2]));
                this.sac.Add(jeton);
                mot = sr.ReadLine();
            }
        }
    }
}
