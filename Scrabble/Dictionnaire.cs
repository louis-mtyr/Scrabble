using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    public class Dictionnaire
    {
        private List<string> ensembleMots;
        private int longueur;
        private string langue;

        public Dictionnaire(List<string> ensembleMots,int longueur,string langue)
        {
            this.ensembleMots = ensembleMots;
            this.longueur = longueur;
            this.langue = langue;
        }

        public Dictionnaire(string fichier, int longueur)
        {
            if (longueur >= 2 && longueur <= 15)
            {
                this.ensembleMots = new List<string>();
                StreamReader sr = new StreamReader(fichier);
                string mot = sr.ReadLine();
                string[] ligne;
                while (mot != Convert.ToString(longueur))
                {
                    mot = sr.ReadLine();
                }
                mot = sr.ReadLine();
                ligne = mot.Split(' ');
                for (int i = 0; i < ligne.Length; i++) this.ensembleMots.Add(ligne[i]);
                this.longueur = longueur;
                this.langue = "francais";
            }
            else
            {
                this.ensembleMots = null;
                this.langue = "indéfinie";
                this.longueur = 0;
            }
        }

        public int Longueur
        {
            get { return this.longueur; }
        }
        public string Langue
        {
            get { return this.langue; }
        }
        public List<string> EnsembleMots
        {
            get { return this.ensembleMots; }
        }
        /// <summary>
        /// Retourne une chaîne de caractères qui décrit le dictionnaire des mots de la longueur mis en instance
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Le Dictionnaire contient : " + this.ensembleMots.Count + " mots de " + this.longueur + " lettres en " + this.langue + ".";
        }
        /// <summary>
        /// Teste que le mot appartient bien au dictionnaire
        /// </summary>
        /// <param name="mot">Mot utilisé pour le test</param>
        /// <param name="i">Index de chacun des mots de la liste</param>
        /// <returns></returns>
        public bool RechDichoRecursif(string mot, int i=0)
        {
            if (this.ensembleMots==null || i == this.ensembleMots.Count) return false;
            else if (this.ensembleMots[i] == mot) return true;
            else return RechDichoRecursif(mot, i + 1);
        }
    }
}
