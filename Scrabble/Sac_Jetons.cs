using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    public class Sac_Jetons
    {
        private List<Jeton> sac;

        public Sac_Jetons(string fichier)
        {
            this.sac = new List<Jeton>();
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
        public List<Jeton> Sac
        {
            get { return this.sac; }
            set { this.sac = value; }
        }
        /// <summary>
        /// Permet de tirer au hasard un jeton parmi tous les jetons possibles
        /// </summary>
        /// <param name="r">Fonction random</param>
        /// <returns></returns>
        public Jeton Retire_Jeton(Random r)
        {
            Jeton jeton_tiré = null;
            if (this.sac != null && this.sac.Count != 0)
            {
                do
                {
                    int aleatoire = r.Next(0, this.sac.Count);
                    jeton_tiré = this.sac[aleatoire];
                } while (jeton_tiré.NombreJ < 1);
                jeton_tiré.NombreJ--;
            }
            return jeton_tiré;
        }
        /// <summary>
        /// Ajoute au sac le jeton défaussé
        /// </summary>
        /// <param name="lettreJeton">La lettre défaussé par le joueur</param>
        public void Ajoute_Jeton(string lettreJeton)
        {
            switch(lettreJeton.ToUpper())
            {
                case "A":
                    this.sac[0].NombreJ++;
                    break;
                case "B":
                    this.sac[1].NombreJ++;
                    break;
                case "C":
                    this.sac[2].NombreJ++;
                    break;
                case "D":
                    this.sac[3].NombreJ++;
                    break;
                case "E":
                    this.sac[4].NombreJ++;
                    break;
                case "F":
                    this.sac[5].NombreJ++;
                    break;
                case "G":
                    this.sac[6].NombreJ++;
                    break;
                case "H":
                    this.sac[7].NombreJ++;
                    break;
                case "I":
                    this.sac[8].NombreJ++;
                    break;
                case "J":
                    this.sac[9].NombreJ++;
                    break;
                case "K":
                    this.sac[10].NombreJ++;
                    break;
                case "L":
                    this.sac[11].NombreJ++;
                    break;
                case "M":
                    this.sac[12].NombreJ++;
                    break;
                case "N":
                    this.sac[13].NombreJ++;
                    break;
                case "O":
                    this.sac[14].NombreJ++;
                    break;
                case "P":
                    this.sac[15].NombreJ++;
                    break;
                case "Q":
                    this.sac[16].NombreJ++;
                    break;
                case "R":
                    this.sac[17].NombreJ++;
                    break;
                case "S":
                    this.sac[18].NombreJ++;
                    break;
                case "T":
                    this.sac[19].NombreJ++;
                    break;
                case "U":
                    this.sac[20].NombreJ++;
                    break;
                case "V":
                    this.sac[21].NombreJ++;
                    break;
                case "W":
                    this.sac[22].NombreJ++;
                    break;
                case "X":
                    this.sac[23].NombreJ++;
                    break;
                case "Y":
                    this.sac[24].NombreJ++;
                    break;
                case "Z":
                    this.sac[25].NombreJ++;
                    break;
                case "*":
                    this.sac[26].NombreJ++;
                    break;
                default:
                    break;
            }
        }

        public Jeton TrouveJeton(string lettreJeton)
        {
            Jeton jetonCherche = null;
            switch (lettreJeton.ToUpper())
            {
                case "A":
                    jetonCherche = this.sac[0];
                    break;
                case "B":
                    jetonCherche = this.sac[1];
                    break;
                case "C":
                    jetonCherche = this.sac[2];
                    break;
                case "D":
                    jetonCherche = this.sac[3];
                    break;
                case "E":
                    jetonCherche = this.sac[4];
                    break;
                case "F":
                    jetonCherche = this.sac[5];
                    break;
                case "G":
                    jetonCherche = this.sac[6];
                    break;
                case "H":
                    jetonCherche = this.sac[7];
                    break;
                case "I":
                    jetonCherche = this.sac[8];
                    break;
                case "J":
                    jetonCherche = this.sac[9];
                    break;
                case "K":
                    jetonCherche = this.sac[10];
                    break;
                case "L":
                    jetonCherche = this.sac[11];
                    break;
                case "M":
                    jetonCherche = this.sac[12];
                    break;
                case "N":
                    jetonCherche = this.sac[13];
                    break;
                case "O":
                    jetonCherche = this.sac[14];
                    break;
                case "P":
                    jetonCherche = this.sac[15];
                    break;
                case "Q":
                    jetonCherche = this.sac[16];
                    break;
                case "R":
                    jetonCherche = this.sac[17];
                    break;
                case "S":
                    jetonCherche = this.sac[18];
                    break;
                case "T":
                    jetonCherche = this.sac[19];
                    break;
                case "U":
                    jetonCherche = this.sac[20];
                    break;
                case "V":
                    jetonCherche = this.sac[21];
                    break;
                case "W":
                    jetonCherche = this.sac[22];
                    break;
                case "X":
                    jetonCherche = this.sac[23];
                    break;
                case "Y":
                    jetonCherche = this.sac[24];
                    break;
                case "Z":
                    jetonCherche = this.sac[25];
                    break;
                case "*":
                    jetonCherche = this.sac[26];
                    break;
                default:
                    break;
            }
            return jetonCherche;
        }

        /// <summary>
        /// Retourne une chaîne de caractères qui décrit un jeton
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string rep = "Il reste dans le sac :";
            for (int i=0; i<sac.Count; i++)
            {
                rep += this.sac[i].NombreJ + "jetons de la lettre " + this.sac[i].Lettre + "\n";
            }
            return rep;
        }
    }
}
