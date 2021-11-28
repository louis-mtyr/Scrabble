using System;
using System.Diagnostics;
using System.Threading;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Random aleatoire = new Random();
            Console.WriteLine("Voici le Scrabble");
            Console.WriteLine("A combien voulez-vous jouer ? (le nombre de joueurs maximum est de 4)");
            int nombrejoueur = Convert.ToInt32(Console.ReadLine());
            while (nombrejoueur > 4 || nombrejoueur <= 1)
            {
                if(nombrejoueur > 4 || nombrejoueur <= 1)
                {
                    Console.WriteLine("Nombre de joueurs entré invalide, réésayez");
                }
                Console.WriteLine("A combien voulez-vous jouer ? (le nombre de joueurs maximum est de 4)");// peut etre l'enleve pas forcement nécessaire
                nombrejoueur = Convert.ToInt32(Console.ReadLine());
            }
            switch (nombrejoueur)
            {
                case 2:
                    Console.WriteLine("Quel est le nom du premier joueur ?");
                    string nom12joueurs = Console.ReadLine();
                    Joueur joueur12joueurs = new Joueur(nom12joueurs);
                    Console.WriteLine("Quel est le nom du deuxième joueur ?");
                    string nom22joueurs = Console.ReadLine();
                    Joueur joueur22joueurs = new Joueur(nom22joueurs);
                    while (joueur12joueurs.ListeJetons.Count <= 7&& joueur22joueurs.ListeJetons.Count <= 7)
                    {
                        Sac_Jetons.Retire_Jeton(aleatoire);
                    }
                    break;
                case 3:
                    Console.WriteLine("Quel est le nom du premier joueur ?");
                    string nom13joueurs = Console.ReadLine();
                    Joueur joueur13joueurs = new Joueur(nom13joueurs);
                    Console.WriteLine("Quel est le nom du deuxième joueur ?");
                    string nom23joueurs = Console.ReadLine();
                    Joueur joueur23joueurs = new Joueur(nom23joueurs);
                    Console.WriteLine("Quel est le nom du troisième joueur ?");
                    string nom33joueurs = Console.ReadLine();
                    Joueur joueur33joueurs = new Joueur(nom33joueurs);
                    break;
                case 4:
                    Console.WriteLine("Quel est le nom du premier joueur ?");
                    string nom14joueurs = Console.ReadLine();
                    Joueur joueur14joueurs = new Joueur(nom14joueurs);
                    Console.WriteLine("Quel est le nom du deuxième joueur ?");
                    string nom24joueurs = Console.ReadLine();
                    Joueur joueur24joueurs = new Joueur(nom24joueurs);
                    Console.WriteLine("Quel est le nom du troisième joueur ?");
                    string nom34joueurs = Console.ReadLine();
                    Joueur joueur34joueurs = new Joueur(nom34joueurs);
                    Console.WriteLine("Quel est le nom du quatrième joueur ?");
                    string nom44joueurs = Console.ReadLine();
                    Joueur joueur44joueurs = new Joueur(nom44joueurs);
                    break;
            }
            Console.WriteLine("Nous allons donc tout d'abord procéder à la phase de tirage");
            while ()
            Joueur joueur1 = new Joueur("Joueurs.txt");
            Dictionnaire dico = new Dictionnaire(null, 0, "turc");
            Plateau monplateau = new Plateau("InstancePlateau.txt",dico, joueur1);
            Console.WriteLine(monplateau.ToString());

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
