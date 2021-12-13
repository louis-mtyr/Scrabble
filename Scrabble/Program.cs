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
            Console.WindowWidth = 165;
            Nous avions essayés de modifier la taille de la console au lancement mais en fonction de l'ordinateur utilisé cela pouvait faire crasher le programme si la console était trop grande*/
            
            Dictionnaire[] leDico = new Dictionnaire[13];
            for(int i=0;i<13;i++)
            {
                leDico[i] = new Dictionnaire("Francais.txt", i + 2); //on initialise les données qui sont constantes à chaque début de partie, à savoir les dictionnaires, le nombre aléatoire, et le 1er sac de jetons
            }
            Sac_Jetons leSac = new Sac_Jetons("Jetons.txt");
            Random aleatoire = new Random();

            Console.WriteLine("Voulez-vous reprendre une partie déjà commencée ? (O/N) (Pensez à bien finir un tour complet avant de quitter où certains joueurs se feront sauter leur tour !)");
            bool recommencerBool = false; //permet de vérifier quelle option a été choisie plutôt que de tout mettre dans le même switch
            string recommencer; //réponse écrite de l'utilisateur
            int nombrejoueur=0; //explicite
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
                            if (int.TryParse(nombreJoueursRecommencer, out nombrejoueur)) //vérifie que la réponse écrite par l'utilisateur peut être transformée en int
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
                    if (int.TryParse(réponse, out nombrejoueur)) //vérifie que la réponse écrite par l'utilisateur peut être transformée en int
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
                switch (nombrejoueur) //crée chaque joueur un par un en fonction du nombre de joueurs choisi
                {
                    case 2:
                        Console.WriteLine("Quel est le nom du premier joueur ?"); //d'abord on demande leur nom
                        string nom12joueurs = Console.ReadLine();
                        Joueur joueur12joueurs = new Joueur(nom12joueurs, 0); //on crée la variable de classe Joueur avec le nom entré (on précise le score à 0 pour différencier du constructeur ne prenant qu'un string mais qui permet de lire un fichier)
                        listeJoueurs.Add(joueur12joueurs); //on ajoute le nouveau joueur dans notre première liste des joueurs présents (elle ne va être utilisée que si on commence une nouvelle partie)
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
                        while (joueur12joueurs.ListeJetons_lettre.Count < 7 || joueur22joueurs.ListeJetons_lettre.Count < 7) //on distribue automatiquement 7 jetons tirés aléatoirement à chaque joueur
                        {
                            jetonRetiré2 = leSac.Retire_Jeton(aleatoire); //tire un jeton aléatoirement du sac
                            if (joueur12joueurs.ListeJetons_lettre.Count != 7) joueur12joueurs.ListeJetons_lettre.Add(jetonRetiré2.Lettre); //ajoute ce jeton à la main du joueur 1 si le joueur 1 a moins de 7 jetons dans sa main
                            else joueur22joueurs.ListeJetons_lettre.Add(jetonRetiré2.Lettre); //ajoute ce jeton a la main du joueur 2 si le joueur 1 a déjà 7 jetons dans sa main
                            leSac.Sac.Remove(jetonRetiré2); //retire le jeton tiré du sac pour ne pas retomber dessus
                            leSac.WriteFileSac("NouveauSacJetons.txt"); //écrit la nouvelle valeur du sac dans le fichier NouveauSacJetons.txt pour la sauvegarder
                            Sac_Jetons.NbJetons--; //on réduit de 1 le nombre de jetons qu'il reste dans le sac
                            leSac = new Sac_Jetons("NouveauSacJetons.txt", Sac_Jetons.NbJetons); //on assimile à notre variable leSac la nouvelle valeur écrite dans le fichier NouveauSacJetons.txt
                        }
                        joueur12joueurs.WriteFile("Joueur1.txt"); //on sauvegarde les données du joueur 1 dans le fichier Joueur1.txt
                        joueur22joueurs.WriteFile("Joueur2.txt"); //on sauvegarde les données du joueur 2 dans le fichier Joueur2.txt
                        break;
                    case 3:
                        Console.WriteLine("Quel est le nom du premier joueur ?"); //idem mais pour 3 joueurs
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
                            leSac.WriteFileSac("NouveauSacJetons.txt");
                            Sac_Jetons.NbJetons--;
                            leSac = new Sac_Jetons("NouveauSacJetons.txt", Sac_Jetons.NbJetons);
                        }
                        joueur13joueurs.WriteFile("Joueur1.txt");
                        joueur23joueurs.WriteFile("Joueur2.txt");
                        joueur33joueurs.WriteFile("Joueur3.txt");
                        break;
                    case 4:
                        Console.WriteLine("Quel est le nom du premier joueur ?"); //idem mais pour 4 joueurs
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
                            leSac.WriteFileSac("NouveauSacJetons.txt");
                            Sac_Jetons.NbJetons--;
                            leSac = new Sac_Jetons("NouveauSacJetons.txt", Sac_Jetons.NbJetons);
                        }
                        joueur14joueurs.WriteFile("Joueur1.txt");
                        joueur24joueurs.WriteFile("Joueur2.txt");
                        joueur34joueurs.WriteFile("Joueur3.txt");
                        joueur44joueurs.WriteFile("Joueur4.txt");
                        break;
                }
            }
            List<Joueur> listeJoueursSauvegarde = new List<Joueur>(); //on crée la liste de joueurs avec les données sauvegardées pour pouvoir continuer notre partie sans soucis
            for (int i=0; i<nombrejoueur; i++)
            {
                if (i == 0) listeJoueursSauvegarde.Add(new Joueur("Joueur1.txt")); //on ajoute les valeurs sauvegardées dans les fichiers Joueurx.txt dans notre liste de joueurs sauvegardés
                if (i == 1) listeJoueursSauvegarde.Add(new Joueur("Joueur2.txt")); //n'ajoute que le nombre de joueurs qui a été précisé au début
                if (i == 2) listeJoueursSauvegarde.Add(new Joueur("Joueur3.txt"));
                if (i == 3) listeJoueursSauvegarde.Add(new Joueur("Joueur4.txt"));
            }
            bool vérifJetonsSac = false; //initialisition de condition de fin de jeu
            Plateau lePlateau; //création de notre plateau que nous allons utiliser
            if (recommencerBool == false) //si l'utilisateur a choisi de reprendre une ancienne partie
            {
                lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[0]); //on assimile notre plateau au plateau sauvegardé de la dernière partie contenu dans le fichier NouveauPlateau.txt
                leSac.ReadFileNbJetons("NombreJetonsSac.txt"); //sauvegarde et met à jour le nombre de jetons restants dans notre sac
                leSac = new Sac_Jetons("NouveauSacJetons.txt", Sac_Jetons.NbJetons); //assimile le sac au sac sauvegardé dans le fichier NouveauSacJetons.txt pour garder les mêmes jetons dedans qu'au moment où on a quitté la partie
            }
            else //si l'utilisateur a choisi de recommencer une nouvelle partie
            {
                lePlateau = new Plateau("TestPlateau.txt", leDico, listeJoueursSauvegarde[0]); //on assimile notre plateau à un plateau vide type contenu dans le fichier TestPlateau.txt
                lePlateau.WriteFile("NouveauPlateau.txt"); //on sauvegarde ce plateau dans le fichier NouveauPlateau.txt qui va être celui que nous allons utiliser pour sauvegarder les changements après
                leSac.WriteFileNbJetons("NombreJetonsSac.txt"); //sauvegarde le nombre de jetons restants dans le sac dans le fichier NombreJetonsSac.txt
            }

            //on fait d'abord un premier tour hors de la boucle pour correctement sauvegarder toutes les données nécessaires pour reprendre une partie

            Jeu leJeu = new Jeu("Francais.txt", lePlateau, leSac); //créé une classe jeu se basant sur le dictionnaire contenu dans le fichier Francais.txt, le plateau donné et le sac donné
            leJeu.Jouer(1, listeJoueursSauvegarde); //exécute un premier tour du jeu pour le joueur 1
            leSac.WriteFileSac("NouveauSacJetons.txt"); //sauvegarde du contenu du sac
            leSac = new Sac_Jetons("NouveauSacJetons.txt", Sac_Jetons.NbJetons); //nouvelle valeur du sac
            listeJoueursSauvegarde[0].WriteFile("Joueur1.txt"); //sauvegarde du joueur 1
            leSac.WriteFileNbJetons("NombreJetonsSac.txt"); //sauvegarde du nombre de jetons du sac restants
            lePlateau.WriteFile("NouveauPlateau.txt"); //sauvegarde du plateau
            lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[1]); //assimiliation nouvelle valeur au plateau + passage au joueur suivant
            leJeu = new Jeu("Francais.txt", lePlateau, leSac);
            leJeu.Jouer(2, listeJoueursSauvegarde); //idem pour le joueur 2
            leSac.WriteFileSac("NouveauSacJetons.txt");
            leSac = new Sac_Jetons("NouveauSacJetons.txt", Sac_Jetons.NbJetons);
            listeJoueursSauvegarde[1].WriteFile("Joueur2.txt");
            leSac.WriteFileNbJetons("NombreJetonsSac.txt");

            if (nombrejoueur==3) //idem pour le joueur 3 s'il y a 3 joueurs ou plus
            {
                lePlateau.WriteFile("NouveauPlateau.txt");
                lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[2]);
                leJeu = new Jeu("Francais.txt", lePlateau, leSac);
                leJeu.Jouer(3, listeJoueursSauvegarde);
                leSac.WriteFileSac("NouveauSacJetons.txt");
                leSac = new Sac_Jetons("NouveauSacJetons.txt", Sac_Jetons.NbJetons);
                listeJoueursSauvegarde[2].WriteFile("Joueur3.txt");
                leSac.WriteFileNbJetons("NombreJetonsSac.txt");
            }
            if (nombrejoueur==4) //idem pour le joueur 4 s'il y a 4 joueurs
            {
                lePlateau.WriteFile("NouveauPlateau.txt");
                lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[2]);
                leJeu = new Jeu("Francais.txt", lePlateau, leSac);
                leJeu.Jouer(3, listeJoueursSauvegarde);
                leSac.WriteFileSac("NouveauSacJetons.txt");
                leSac = new Sac_Jetons("NouveauSacJetons.txt", Sac_Jetons.NbJetons);
                listeJoueursSauvegarde[2].WriteFile("Joueur3.txt");
                leSac.WriteFileNbJetons("NombreJetonsSac.txt");
                lePlateau.WriteFile("NouveauPlateau.txt");
                lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[3]);
                leJeu = new Jeu("Francais.txt", lePlateau, leSac);
                leJeu.Jouer(4, listeJoueursSauvegarde);
                leSac.WriteFileSac("NouveauSacJetons.txt");
                leSac = new Sac_Jetons("NouveauSacJetons.txt", Sac_Jetons.NbJetons);
                listeJoueursSauvegarde[3].WriteFile("Joueur4.txt");
                leSac.WriteFileNbJetons("NombreJetonsSac.txt");
            }

            while (vérifJetonsSac == false) //répète tant que la condition de fin n'est pas vérifiée
            {
                for (int i = 1; i <= nombrejoueur && vérifJetonsSac == false; i++) //alterne entre tous les joueurs de la partie
                {
                    leSac.WriteFileNbJetons("NombreJetonsSac.txt"); //sauvegarde le nombre de jetons dans le sac
                    leSac.WriteFileSac("NouveauSacJetons.txt"); //sauvegarde le contenu du sac
                    leSac = new Sac_Jetons("NouveauSacJetons.txt", Sac_Jetons.NbJetons); //assimile sa nouvelle valeur au sac
                    lePlateau.WriteFile("NouveauPlateau.txt"); //sauvegarde du plateau
                    lePlateau = new Plateau("NouveauPlateau.txt", leDico, listeJoueursSauvegarde[i - 1]); //assimile sa nouvelle valeur au plateau + s'assure que c'est le bon joueur qui est observé
                    leJeu = new Jeu("Francais.txt", lePlateau, leSac); //assimile la classe jeu aux bons éléments à jour en paramètre
                    leJeu.Jouer(i, listeJoueursSauvegarde); //tour du i-ème joueur

                    if (i == 1) listeJoueursSauvegarde[i - 1].WriteFile("Joueur1.txt"); //sauvegarde de chaque joueur ayant joué
                    if (i == 2) listeJoueursSauvegarde[i - 1].WriteFile("Joueur2.txt");
                    if (i == 3) listeJoueursSauvegarde[i - 1].WriteFile("Joueur3.txt");
                    if (i == 4) listeJoueursSauvegarde[i - 1].WriteFile("Joueur4.txt");

                    int compteurMainJoueurVide = 0; //compte le nombre de joueurs n'ayant plus aucun jeton dans leur main
                    if (leSac.Sac.Count == 0) //si le sac n'a plus aucun jeton
                    {
                        for (int j = 0; j < listeJoueursSauvegarde.Count; j++) //vérifie pour chaque joueur s'il leur reste des jetons dans leur main
                        {
                            if (listeJoueursSauvegarde[j].ListeJetons_lettre[0] == "") compteurMainJoueurVide++; //si l'un d'eux n'en a plus aucun on augmente le compteurMainJoueurVide
                        }
                        if (compteurMainJoueurVide != 0) vérifJetonsSac = true; //si un seul joueur n'a plus de jeton dans sa main on met vérifJetonsSac à true, ce qui mettra fin à la boucle while
                    }
                    if (leSac.Sac.Count < 7 && Jeu.CompteurPasseTour >= nombrejoueur * 3) vérifJetonsSac = true; //s'il reste moins de 7 jetons dans le sac et que tous les joueurs piochent ou passent leur tour 3 fois d'affilée, la partie prend fin
                }
            }
            for (int i = 0; i < nombrejoueur; i++) //lorsque la partie est finie, on enlève à chaque joueur le score de chacune de leur lettre restantes dans leur main de leur score total
            {
                for (int j = 0; j < listeJoueursSauvegarde[i].ListeJetons_lettre.Count; j++)
                {
                    listeJoueursSauvegarde[i].Score -= leSac.TrouveJeton(listeJoueursSauvegarde[i].ListeJetons_lettre[j]).Score;
                }
            }
            Joueur stock;
            for (int i = 0; i < nombrejoueur - 1; i++)
            {
                for (int j = i+1; j<nombrejoueur; j++)
                {
                    if(listeJoueursSauvegarde[i].Score<listeJoueursSauvegarde[j].Score) //on trie la liste des joueurs pour qu'elle soit dans l'ordre décroissante par rapport à leur score
                    {
                        stock = listeJoueursSauvegarde[i];
                        listeJoueursSauvegarde[i] = listeJoueursSauvegarde[j];
                        listeJoueursSauvegarde[j] = stock;
                    }
                }
            }
            Joueur Vainqueur = listeJoueursSauvegarde[0]; //affichage de fin de partie avec les scores
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
