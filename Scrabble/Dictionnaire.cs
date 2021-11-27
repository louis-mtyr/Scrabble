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

        }
        public override string ToString()
        {
            string rep = "Le Dictionnaire contient : \n";
            for(int i = 0; i <= 5; i++)
            {
                rep=rep+this.ensembleMots.Count+" mots de " + this.longueur + " en " + this.langue;
            }
            return rep;
        }
        public bool RechDichoRecursif(string mot)
        {

        }
    }
}
