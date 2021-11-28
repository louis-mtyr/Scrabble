using System;
using System.Diagnostics;
using System.Threading;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionnaire leDico = new Dictionnaire(null, 0, "français");
            Joueur leJoueur = new Joueur("Joueurs.txt");
            Plateau lePlateau = new Plateau("TestPlateau.txt", leDico, leJoueur);
            Sac_Jetons leSac = new Sac_Jetons("Jetons.txt");
            Jeu leJeu = new Jeu("", lePlateau, leSac);
            leJeu.Jouer();
        }
    }
}
