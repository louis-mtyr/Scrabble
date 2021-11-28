using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    class Jeu
    {
        private Dictionnaire[] mondico;
        private Plateau monplateau;
        private Sac_Jetons monsac_jetons;

        public Jeu(string fichierDico, Plateau monplateau, string fichierSac_jetons)
        {
            //fichier dictionnaire kompranpa
            this.monplateau = monplateau;
            this.monsac_jetons = new Sac_Jetons(fichierSac_jetons);
        }

        public Jeu(string fichierDico, Plateau monplateau, Sac_Jetons monsac_jetons)
        {
            //fichier dictionnaire kompranpa
            this.monplateau = monplateau;
            this.monsac_jetons = monsac_jetons;
        }
        public Dictionnaire[] Mondico
        {
            get { return this.mondico; }
        }
        public Plateau Monplateau
        {
            get { return this.monplateau; }
        }
        public Sac_Jetons Monsac_jetons
        {
            get { return this.monsac_jetons; }
        }
    }
}
