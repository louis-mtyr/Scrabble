using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

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

        public void Jouer()
        {
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
                        Console.WriteLine("A combien voulez-vous jouer ? (le nombre de joueurs maximum est de 4)");// peut etre l'enleve pas forcement nécessaire
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
                        jetonRetiré2 = this.monsac_jetons.Retire_Jeton(aleatoire);
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
                        jetonRetiré3 = this.monsac_jetons.Retire_Jeton(aleatoire);
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
                        jetonRetiré4 = this.monsac_jetons.Retire_Jeton(aleatoire);
                        if (joueur14joueurs.ListeJetons_lettre.Count != 7) joueur14joueurs.ListeJetons_lettre.Add(jetonRetiré4.Lettre);
                        else if (joueur24joueurs.ListeJetons_lettre.Count != 7) joueur24joueurs.ListeJetons_lettre.Add(jetonRetiré4.Lettre);
                        else if (joueur34joueurs.ListeJetons_lettre.Count != 7) joueur34joueurs.ListeJetons_lettre.Add(jetonRetiré4.Lettre);
                        else joueur44joueurs.ListeJetons_lettre.Add(jetonRetiré4.Lettre);
                    }
                    break;
            }
            Console.Clear();
            Console.WriteLine("Nous allons donc tout d'abord procéder à la phase de tirage");
            Console.WriteLine(this.monplateau.ToString());
            for (int i=1; i<=nombrejoueur; i++)
            {
                Console.WriteLine("C'est au tour du joueur {0} :", i);
                Console.WriteLine(listeJoueurs[i - 1].ToString());
                Console.WriteLine("Quel mot voulez-vous écrire ?\n");
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Thread.Sleep(5000);
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
