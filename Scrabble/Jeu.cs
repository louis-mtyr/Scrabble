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
            //fichier sac jetons flemme
        }

        public Jeu(string fichierDico, Plateau monplateau, Sac_Jetons monsac_jetons)
        {
            //fichier dictionnaire kompranpa
            this.monplateau = monplateau;
            this.monsac_jetons = monsac_jetons;
        }
    }
}
