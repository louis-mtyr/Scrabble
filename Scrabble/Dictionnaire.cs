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

        public Dictionnaire(string fichier)
        {
            StreamReader sr = new StreamReader(fichier);
            string mot = sr.ReadLine();
            string[] ligne;
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
            string rep = "Le Dictionnaire contient : \n";
            for(int i = 0; i <= 5; i++)
            {
                rep=rep+this.ensembleMots.Count+" mots de " + this.longueur + " lettres en " + this.langue;
            }
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
