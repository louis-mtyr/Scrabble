using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Scrabble
{
    public class Jeu
    {
        private Dictionnaire[] mondico;
        private Plateau monplateau;
        private Sac_Jetons monsac_jetons;

        public Jeu(string fichierDico, Plateau monplateau, string fichierSac_jetons)
        {
            this.mondico = new Dictionnaire[14];
            for (int i = 0; i < mondico.Length; i++) this.mondico[i] = new Dictionnaire(fichierDico, i+2);
            this.monplateau = monplateau;
            this.monsac_jetons = new Sac_Jetons(fichierSac_jetons);
        }

        public Jeu(string fichierDico, Plateau monplateau, Sac_Jetons monsac_jetons)
        {
            this.mondico = new Dictionnaire[14];
            for (int i = 0; i < mondico.Length; i++) this.mondico[i] = new Dictionnaire(fichierDico, i + 2);
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
        /// <summary>
        /// Simule le tour d'un joueur
        /// </summary>
        /// <param name="numéroJoueur">Numéro du joueur à qui c'est le tour</param>
        /// <param name="listeJoueurs">Liste des joueurs qui jouent au Scrabble</param>
        public void Jouer(int numéroJoueur, List<Joueur> listeJoueurs)
        {
            Random aleatoire = new Random();
            Console.Clear();
            Console.WriteLine(this.monplateau.ToString());

            Console.WriteLine("C'est au tour du joueur {0} :", numéroJoueur);
            Console.WriteLine(listeJoueurs[numéroJoueur - 1].ToString());
            Console.WriteLine("Quelle action voulez-vous faire ?\n 1. Piocher un nouveau jeton\n 2. Poser un mot à l'horizontale\n 3. Poser un mot à la verticale\n 4. Passer le tour");
            string réponseJoueur = Console.ReadLine();
            bool tourFini = false;
            string motAAjouter="";
            bool existence;
            string coordMotX;
            int nbrCoordMotX;
            string coordMotY;
            int nbrCoordMotY;
            while (tourFini == false)
            {
                switch (réponseJoueur)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine(this.monplateau.ToString());
                        Console.WriteLine(listeJoueurs[numéroJoueur - 1].ToString());
                        Console.WriteLine("Combien de jetons voulez-vous défausser ? (tapez 'quitter' pour annuler votre choix et passer votre tour)");
                        string défausse = Console.ReadLine();
                        int nbDéfausse;
                        do
                        {
                            if (int.TryParse(défausse, out nbDéfausse))
                            {
                                if (nbDéfausse <= 0 || nbDéfausse >= 8)
                                {
                                    Console.WriteLine("Vous ne pouvez pas défausser autant de jetons");
                                    Console.WriteLine("Combien de jetons voulez-vous défausser ?");
                                    défausse = Console.ReadLine();
                                }
                            }
                            else
                            {
                                if (défausse == "quitter")
                                {
                                    Console.WriteLine("Le joueur {0} a passé son tour.", numéroJoueur);
                                    break;
                                }
                                else
                                {
                                    nbDéfausse = 0;
                                    Console.WriteLine("Ce nombre n'existe pas");
                                    Console.WriteLine("Combien de jetons voulez-vous défausser ?");
                                    défausse = Console.ReadLine();
                                }
                            }
                        } while (nbDéfausse <= 0 || nbDéfausse >= 8);
                        for (int n = 0; n < nbDéfausse; n++)
                        {
                            Jeton jetonPioché = this.monsac_jetons.Retire_Jeton(aleatoire);
                            Console.Write("Le jeton pioché est : ");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(jetonPioché.Lettre);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nLequel de vos jetons souhaitez-vous remplacer par ce nouveau jeton ?");
                            string jetonARemplacer = Console.ReadLine().ToUpper();
                            bool appartient = false;
                            while (appartient == false)
                            {
                                for (int i = 0; i < listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Count-n; i++)
                                {
                                    if (jetonARemplacer == listeJoueurs[numéroJoueur - 1].ListeJetons_lettre[i]) appartient = true;
                                }
                                if (appartient == false)
                                {
                                    Console.WriteLine("Vous n'avez pas de jeton " + jetonARemplacer + " dans votre main. Veuillez choisir un de vos jetons à remplacer :");
                                    jetonARemplacer = Console.ReadLine().ToUpper();
                                }
                            }
                            listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Remove(jetonARemplacer);
                            this.monsac_jetons.Ajoute_Jeton(jetonARemplacer);
                            listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Add(jetonPioché.Lettre);
                        }
                        Console.WriteLine("Le tour du joueur {0} est terminé, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                        tourFini = true;
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.WriteLine("Quel est le mot que vous voulez ajouter sur le plateau ? (sans les accents) (tapez 'q' pour annuler votre choix et passer votre tour)");
                        motAAjouter = Console.ReadLine().ToUpper();
                        if (motAAjouter == "Q")
                        {
                            Console.WriteLine("Le joueur {0} a passé son tour, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                            tourFini = true;
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            while (motAAjouter.Length > 15 || motAAjouter.Length < 2)
                            {
                                Console.WriteLine("Ce mot ne rentre pas dans le plateau\nVeuillez choisir un mot valable :");
                                motAAjouter = Console.ReadLine().ToUpper();
                            }
                            existence = mondico[motAAjouter.Length - 2].RechDichoRecursif(motAAjouter);
                            while (existence == false)
                            {                                                           
                                Console.WriteLine("Ce mot n'existe pas dans le dictionnaire choisi\nVeuillez choisir un mot valable :");
                                motAAjouter = Console.ReadLine().ToUpper();
                                while ((motAAjouter.Length > 15 || motAAjouter.Length < 2) && motAAjouter != "Q")
                                {
                                    Console.WriteLine("Ce mot ne rentre pas dans le plateau\nVeuillez choisir un mot valable :");
                                    motAAjouter = Console.ReadLine().ToUpper();
                                }
                                if (motAAjouter == "Q")
                                {
                                    Console.WriteLine("Le joueur {0} a passé son tour, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                                    tourFini = true;
                                    Console.ReadKey();
                                    break;
                                }
                                else existence = mondico[motAAjouter.Length - 2].RechDichoRecursif(motAAjouter);
                            }
                            if (motAAjouter == "Q") break;
                            Console.WriteLine("Veuillez indiquer la ligne de la 1ere lettre de votre mot sur le plateau");
                            coordMotX = Console.ReadLine();
                            do
                            {
                                if (int.TryParse(coordMotX, out nbrCoordMotX))
                                {
                                    if (nbrCoordMotX < 1 || nbrCoordMotX > 15)
                                    {
                                        Console.WriteLine("Cette coordonnée est invalide\nVeuillez indiquer la ligne de la 1ere lettre de votre mot sur le plateau");
                                        coordMotX = Console.ReadLine();
                                    }
                                }
                                else
                                {
                                    nbrCoordMotX = 0;
                                    Console.WriteLine("Cette coordonnée est invalide\nVeuillez indiquer la ligne de la 1ere lettre de votre mot sur le plateau");
                                    coordMotX = Console.ReadLine();
                                }
                            } while (nbrCoordMotX < 1 || nbrCoordMotX > 15);
                            Console.WriteLine("Veuillez indiquer la colonne de la 1ere lettre de votre mot sur le plateau");
                            nbrCoordMotY = -1;
                            do
                            {
                                coordMotY = Console.ReadLine().ToUpper();
                                switch (coordMotY)
                                {
                                    case "A":
                                        nbrCoordMotY = 0;
                                        break;
                                    case "B":
                                        nbrCoordMotY = 1;
                                        break;
                                    case "C":
                                        nbrCoordMotY = 2;
                                        break;
                                    case "D":
                                        nbrCoordMotY = 3;
                                        break;
                                    case "E":
                                        nbrCoordMotY = 4;
                                        break;
                                    case "F":
                                        nbrCoordMotY = 5;
                                        break;
                                    case "G":
                                        nbrCoordMotY = 6;
                                        break;
                                    case "H":
                                        nbrCoordMotY = 7;
                                        break;
                                    case "I":
                                        nbrCoordMotY = 8;
                                        break;
                                    case "J":
                                        nbrCoordMotY = 9;
                                        break;
                                    case "K":
                                        nbrCoordMotY = 10;
                                        break;
                                    case "L":
                                        nbrCoordMotY = 11;
                                        break;
                                    case "M":
                                        nbrCoordMotY = 12;
                                        break;
                                    case "N":
                                        nbrCoordMotY = 13;
                                        break;
                                    case "O":
                                        nbrCoordMotY = 14;
                                        break;
                                    default:
                                        Console.WriteLine("Cette coordonnée est invalide\nVeuillez indiquer la colonne de la 1ere lettre de votre mot sur le plateau");
                                        break;
                                }
                            } while (coordMotY != "A" && coordMotY != "B" && coordMotY != "C" && coordMotY != "D" && coordMotY != "E" && coordMotY != "F" && coordMotY != "G" && coordMotY != "H" && coordMotY != "I" && coordMotY != "J" && coordMotY != "K" && coordMotY != "L" && coordMotY != "M" && coordMotY != "N" && coordMotY != "O");
                            if (monplateau.Test_Plateau(motAAjouter, nbrCoordMotX - 1, nbrCoordMotY, 'h') == false)
                            {
                                Console.WriteLine("Cette action est impossible, veuillez appuyer sur une touche pour recommencer");
                                Console.ReadKey();
                                Console.Clear();
                                Console.WriteLine(this.monplateau.ToString());
                                Console.WriteLine(listeJoueurs[numéroJoueur - 1].ToString());
                            }
                            else
                            {
                                int compteurLettre = 0;
                                Jeton nouveauJetonTire = null;
                                for (int j = nbrCoordMotY; j < nbrCoordMotY + motAAjouter.Length; j++)
                                {
                                    if (monplateau.Matrice[nbrCoordMotX - 1, j] != Convert.ToString(motAAjouter[compteurLettre]))
                                    {
                                        monplateau.Matrice[nbrCoordMotX - 1, j] = Convert.ToString(motAAjouter[compteurLettre]);
                                        if (monplateau.Matrice[nbrCoordMotX - 1, j] != "*")
                                        {
                                            listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Remove(Convert.ToString(motAAjouter[compteurLettre]));
                                            listeJoueurs[numéroJoueur - 1].Score += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[compteurLettre])).Score;
                                        }
                                        else
                                        {
                                            listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Remove("*");
                                        }
                                        nouveauJetonTire = monsac_jetons.Retire_Jeton(aleatoire);
                                        listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Add(nouveauJetonTire.Lettre);
                                        monsac_jetons.Sac.Remove(nouveauJetonTire);
                                    }
                                    compteurLettre++;
                                }
                                listeJoueurs[numéroJoueur - 1].MotsTrouves.Add(motAAjouter);
                                Console.WriteLine("Le tour du joueur {0} est terminé, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                                tourFini = true;
                                Console.ReadKey();
                            }
                            break;
                        }
                    case "3":
                        Console.WriteLine("Quel est le mot que vous voulez ajouter sur le plateau ? (sans les accents) (tapez 'q' pour annuler votre choix et passer votre tour)");
                        motAAjouter = Console.ReadLine().ToUpper();
                        if (motAAjouter == "Q")
                        {
                            Console.WriteLine("Le joueur {0} a passé son tour, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                            tourFini = true;
                            Console.ReadKey();
                        }
                        else
                        {
                            while (motAAjouter.Length > 15 || motAAjouter.Length < 2)
                            {
                                Console.WriteLine("Ce mot ne rentre pas dans le plateau\nVeuillez choisir un mot valable :");
                                motAAjouter = Console.ReadLine().ToUpper();
                            }
                            existence = mondico[motAAjouter.Length - 2].RechDichoRecursif(motAAjouter);
                            while (existence == false)
                            {
                                Console.WriteLine("Ce mot n'existe pas dans le dictionnaire choisi\nVeuillez choisir un mot valable :");
                                motAAjouter = Console.ReadLine().ToUpper();
                                while ((motAAjouter.Length > 15 || motAAjouter.Length < 2) && motAAjouter != "Q")
                                {
                                    Console.WriteLine("Ce mot ne rentre pas dans le plateau\nVeuillez choisir un mot valable :");
                                    motAAjouter = Console.ReadLine().ToUpper();
                                }
                                if (motAAjouter == "Q")
                                {
                                    Console.WriteLine("Le joueur {0} a passé son tour, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                                    tourFini = true;
                                    Console.ReadKey();
                                    break;
                                }
                                else existence = mondico[motAAjouter.Length - 2].RechDichoRecursif(motAAjouter);
                            }
                            if (motAAjouter == "Q") break;
                            Console.WriteLine("Veuillez indiquer la ligne de la 1ere lettre de votre mot sur le plateau");
                            coordMotX = Console.ReadLine();
                            do
                            {
                                if (int.TryParse(coordMotX, out nbrCoordMotX))
                                {
                                    if (nbrCoordMotX < 1 || nbrCoordMotX > 15)
                                    {
                                        Console.WriteLine("Cette coordonnée est invalide\nVeuillez indiquer la ligne de la 1ere lettre de votre mot sur le plateau");
                                        coordMotX = Console.ReadLine();
                                    }
                                }
                                else
                                {
                                    nbrCoordMotX = 0;
                                    Console.WriteLine("Cette coordonnée est invalide\nVeuillez indiquer la ligne de la 1ere lettre de votre mot sur le plateau");
                                    coordMotX = Console.ReadLine();
                                }
                            } while (nbrCoordMotX < 1 || nbrCoordMotX > 15);

                            Console.WriteLine("Veuillez indiquer la colonne de la 1ere lettre de votre mot sur le plateau");
                            nbrCoordMotY = -1;
                            do
                            {
                                coordMotY = Console.ReadLine().ToUpper();
                                switch (coordMotY)
                                {
                                    case "A":
                                        nbrCoordMotY = 0;
                                        break;
                                    case "B":
                                        nbrCoordMotY = 1;
                                        break;
                                    case "C":
                                        nbrCoordMotY = 2;
                                        break;
                                    case "D":
                                        nbrCoordMotY = 3;
                                        break;
                                    case "E":
                                        nbrCoordMotY = 4;
                                        break;
                                    case "F":
                                        nbrCoordMotY = 5;
                                        break;
                                    case "G":
                                        nbrCoordMotY = 6;
                                        break;
                                    case "H":
                                        nbrCoordMotY = 7;
                                        break;
                                    case "I":
                                        nbrCoordMotY = 8;
                                        break;
                                    case "J":
                                        nbrCoordMotY = 9;
                                        break;
                                    case "K":
                                        nbrCoordMotY = 10;
                                        break;
                                    case "L":
                                        nbrCoordMotY = 11;
                                        break;
                                    case "M":
                                        nbrCoordMotY = 12;
                                        break;
                                    case "N":
                                        nbrCoordMotY = 13;
                                        break;
                                    case "O":
                                        nbrCoordMotY = 14;
                                        break;
                                    default:
                                        Console.WriteLine("Cette coordonnée est invalide\nVeuillez indiquer la colonne de la 1ere lettre de votre mot sur le plateau");
                                        break;
                                }
                            } while (coordMotY != "A" && coordMotY != "B" && coordMotY != "C" && coordMotY != "D" && coordMotY != "E" && coordMotY != "F" && coordMotY != "G" && coordMotY != "H" && coordMotY != "I" && coordMotY != "J" && coordMotY != "K" && coordMotY != "L" && coordMotY != "M" && coordMotY != "N" && coordMotY != "O");
                            if (monplateau.Test_Plateau(motAAjouter, nbrCoordMotX - 1, nbrCoordMotY, 'v') == false)
                            {
                                Console.WriteLine("Cette action est impossible, veuillez appuyer sur une touche pour recommencer");
                                Console.ReadKey();
                                Console.Clear();
                                Console.WriteLine(this.monplateau.ToString());
                                Console.WriteLine(listeJoueurs[numéroJoueur - 1].ToString());
                            }
                            else
                            {
                                int compteurLettre = 0;
                                Jeton nouveauJetonTire = null;
                                for (int j = nbrCoordMotX - 1; j < nbrCoordMotX - 1 + motAAjouter.Length; j++)
                                {
                                    if (monplateau.Matrice[j, nbrCoordMotY] != "*" && monplateau.Matrice[j, nbrCoordMotY] != Convert.ToString(motAAjouter[compteurLettre]))
                                    {
                                        monplateau.Matrice[j, nbrCoordMotY] = Convert.ToString(motAAjouter[compteurLettre]);
                                        if (monplateau.Matrice[j, nbrCoordMotY] != "*")
                                        {
                                            listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Remove(Convert.ToString(motAAjouter[compteurLettre]));
                                            listeJoueurs[numéroJoueur - 1].Score += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[compteurLettre])).Score;
                                        }
                                        else
                                        {
                                            listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Remove("*");
                                        }
                                        nouveauJetonTire = monsac_jetons.Retire_Jeton(aleatoire);
                                        listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Add(nouveauJetonTire.Lettre);
                                        monsac_jetons.Sac.Remove(nouveauJetonTire);
                                    }
                                    compteurLettre++;
                                }
                                listeJoueurs[numéroJoueur - 1].MotsTrouves.Add(motAAjouter);
                                Console.WriteLine("Le tour du joueur {0} est terminé, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                                tourFini = true;
                                Console.ReadKey();
                            }
                        }
                        break;
                    case "4":
                        Console.WriteLine("Le joueur {0} a passé son tour, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                        tourFini = true;
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Action non reconnue, veuillez taper '1', '2' ou '3' pour réaliser une action.");
                        réponseJoueur = Console.ReadLine();
                        break;
                }
            }

            /*Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Thread.Sleep(5000);
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);*/
        }
    }
}
