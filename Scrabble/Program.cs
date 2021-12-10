using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Scrabble
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*Console.WindowHeight = 44;
            Console.WindowWidth = 165;*/
            //Sac_Jetons sac = new Sac_Jetons("Jetons.txt");
            //Console.WriteLine(sac.ToString());
            Dictionnaire[] leDico = new Dictionnaire[13];
            for(int i=0;i<13;i++)
            {
                leDico[i] = new Dictionnaire("Francais.txt", i + 2);
                //Console.WriteLine(leDico[i].ToString());
            }
            //Joueur leJoueur = new Joueur("Joueurs.txt");
            //Plateau lePlateau = new Plateau("TestPlateau.txt", leDico, leJoueur);
            Sac_Jetons leSac = new Sac_Jetons("Jetons.txt");
            //Jeu leJeu = new Jeu("Francais.txt", lePlateau, leSac);

            Random aleatoire = new Random();
            Console.WriteLine("Voulez-vous reprendre une partie déjà commencée ? (O/N) (Pensez à bien finir un tour complet avant de quitter où certains joueurs se feront sauter leur tour !)");
            bool recommencerBool = false;
            string recommencer;
            int nombrejoueur=0;
            do
            {

                recommencer = Console.ReadLine().ToUpper();
                switch (recommencer)
                {
                    case "O":
                    case "OUI":
                        Console.WriteLine("Combien de joueurs y avait-il dans la partie ?");
                        string nombreJoueursRecommencer = Console.ReadLine();
                        do
                        {
                            if (int.TryParse(nombreJoueursRecommencer, out nombrejoueur))
                            {
                                if (nombrejoueur != 2 && nombrejoueur != 3 && nombrejoueur != 4)
                                {
                                    Console.WriteLine("Vous ne pouvez pas avoir autant de joueurs dans la partie\nCombien de joueurs y avait-il dans la partie ?");
                                    nombreJoueursRecommencer = Console.ReadLine();
                                }
                            }
                            else
                            {
                                nombrejoueur = 0;
                                Console.WriteLine("Ce n'est pas un nombre\nCombien de joueurs y avait-il dans la partie ?");
                                nombreJoueursRecommencer = Console.ReadLine();
                            }
                        } while (nombrejoueur != 2 && nombrejoueur != 3 && nombrejoueur != 4);
                        break;
                    case "N":
                    case "NON":
                        //Plateau plateauVide = new Plateau("TestPlateau.txt", leDico, null);
                        recommencerBool = true;
                        break;
                    default:
                        Console.WriteLine("Veuillez écrire 'O' ou 'N' pour répondre à la question");
                        break;
                }
            } while (recommencer != "O" && recommencer != "N" && recommencer != "OUI" && recommencer != "NON");

            if (recommencerBool == true)
            {
                Console.WriteLine("A combien voulez-vous jouer ? (le nombre de joueurs maximum est de 4)");
                string réponse = Console.ReadLine();
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
                            leSac.Sac.Remove(jetonRetiré2);
                            Sac_Jetons.NbJetons--;
                        }
                        joueur12joueurs.WriteFile("Joueur1.txt");
                        joueur22joueurs.WriteFile("Joueur2.txt");
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
                            leSac.Sac.Remove(jetonRetiré3);
                            Sac_Jetons.NbJetons--;
                        }
                        joueur13joueurs.WriteFile("Joueur1.txt");
                        joueur23joueurs.WriteFile("Joueur2.txt");
                        joueur33joueurs.WriteFile("Joueur3.txt");
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
                            leSac.Sac.Remove(jetonRetiré4);
                            Sac_Jetons.NbJetons--;
                        }
                        joueur14joueurs.WriteFile("Joueur1.txt");
                        joueur24joueurs.WriteFile("Joueur2.txt");
                        joueur34joueurs.WriteFile("Joueur3.txt");
                        joueur44joueurs.WriteFile("Joueur4.txt");
                        break;
                }
            }
            List<Joueur> listeJoueursSauvegarde = new List<Joueur>();
            for (int i=0; i<nombrejoueur; i++)
            {
                if (i == 0) listeJoueursSauvegarde.Add(new Joueur("Joueur1.txt"));
                if (i == 1) listeJoueursSauvegarde.Add(new Joueur("Joueur2.txt"));
                if (i == 2) listeJoueursSauvegarde.Add(new Joueur("Joueur3.txt"));
                if (i == 3) listeJoueursSauvegarde.Add(new Joueur("Joueur4.txt"));
            }
            bool vérifJetonsSac = false;
            Plateau lePlateau;
            if (recommencerBool == false)
            {
                lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[0]);
                leSac.ReadFile("NombreJetonsSac.txt");
            }
            else
            {
                lePlateau = new Plateau("TestPlateau.txt", leDico, listeJoueursSauvegarde[0]);
                lePlateau.WriteFile("NouveauPlateau.txt");
            }

            Jeu leJeu = new Jeu("Francais.txt", lePlateau, leSac);
            leJeu.Jouer(1, listeJoueursSauvegarde);
            listeJoueursSauvegarde[0].WriteFile("Joueur1.txt");
            leSac.WriteFile("NombreJetonsSac.txt");

            lePlateau.WriteFile("NouveauPlateau.txt");
            lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[1]);
            leJeu = new Jeu("Francais.txt", lePlateau, leSac);
            leJeu.Jouer(2, listeJoueursSauvegarde);
            listeJoueursSauvegarde[1].WriteFile("Joueur2.txt");
            leSac.WriteFile("NombreJetonsSac.txt");

            if (nombrejoueur==3)
            {
                lePlateau.WriteFile("NouveauPlateau.txt");
                lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[2]);
                leJeu = new Jeu("Francais.txt", lePlateau, leSac);
                leJeu.Jouer(3, listeJoueursSauvegarde);
                listeJoueursSauvegarde[2].WriteFile("Joueur3.txt");
                leSac.WriteFile("NombreJetonsSac.txt");
            }
            if (nombrejoueur==4)
            {
                lePlateau.WriteFile("NouveauPlateau.txt");
                lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[2]);
                leJeu = new Jeu("Francais.txt", lePlateau, leSac);
                leJeu.Jouer(3, listeJoueursSauvegarde);
                listeJoueursSauvegarde[2].WriteFile("Joueur3.txt");
                leSac.WriteFile("NombreJetonsSac.txt");
                lePlateau.WriteFile("NouveauPlateau.txt");
                lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[3]);
                leJeu = new Jeu("Francais.txt", lePlateau, leSac);
                leJeu.Jouer(4, listeJoueursSauvegarde);
                listeJoueursSauvegarde[3].WriteFile("Joueur4.txt");
                leSac.WriteFile("NombreJetonsSac.txt");
            }

            while (vérifJetonsSac == false)
            {
                for (int i = 1; i <= nombrejoueur && vérifJetonsSac == false; i++)
                {
                    //lePlateau = new Plateau(lePlateau.Matrice, leDico, listeJoueurs[i-1]);
                    //Jeu leJeu = new Jeu("Francais.txt", lePlateau, leSac);
                    leSac.WriteFile("NombreJetonsSac.txt");
                    lePlateau.WriteFile("NouveauPlateau.txt");
                    lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[i - 1]);
                    leJeu = new Jeu("Francais.txt", lePlateau, leSac);
                    leJeu.Jouer(i, listeJoueursSauvegarde);

                    if (i == 1) listeJoueursSauvegarde[i - 1].WriteFile("Joueur1.txt");
                    if (i == 2) listeJoueursSauvegarde[i - 1].WriteFile("Joueur2.txt");
                    if (i == 3) listeJoueursSauvegarde[i - 1].WriteFile("Joueur3.txt");
                    if (i == 4) listeJoueursSauvegarde[i - 1].WriteFile("Joueur4.txt");

                    int compteurMainJoueurVide = 0;
                    if (leSac.Sac.Count <= 0)
                    {
                        for (int j = 0; j < listeJoueursSauvegarde.Count; j++)
                        {
                            if (listeJoueursSauvegarde[j].ListeJetons_lettre.Count == 0) compteurMainJoueurVide++;
                        }
                        if (compteurMainJoueurVide != 0) vérifJetonsSac = true;
                    }
                    if (leSac.Sac.Count < 7 && Jeu.CompteurPasseTour >= nombrejoueur * 3) vérifJetonsSac = true;
                }
            }
            Joueur stock;
            for (int i = 0; i < nombrejoueur - 1; i++)
            {
                for (int j = i+1; j<nombrejoueur; j++)
                {
                    if(listeJoueursSauvegarde[i].Score<listeJoueursSauvegarde[j].Score)
                    {
                        stock = listeJoueursSauvegarde[i];
                        listeJoueursSauvegarde[i] = listeJoueursSauvegarde[j];
                        listeJoueursSauvegarde[j] = stock;
                    }
                }
            }
            Joueur Vainqueur = listeJoueursSauvegarde[0];
            Joueur Deuxieme = listeJoueursSauvegarde[1];
            Joueur Troisieme = listeJoueursSauvegarde[2];
            Joueur Quatrieme = listeJoueursSauvegarde[3];
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("La partie est terminée \nVoici les résultats de cette partie : \n");
            if (nombrejoueur == 2)
            {
                Console.Write("Le gagnant de cette partie, qui a réussi à écraser toute concurrence est ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(Vainqueur.Nom);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" avec un score monstrueux de ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(Vainqueur.Score);
                Console.ForegroundColor = ConsoleColor.White; 
                Console.Write(" points.\n");

                Console.Write("Malheureusement, il faut toujours un perdant et aujourd'hui c'est ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(Deuxieme.Nom);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" avec un score décevant de ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(Deuxieme.Score);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" points.\n");
            }
            if (nombrejoueur == 3)
            {
                Console.Write("Le gagnant de cette partie, qui a réussi à écraser toute concurrence est ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(Vainqueur.Nom);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" avec un score monstrueux de ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(Vainqueur.Score);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" points.\n");

                Console.Write("Le deuxième de cette partie, et qui l'a mérité est ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(Deuxieme.Nom);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" avec tout de même un score de ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(Deuxieme.Score);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" points.\n");

                Console.Write("Malheureusement, il faut toujours un perdant et aujourd'hui c'est ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(Troisieme.Nom);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" avec un score décevant de ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(Troisieme.Score);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" points.\n");
            }
            if (nombrejoueur == 4)
            {
                Console.Write("Le gagnant de cette partie, qui a réussi à écraser toute concurrence est ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(Vainqueur.Nom);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" avec un score monstrueux de ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(Vainqueur.Score);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" points.\n");

                Console.Write("Le deuxième de cette partie, et qui l'a mérité est ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(Deuxieme.Nom);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" avec tout de même un score de ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(Deuxieme.Score);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" points.\n");

                Console.Write("Le troisième de cette partie, se trouvant donc sur la dernière marche du podium, c'est ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(Troisieme.Nom);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" avec un score qui laisse à désirer de ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(Troisieme.Score);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" points.\n");

                Console.Write("Malheureusement, il faut toujours un perdant et aujourd'hui c'est ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Quatrieme.Nom);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" avec un score décevant de ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(Quatrieme.Score);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" points.\n");
            }
        }
    }
}
