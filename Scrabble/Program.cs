using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.IO;

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

            Random aleatoire = new Random();
            Console.WriteLine("Voici le Scrabble");
            Console.WriteLine("A combien voulez-vous jouer ? (le nombre de joueurs maximum est de 4)");
            string réponse = Console.ReadLine();
            int nombrejoueur;
            do
            {
                Console.Clear();
                if (int.TryParse(réponse, out nombrejoueur))
                {
                    if (nombrejoueur > 4 || nombrejoueur <= 1)
                    {
                        Console.WriteLine("Nombre de joueurs entré invalide, réésayez");
                        Console.WriteLine("A combien voulez-vous jouer ? (le nombre de joueurs maximum est de 4)");
                        réponse = Console.ReadLine();
                    }
                }
                else
                {
                    nombrejoueur = 0;
                    Console.WriteLine("Nombre de joueurs entré invalide, réésayez");
                    Console.WriteLine("A combien voulez-vous jouer ? (le nombre de joueurs maximum est de 4)");
                    réponse = Console.ReadLine();
                }
            } while (nombrejoueur > 4 || nombrejoueur <= 1);
            List<Joueur> listeJoueurs = new List<Joueur>();
            switch (nombrejoueur)
            {
                case 2:
                    Console.WriteLine("Quel est le nom du premier joueur ?");
                    string nom12joueurs = Console.ReadLine();
                    Joueur joueur12joueurs = new Joueur(nom12joueurs, 0);
                    listeJoueurs.Add(joueur12joueurs);
                    Console.WriteLine("Quel est le nom du deuxième joueur ?");
                    string nom22joueurs = Console.ReadLine();
                    while (nom22joueurs == nom12joueurs)
                    {
                        Console.WriteLine("Ce nom a déjà été utilisé veuillez en prendre un autre :");
                        nom22joueurs = Console.ReadLine();
                    }
                    Joueur joueur22joueurs = new Joueur(nom22joueurs, 0);
                    listeJoueurs.Add(joueur22joueurs);
                    Jeton jetonRetiré2;
                    while (joueur12joueurs.ListeJetons_lettre.Count < 7 || joueur22joueurs.ListeJetons_lettre.Count < 7)
                    {
                        jetonRetiré2 = leSac.Retire_Jeton(aleatoire);
                        if (joueur12joueurs.ListeJetons_lettre.Count != 7) joueur12joueurs.ListeJetons_lettre.Add(jetonRetiré2.Lettre);
                        else joueur22joueurs.ListeJetons_lettre.Add(jetonRetiré2.Lettre);
                    }
                    break;
                case 3:
                    Console.WriteLine("Quel est le nom du premier joueur ?");
                    string nom13joueurs = Console.ReadLine();
                    Joueur joueur13joueurs = new Joueur(nom13joueurs, 0);
                    listeJoueurs.Add(joueur13joueurs);
                    Console.WriteLine("Quel est le nom du deuxième joueur ?");
                    string nom23joueurs = Console.ReadLine();
                    while (nom23joueurs == nom13joueurs)
                    {
                        Console.WriteLine("Ce nom a déjà été utilisé veuillez en prendre un autre :");
                        nom23joueurs = Console.ReadLine();
                    }
                    Joueur joueur23joueurs = new Joueur(nom23joueurs, 0);
                    listeJoueurs.Add(joueur23joueurs);
                    Console.WriteLine("Quel est le nom du troisième joueur ?");
                    string nom33joueurs = Console.ReadLine();
                    while (nom33joueurs == nom13joueurs || nom33joueurs == nom23joueurs)
                    {
                        Console.WriteLine("Ce nom a déjà été utilisé veuillez en prendre un autre :");
                        nom33joueurs = Console.ReadLine();
                    }
                    Joueur joueur33joueurs = new Joueur(nom33joueurs, 0);
                    listeJoueurs.Add(joueur33joueurs);
                    Jeton jetonRetiré3;
                    while (joueur13joueurs.ListeJetons_lettre.Count < 7 || joueur23joueurs.ListeJetons_lettre.Count < 7 || joueur33joueurs.ListeJetons_lettre.Count < 7)
                    {
                        jetonRetiré3 = leSac.Retire_Jeton(aleatoire);
                        if (joueur13joueurs.ListeJetons_lettre.Count != 7) joueur13joueurs.ListeJetons_lettre.Add(jetonRetiré3.Lettre);
                        else if (joueur23joueurs.ListeJetons_lettre.Count != 7) joueur23joueurs.ListeJetons_lettre.Add(jetonRetiré3.Lettre);
                        else joueur33joueurs.ListeJetons_lettre.Add(jetonRetiré3.Lettre);
                    }
                    break;
                case 4:
                    Console.WriteLine("Quel est le nom du premier joueur ?");
                    string nom14joueurs = Console.ReadLine();
                    Joueur joueur14joueurs = new Joueur(nom14joueurs, 0);
                    listeJoueurs.Add(joueur14joueurs);
                    Console.WriteLine("Quel est le nom du deuxième joueur ?");
                    string nom24joueurs = Console.ReadLine();
                    while (nom24joueurs == nom14joueurs)
                    {
                        Console.WriteLine("Ce nom a déjà été utilisé veuillez en prendre un autre :");
                        nom24joueurs = Console.ReadLine();
                    }
                    Joueur joueur24joueurs = new Joueur(nom24joueurs, 0);
                    listeJoueurs.Add(joueur24joueurs);
                    Console.WriteLine("Quel est le nom du troisième joueur ?");
                    string nom34joueurs = Console.ReadLine();
                    while (nom34joueurs == nom14joueurs || nom34joueurs == nom24joueurs)
                    {
                        Console.WriteLine("Ce nom a déjà été utilisé veuillez en prendre un autre :");
                        nom34joueurs = Console.ReadLine();
                    }
                    Joueur joueur34joueurs = new Joueur(nom34joueurs, 0);
                    listeJoueurs.Add(joueur34joueurs);
                    Console.WriteLine("Quel est le nom du quatrième joueur ?");
                    string nom44joueurs = Console.ReadLine();
                    while (nom44joueurs == nom14joueurs || nom44joueurs == nom24joueurs || nom44joueurs == nom34joueurs)
                    {
                        Console.WriteLine("Ce nom a déjà été utilisé veuillez en prendre un autre :");
                        nom44joueurs = Console.ReadLine();
                    }
                    Joueur joueur44joueurs = new Joueur(nom44joueurs, 0);
                    listeJoueurs.Add(joueur44joueurs);
                    Jeton jetonRetiré4;
                    while (joueur14joueurs.ListeJetons_lettre.Count < 7 || joueur24joueurs.ListeJetons_lettre.Count < 7 || joueur34joueurs.ListeJetons_lettre.Count < 7 || joueur44joueurs.ListeJetons_lettre.Count < 7)
                    {
                        jetonRetiré4 = leSac.Retire_Jeton(aleatoire);
                        if (joueur14joueurs.ListeJetons_lettre.Count != 7) joueur14joueurs.ListeJetons_lettre.Add(jetonRetiré4.Lettre);
                        else if (joueur24joueurs.ListeJetons_lettre.Count != 7) joueur24joueurs.ListeJetons_lettre.Add(jetonRetiré4.Lettre);
                        else if (joueur34joueurs.ListeJetons_lettre.Count != 7) joueur34joueurs.ListeJetons_lettre.Add(jetonRetiré4.Lettre);
                        else joueur44joueurs.ListeJetons_lettre.Add(jetonRetiré4.Lettre);
                    }
                    break;
            }

            bool vérifJetonsSac = false;
            int compteurVérifJetonsSac = 0;
            for (int i=0; i<leSac.Sac.Count; i++)
            {
                if (leSac.Sac[i].NombreJ == 0) compteurVérifJetonsSac++;
            }
            if (compteurVérifJetonsSac == leSac.Sac.Count) vérifJetonsSac = true;

            while (vérifJetonsSac == false)
            {
                for (int i = 1; i <= nombrejoueur; i++)
                {
                    leJeu.Jouer(i, listeJoueurs); //c'est pas la bonne boucle mais on a l'idée
                }
            }
        }
    }
}
