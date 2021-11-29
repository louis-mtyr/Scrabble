using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    class Dictionnaire
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

        public override string ToString()
        {
            string rep = "Le Dictionnaire contient : ";
            rep=rep+this.ensembleMots.Count+" mots de " + this.longueur + " lettres en " + this.langue + ".";
            return rep;
        }

        public bool RechDichoRecursif(string mot, int i=0)
        {
            if (this.ensembleMots==null || i == this.ensembleMots.Count) return false;
            else if (this.ensembleMots[i] == mot) return true;
            else return RechDichoRecursif(mot, i + 1);
        }
    }
}
