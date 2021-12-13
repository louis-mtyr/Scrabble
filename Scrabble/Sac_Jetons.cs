using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    public class Sac_Jetons
    {
        private List<Jeton> sac;
        private static int nbJetons=0;

        public Sac_Jetons(string fichier)
        {
            this.sac = new List<Jeton>();
            StreamReader sr = new StreamReader(fichier);
            string mot = sr.ReadLine();
            string[] ligne;
            Jeton jeton = null;
            while (mot!=null) //La boucle permet de remplir notre sac de jetons avec les caractéristiques de jetons mis en paramètre dans le fichier
            {
                ligne = mot.Split(';'); 
                jeton = new Jeton(ligne[0], Convert.ToInt32(ligne[1]));
                for (int i = 0; i < Convert.ToInt32(ligne[2]); i++)
                {
                    this.sac.Add(jeton);
                    nbJetons++;
                }
                mot = sr.ReadLine();
            }
        }

        public Sac_Jetons(string fichier, int nbrJetons) // permet de créer un nouveau sac de jetons en se basant sur un fichier contenant les valeurs sauvegardées créé par nos soins
        {
            this.sac = new List<Jeton>();
            StreamReader sr = new StreamReader(fichier);
            string mot = sr.ReadLine();
            if (mot != null)
            { 
                string[] ligne = mot.Split(';');
                Jeton jeton = null;
                for (int i = 0; i < ligne.Length; i++)
                {
                    switch (ligne[i])
                    {
                        case "A":
                            jeton = new Jeton("A", 1);
                            break;
                        case "B":
                            jeton = new Jeton("B", 3);
                            break;
                        case "C":
                            jeton = new Jeton("C", 3);
                            break;
                        case "D":
                            jeton = new Jeton("D", 2);
                            break;
                        case "E":
                            jeton = new Jeton("E", 1);
                            break;
                        case "F":
                            jeton = new Jeton("F", 4);
                            break;
                        case "G":
                            jeton = new Jeton("G", 2);
                            break;
                        case "H":
                            jeton = new Jeton("H", 4);
                            break;
                        case "I":
                            jeton = new Jeton("I", 1);
                            break;
                        case "J":
                            jeton = new Jeton("J", 8);
                            break;
                        case "K":
                            jeton = new Jeton("K", 10);
                            break;
                        case "L":
                            jeton = new Jeton("L", 1);
                            break;
                        case "M":
                            jeton = new Jeton("M", 2);
                            break;
                        case "N":
                            jeton = new Jeton("N", 1);
                            break;
                        case "O":
                            jeton = new Jeton("O", 1);
                            break;
                        case "P":
                            jeton = new Jeton("P", 3);
                            break;
                        case "Q":
                            jeton = new Jeton("Q", 8);
                            break;
                        case "R":
                            jeton = new Jeton("R", 1);
                            break;
                        case "S":
                            jeton = new Jeton("S", 1);
                            break;
                        case "T":
                            jeton = new Jeton("T", 1);
                            break;
                        case "U":
                            jeton = new Jeton("U", 1);
                            break;
                        case "V":
                            jeton = new Jeton("V", 4);
                            break;
                        case "W":
                            jeton = new Jeton("W", 10);
                            break;
                        case "X":
                            jeton = new Jeton("X", 10);
                            break;
                        case "Y":
                            jeton = new Jeton("Y", 10);
                            break;
                        case "Z":
                            jeton = new Jeton("Z", 10);
                            break;
                        case "*":
                            jeton = new Jeton("*", 0);
                            break;
                        default:
                            break;
                    }
                    this.sac.Add(jeton);
                }
            }
            sr.Close();
        }

        public Sac_Jetons(List<Jeton> sac)
        {
            this.sac = sac;
        }

        public List<Jeton> Sac
        {
            get { return this.sac; }
            set { this.sac = value; }
        }

        public static int NbJetons
        {
            get { return nbJetons; }
            set { nbJetons = value; }
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
                int aleatoire = r.Next(0, this.sac.Count);
                jeton_tiré = this.sac[aleatoire];
            }
            return jeton_tiré;
        }
        /// <summary>
        /// Ajoute au sac le jeton défaussé
        /// </summary>
        /// <param name="lettreJeton">La lettre défaussé par le joueur</param>
        public void Ajoute_Jeton(string lettreJeton)
        {
            Jeton nouveauJeton = null;
            switch(lettreJeton.ToUpper())
            {
                case "A":
                    nouveauJeton = new Jeton("A", 1);
                    this.sac.Add(nouveauJeton);
                    break;
                case "B":
                    nouveauJeton = new Jeton("B", 3);
                    this.sac.Add(nouveauJeton);
                    break;
                case "C":
                    nouveauJeton = new Jeton("C", 3);
                    this.sac.Add(nouveauJeton);
                    break;
                case "D":
                    nouveauJeton = new Jeton("D", 2);
                    this.sac.Add(nouveauJeton);
                    break;
                case "E":
                    nouveauJeton = new Jeton("E", 1);
                    this.sac.Add(nouveauJeton);
                    break;
                case "F":
                    nouveauJeton = new Jeton("F", 4);
                    this.sac.Add(nouveauJeton);
                    break;
                case "G":
                    nouveauJeton = new Jeton("G", 2);
                    this.sac.Add(nouveauJeton);
                    break;
                case "H":
                    nouveauJeton = new Jeton("H", 4);
                    this.sac.Add(nouveauJeton);
                    break;
                case "I":
                    nouveauJeton = new Jeton("I", 1);
                    this.sac.Add(nouveauJeton);
                    break;
                case "J":
                    nouveauJeton = new Jeton("J", 8);
                    this.sac.Add(nouveauJeton);
                    break;
                case "K":
                    nouveauJeton = new Jeton("K", 10);
                    this.sac.Add(nouveauJeton);
                    break;
                case "L":
                    nouveauJeton = new Jeton("L", 1);
                    this.sac.Add(nouveauJeton);
                    break;
                case "M":
                    nouveauJeton = new Jeton("M", 2);
                    this.sac.Add(nouveauJeton);
                    break;
                case "N":
                    nouveauJeton = new Jeton("N", 1);
                    this.sac.Add(nouveauJeton);
                    break;
                case "O":
                    nouveauJeton = new Jeton("O", 1);
                    this.sac.Add(nouveauJeton);
                    break;
                case "P":
                    nouveauJeton = new Jeton("P", 3);
                    this.sac.Add(nouveauJeton);
                    break;
                case "Q":
                    nouveauJeton = new Jeton("Q", 8);
                    this.sac.Add(nouveauJeton);
                    break;
                case "R":
                    nouveauJeton = new Jeton("R", 1);
                    this.sac.Add(nouveauJeton);
                    break;
                case "S":
                    nouveauJeton = new Jeton("S", 1);
                    this.sac.Add(nouveauJeton);
                    break;
                case "T":
                    nouveauJeton = new Jeton("T", 1);
                    this.sac.Add(nouveauJeton);
                    break;
                case "U":
                    nouveauJeton = new Jeton("U", 1);
                    this.sac.Add(nouveauJeton);
                    break;
                case "V":
                    nouveauJeton = new Jeton("V", 4);
                    this.sac.Add(nouveauJeton);
                    break;
                case "W":
                    nouveauJeton = new Jeton("W", 10);
                    this.sac.Add(nouveauJeton);
                    break;
                case "X":
                    nouveauJeton = new Jeton("X", 10);
                    this.sac.Add(nouveauJeton);
                    break;
                case "Y":
                    nouveauJeton = new Jeton("Y", 10);
                    this.sac.Add(nouveauJeton);
                    break;
                case "Z":
                    nouveauJeton = new Jeton("Z", 10);
                    this.sac.Add(nouveauJeton);
                    break;
                case "*":
                    nouveauJeton = new Jeton("*", 0);
                    this.sac.Add(nouveauJeton);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Retourne le jeton cherché avec toutes ces caractéristiques à partir d'un string correspondant à sa lettre
        /// </summary>
        /// <param name="lettreJeton">Nom du jeton cherché</param>
        /// <returns></returns>
        public Jeton TrouveJeton(string lettreJeton)
        {
            Jeton jetonCherche = null;
            switch (lettreJeton.ToUpper())
            {
                case "A":
                    jetonCherche = new Jeton("A", 1);
                    break;
                case "B":
                    jetonCherche = new Jeton("B", 3);
                    break;
                case "C":
                    jetonCherche = new Jeton("C", 3);
                    break;
                case "D":
                    jetonCherche = new Jeton("D", 2);
                    break;
                case "E":
                    jetonCherche = new Jeton("E", 1);
                    break;
                case "F":
                    jetonCherche = new Jeton("F", 4);
                    break;
                case "G":
                    jetonCherche = new Jeton("G", 2);
                    break;
                case "H":
                    jetonCherche = new Jeton("H", 4);
                    break;
                case "I":
                    jetonCherche = new Jeton("I", 1);
                    break;
                case "J":
                    jetonCherche = new Jeton("J", 8);
                    break;
                case "K":
                    jetonCherche = new Jeton("K", 10);
                    break;
                case "L":
                    jetonCherche = new Jeton("L", 1);
                    break;
                case "M":
                    jetonCherche = new Jeton("M", 2);
                    break;
                case "N":
                    jetonCherche = new Jeton("N", 1);
                    break;
                case "O":
                    jetonCherche = new Jeton("O", 1);
                    break;
                case "P":
                    jetonCherche = new Jeton("P", 3);
                    break;
                case "Q":
                    jetonCherche = new Jeton("Q", 8);
                    break;
                case "R":
                    jetonCherche = new Jeton("R", 1);
                    break;
                case "S":
                    jetonCherche = new Jeton("S", 1);
                    break;
                case "T":
                    jetonCherche = new Jeton("T", 1);
                    break;
                case "U":
                    jetonCherche = new Jeton("U", 1);
                    break;
                case "V":
                    jetonCherche = new Jeton("V", 4);
                    break;
                case "W":
                    jetonCherche = new Jeton("W", 10);
                    break;
                case "X":
                    jetonCherche = new Jeton("X", 10);
                    break;
                case "Y":
                    jetonCherche = new Jeton("Y", 10);
                    break;
                case "Z":
                    jetonCherche = new Jeton("Z", 10);
                    break;
                case "*":
                    jetonCherche = new Jeton("*", 0);
                    break;
                default:
                    jetonCherche = new Jeton("null", 0);
                    break;
            }
            return jetonCherche;
        }

        /// <summary>
        /// Retourne une chaîne de caractères qui décrit un sac de jetons
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string rep = "Il reste dans le sac : \n";
            int[] compteurLettres = new int[27];
            string[] lettres = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "*" };
            int[] scoreLettres = { 1, 3, 3, 2, 1, 4, 2, 4, 1, 8, 10, 1, 2, 1, 1, 3, 8, 1, 1, 1, 1, 4, 10, 10, 10, 10, 0 };
            Jeton leJeton = null;
            for (int j = 0; j < 27; j++)
            {
                for (int i = 0; i < this.sac.Count; i++)
                {
                    if (this.sac[i].Lettre == lettres[j])
                    {
                        compteurLettres[j]++;
                    }
                }
                leJeton = new Jeton(lettres[j], scoreLettres[j]);
                rep += compteurLettres[j] + " jeton(s) de la lettre "+lettres[j]+" qui valent "+scoreLettres[j]+" point(s).\n";
            }
            return rep;
        }
        /// <summary>
        /// Lit la valeur entrée dans le fichier texte et assimile la valeur static nbJetons à celle lue
        /// </summary>
        /// <param name="filename">Nom du fichier texte lu</param>
        public void ReadFileNbJetons(string filename)
        {
            StreamReader fichier = new StreamReader(filename);
            string ligne = fichier.ReadLine();
            nbJetons = Convert.ToInt32(ligne);
            fichier.Close();
        }
        /// <summary>
        /// Sauvegarde la valeur static nbJetons en l'écrivant dans un fichier texte
        /// </summary>
        /// <param name="filename">Nom du fichier texte dans lequel la valeur va être écrite</param>
        public void WriteFileNbJetons(string filename)
        {
            StreamWriter fichier = new StreamWriter(filename);
            fichier.Write(nbJetons);
            fichier.Close();
        }
        /// <summary>
        /// Permet de sauvegarder tous les jetons restants dans le sac en les écrivant dans un fichier texte lisible par un constructeur spécifique
        /// </summary>
        /// <param name="filename">Nom du fichier texte dans lequel les valeurs vont être écrites</param>
        public void WriteFileSac(string filename)
        {
            StreamWriter fichier = new StreamWriter(filename);
            for (int i=0; i<sac.Count; i++)
            {
                if (i!=sac.Count-1) fichier.Write(sac[i].Lettre + ";");
                else fichier.Write(sac[i].Lettre);
            }
            fichier.Close();
        }
    }
}
