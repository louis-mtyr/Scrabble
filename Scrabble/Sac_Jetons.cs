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
        public Jeton Retire_Jeton(Random r)
        {
            int aleatoire = r.Next(0,27);
            Jeton jeton_tiré =this.sac[aleatoire];
            return jeton_tiré;
        }
        public override string ToString()
        {
            return "Lettre : " + this.sac[].Lettre + " ,Score : " + this.sac[].Score + " ,Lettre " + this.sac[1].Lettre + " restantes : " + this.sac[].NombreJ;
        }
    }
}
