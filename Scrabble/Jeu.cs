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
        private static int compteurPasseTour;

        public Jeu(string fichierDico, Plateau monplateau, string fichierSac_jetons)
        {
            this.mondico = new Dictionnaire[14];
            for (int i = 0; i < mondico.Length; i++) this.mondico[i] = new Dictionnaire(fichierDico, i+2);
            this.monplateau = monplateau;
            this.monsac_jetons = new Sac_Jetons(fichierSac_jetons);
            compteurPasseTour = 0;
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

        public static int CompteurPasseTour
        {
            get { return compteurPasseTour; }
        }

        /// <summary>
        /// Simule le tour d'un joueur
        /// </summary>
        /// <param name="numéroJoueur">Numéro du joueur à qui c'est le tour</param>
        /// <param name="listeJoueurs">Liste des joueurs qui jouent au Scrabble</param>
        public void Jouer(int numéroJoueur, List<Joueur> listeJoueurs)
        {
            Random aleatoire = new Random();
            DateTime oldTimer = DateTime.Now; //démarre le décompte du timer
            Console.Clear(); //efface la console pour afficher le plateau, les infos du joueur concerné et du sac, et ses actions disponibles
            Console.WriteLine(this.monplateau.ToString());
            Console.WriteLine("C'est au tour du joueur {0} :", numéroJoueur);
            Console.WriteLine(listeJoueurs[numéroJoueur - 1].ToString());
            Console.Write("Il reste ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(Sac_Jetons.NbJetons);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" jetons dans le sac");
            Console.WriteLine("Quelle action voulez-vous faire ?\n 1. Piocher un nouveau jeton\n 2. Poser un mot à l'horizontale\n 3. Poser un mot à la verticale\n 4. Passer le tour");
            string réponseJoueur = Console.ReadLine();
            bool tourFini = false;
            string motAAjouter="";
            bool existence;
            string coordMotX;
            int nbrCoordMotX;
            string coordMotY;
            int nbrCoordMotY;
            int scoreMot = 0;
            int multiplicateur = 1;
            int compteurJetonPose = 0;
            while (tourFini == false) //permet de vérifier si l'action est bien terminée pour sortir de la boucle
            {
                switch (réponseJoueur)
                {
                    case "1": //le joueur veut piocher un ou plusieurs jetons
                        compteurPasseTour++; //augmente le compteurPasseTour pour vérifier la condition de fin si le sac contient moins de 7 jetons
                        Console.Clear(); //affiche le plateau et les infos du joueur concerné
                        Console.WriteLine(this.monplateau.ToString());
                        Console.WriteLine(listeJoueurs[numéroJoueur - 1].ToString());
                        if (monsac_jetons.Sac.Count != 0) //vérifie qu'il reste des jetons à piocher dans le sac
                        {
                            Console.WriteLine("Combien de jetons voulez-vous défausser ? (tapez 'quitter' pour annuler votre choix et passer votre tour)");
                            string défausse = Console.ReadLine();
                            int nbDéfausse;
                            do
                            {
                                if (int.TryParse(défausse, out nbDéfausse)) //vérifie qu'on peut convertir la réponse en int
                                {
                                    if (nbDéfausse <= 0 || nbDéfausse >= 8 || nbDéfausse > monsac_jetons.Sac.Count) //vérifie qu'on ne puisse pas piocher plus de jetons que l'on doit en avoir dans sa main ou qu'il n'en reste dans le sac
                                    {
                                        Console.WriteLine("Vous ne pouvez pas défausser autant de jetons");
                                        Console.WriteLine("Combien de jetons voulez-vous défausser ?");
                                        défausse = Console.ReadLine();
                                    }
                                }
                                else
                                {
                                    if (défausse == "quitter") //permet d'annuler son choix et de passer son tour si l'on s'est trompé
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
                            } while (nbDéfausse <= 0 || nbDéfausse >= 8 || nbDéfausse > monsac_jetons.Sac.Count);

                            if (nbDéfausse != 7)
                            {
                                for (int n = 0; n < nbDéfausse; n++)
                                {
                                    Jeton jetonPioché = monsac_jetons.Retire_Jeton(aleatoire); //tire un jeton du sac aléatoirement et l'affiche
                                    Console.Write("Le jeton pioché est : ");
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.Write(jetonPioché.Lettre);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("\nLequel de vos jetons souhaitez-vous remplacer par ce nouveau jeton ?");
                                    string jetonARemplacer = Console.ReadLine().ToUpper();
                                    bool appartient = false;
                                    while (appartient == false) //demande au joueur quel jeton de sa main il veut défausser pour le remplacer par le jeton affiché
                                    {
                                        for (int i = 0; i < listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Count - n; i++)
                                        {
                                            if (jetonARemplacer == listeJoueurs[numéroJoueur - 1].ListeJetons_lettre[i]) appartient = true;
                                        }
                                        if (appartient == false) //vérifie que le jeton entré en réponse appartient bien à la main du joueur
                                        {
                                            Console.WriteLine("Vous n'avez pas de jeton " + jetonARemplacer + " dans votre main. Veuillez choisir un de vos jetons à remplacer :");
                                            jetonARemplacer = Console.ReadLine().ToUpper();
                                        }
                                    }
                                    listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Remove(jetonARemplacer); //remplace le jeton défaussé et le remet dans le sac
                                    monsac_jetons.Ajoute_Jeton(jetonARemplacer);
                                    listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Add(jetonPioché.Lettre); //retire le jeton tiré du sac et l'ajoute à la main du joueur
                                    monsac_jetons.Sac.Remove(jetonPioché);
                                }
                            }
                            else
                            {
                                for (int n=0; n<7; n++) //fais la même chose qu'au dessus mais change les 7 d'un coup pour aller plus vite
                                {
                                    Jeton jetonPioché = monsac_jetons.Retire_Jeton(aleatoire);
                                    string jetonARetirer = listeJoueurs[numéroJoueur - 1].ListeJetons_lettre[0];
                                    listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Remove(jetonARetirer);
                                    monsac_jetons.Ajoute_Jeton(jetonARetirer);
                                    listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Add(jetonPioché.Lettre);
                                    monsac_jetons.Sac.Remove(jetonPioché);
                                }
                                Console.WriteLine("Votre jeu a été complètement changé");
                            }
                        }
                        else //si le sac ne contient plus aucun jeton
                        {
                            Console.WriteLine("Il n'y a plus de jetons dans le sac, vous ne pouvez plus piocher !");
                        }
                        Console.WriteLine("Le tour du joueur {0} est terminé, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                        tourFini = true; //fin du tour
                        Console.ReadKey();
                        break;

                    case "2": //le joueur va poser un mot à l'horizontale
                        compteurPasseTour = 0; //on réinitialise le compteurPasseTour pour éviter de finir la partie si le sac contient moins de 7 jetons et que un ou plusieurs joueurs peuvent encore jouer
                        Console.WriteLine("Quel est le mot que vous voulez ajouter sur le plateau ? (sans les accents) (tapez 'q' pour annuler votre choix et passer votre tour)");
                        motAAjouter = Console.ReadLine().ToUpper();
                        if (motAAjouter == "Q") //permet d'annuler le tour avant d'entrer un premier mot si le joueur s'est trompé de commande
                        {
                            Console.WriteLine("Le joueur {0} a passé son tour, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                            tourFini = true; //met fin au tour
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            DateTime newTimer = DateTime.Now;
                            double Delta = newTimer.Subtract(oldTimer).TotalSeconds;
                            if (Delta >= 300) //vérifie que le joueur a répondu dans le temps imparti (5mns)
                            {
                                Console.WriteLine("Temps imparti écoulé, votre action n'est pas comptabilisée");
                                tourFini = true; //met fin au tour
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                while (motAAjouter.Length > 15 || motAAjouter.Length < 2) //vérifie que le mot respecte les tailles limites imposées
                                {
                                    if (motAAjouter == "Q") //permet d'annuler le tour après avoir entré un premier mot si le joueur s'est trompé
                                    {
                                        Console.WriteLine("Le joueur {0} a passé son tour, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                                        tourFini = true; //met fin au tour
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ce mot ne rentre pas dans le plateau\nVeuillez choisir un mot valable :");
                                        motAAjouter = Console.ReadLine().ToUpper();
                                    }
                                }
                                if (motAAjouter != "Q") existence = mondico[motAAjouter.Length - 2].RechDichoRecursif(motAAjouter); //vérifie que le mot écrit existe dans le dictionnaire de la taille du-dit mot
                                else existence = true; //si le joueur a entré "Q" pour annuler son tour on met existence à true pour sauter la prochaine boucle
                                while (existence == false) //si le mot n'appartient pas au dictionnaire
                                {
                                    Console.WriteLine("Ce mot n'existe pas dans le dictionnaire choisi\nVeuillez choisir un mot valable :");
                                    motAAjouter = Console.ReadLine().ToUpper();
                                    while ((motAAjouter.Length > 15 || motAAjouter.Length < 2) && motAAjouter != "Q") //on redemande un mot et on reteste son éligibilité
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
                                if (motAAjouter == "Q") break; //met fin au tour si le joueur a entré "Q"
                                Console.WriteLine("Veuillez indiquer la ligne de la 1ere lettre de votre mot sur le plateau");
                                coordMotX = Console.ReadLine();
                                do
                                {
                                    if (int.TryParse(coordMotX, out nbrCoordMotX)) //vérifie que la réponse peut être convertie en int
                                    {
                                        if (nbrCoordMotX < 1 || nbrCoordMotX > 15) //vérifie que le joueur entre une coordonnée qui rentre dans celles du plateau
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
                                    coordMotY = Console.ReadLine().ToUpper(); //convertie la lettre choisie au nombre correspondant de la matrice de notre plateau
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
                                if (monplateau.Test_Plateau(motAAjouter, nbrCoordMotX - 1, nbrCoordMotY, 'h') == false) //vérifie s'il est possible d'écrire le mot choisi sur le plateau aux coordonnées choisies
                                { //si c'est impossible on revient au début du switch avec réponse = 2, le joueur pourra donc à nouveau choisir un mot à écrire à l'horizontale, ou passer son tour
                                    Console.WriteLine("Cette action est impossible, veuillez appuyer sur une touche pour recommencer");
                                    Console.ReadKey();
                                    Console.Clear();
                                    Console.WriteLine(this.monplateau.ToString());
                                    Console.WriteLine(listeJoueurs[numéroJoueur - 1].ToString());
                                    Console.WriteLine("Vous allez placer un mot à l'horizontale");
                                }
                                else //s'il est possible d'écrire le mot choisi aux coordonnées choisies
                                {
                                    int compteurLettre = 0;
                                    int compteurJoker = 0;
                                    scoreMot = 0;
                                    multiplicateur = 1;
                                    string[] verifCasePremiereLettre = new string[motAAjouter.Length]; //regarde quelle est la case se trouvant à la i-ème lettre du mot choisi
                                    Jeton nouveauJetonTire = null;
                                    for (int i = 0; i < motAAjouter.Length; i++) //ajoute le score total du mot choisi aux coordonnées choisies
                                    {
                                        if (monplateau.Matrice[nbrCoordMotX - 1, nbrCoordMotY + i] == "_" || monplateau.Matrice[nbrCoordMotX - 1, nbrCoordMotY + i] == Convert.ToString(motAAjouter[i]))
                                        {
                                            scoreMot += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[i])).Score; //ajoute simplement le score de la lettre si elle se trouve sur une case vide ou si elle est déjà présente sur le plateau
                                            verifCasePremiereLettre[i] = "_";
                                        }
                                        if (monplateau.Matrice[nbrCoordMotX - 1, nbrCoordMotY + i] == "2")
                                        {
                                            scoreMot += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[i])).Score * 2; //ajoute le double du score de la lettre si elle se trouve sur une case lettre compte double (2 bleu)
                                            verifCasePremiereLettre[i] = "2";
                                        }
                                        if (monplateau.Matrice[nbrCoordMotX - 1, nbrCoordMotY + i] == "3")
                                        {
                                            scoreMot += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[i])).Score * 3; //ajoute le triple du score de la lettre si elle se trouve sur une case lettre compte triple (3 bleu)
                                            verifCasePremiereLettre[i] = "3";
                                        }
                                        if (monplateau.Matrice[nbrCoordMotX - 1, nbrCoordMotY + i] == "4")
                                        {
                                            scoreMot += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[i])).Score; //ajoute le score de la lettre et multiplie le multiplicateur par 2 si la lettre se trouve sur une case mot compte double (2 rouge)
                                            multiplicateur *= 2;
                                            verifCasePremiereLettre[i] = "4";
                                        }
                                        if (monplateau.Matrice[nbrCoordMotX - 1, nbrCoordMotY + i] == "5")
                                        {
                                            scoreMot += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[i])).Score; //ajoute le score de la lettre et multiplie le multiplicateur par 3 si la lettre se trouve sur une case mot compte triple (3 rouge)
                                            multiplicateur *= 3;
                                            verifCasePremiereLettre[i] = "5";
                                        }
                                    }
                                    for (int j = nbrCoordMotY; j < nbrCoordMotY + motAAjouter.Length; j++) //part de la case de départ jusqu'à la dernière case du plateau occupée par le mot choisi
                                    {
                                        compteurJoker = 0; //remet le compteurJoker à 0
                                        if (monplateau.Matrice[nbrCoordMotX - 1, j] != Convert.ToString(motAAjouter[compteurLettre])) //si la i-ème lettre du mot n'est pas déjà présente sur le plateau
                                        {
                                            for (int i = 0; i < listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Count; i++)
                                            {
                                                if (listeJoueurs[numéroJoueur - 1].ListeJetons_lettre[i] == Convert.ToString(motAAjouter[compteurLettre])) compteurJoker++; //on augmente le compteurJoker si le joueur possède au moins un jeton de la i-ème lettre du mot dans sa main
                                            }
                                            if (compteurJoker != 0) //si le compteurJoker est différent de 0 on retire le jeton de la i-ème lettre du mot de la main du joueur
                                            {
                                                listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Remove(Convert.ToString(motAAjouter[compteurLettre]));
                                            }
                                            else //si le compteurJoker est égal à 0, cela veut dire que le joueur possède un joker, donc on lui retire de sa main, et on retire les points apportés par la lettre remplacée par le joker du score total du mot
                                            {
                                                listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Remove("*");
                                                if (monplateau.Matrice[nbrCoordMotX - 1, j] == "_") scoreMot -= monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[compteurLettre])).Score;
                                                if (monplateau.Matrice[nbrCoordMotX - 1, j] == "2") scoreMot -= monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[compteurLettre])).Score * 2;
                                                if (monplateau.Matrice[nbrCoordMotX - 1, j] == "3") scoreMot -= monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[compteurLettre])).Score * 3;
                                            }
                                            if (monsac_jetons.Sac.Count > 0) nouveauJetonTire = monsac_jetons.Retire_Jeton(aleatoire); //tire un jeton aléatoirement du sac s'il n'est pas vide
                                            monplateau.Matrice[nbrCoordMotX - 1, j] = Convert.ToString(motAAjouter[compteurLettre]); //écrit la i-ème lettre du mot sur le plateau à la case concernée
                                            if (monsac_jetons.Sac.Count > 0) listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Add(nouveauJetonTire.Lettre); //si le sac n'est pas vide, on ajoute le jeton tiré à la main du joueur
                                            if (monsac_jetons.Sac.Count > 0) Sac_Jetons.NbJetons--; //si le sac n'est pas vide, on décrémente NbJetons de 1 pour mettre à jour le nombre de jetons restants dans le sac
                                            if (monsac_jetons.Sac.Count > 0) monsac_jetons.Sac.Remove(nouveauJetonTire); //si le sac n'est pas vide, on retire le jeton tiré du sac
                                            compteurJetonPose++; //augmente de 1 pour chaque jeton de la main du joueur utilisé en un tour 
                                        }
                                        compteurLettre++; //permet de passer de la i-ème lettre du mot à la (i+1)-ème lettre du mot
                                    }
                                    listeJoueurs[numéroJoueur - 1].Score += scoreMot * multiplicateur; //on ajoute la valeur totale du score du mot placé au score du joueur
                                    if (compteurJetonPose == 7) listeJoueurs[numéroJoueur - 1].Score += 50; //si le joueur a placé ses 7 jetons d'un coup en un tour il gagne un bonus de 50 points
                                    int incrementation = 0;
                                    int decrementation = 0;
                                    int compteurMotsTrouves = 0;
                                    string rep = "";
                                    scoreMot = 0;
                                    multiplicateur = 1;
                                    listeJoueurs[numéroJoueur - 1].MotsTrouves.Add(motAAjouter); //on ajoute le mot écrite à la liste des mots trouvés du joueur
                                    for (int i = 0; i < motAAjouter.Length; i++)
                                    {
                                        compteurMotsTrouves = 0;
                                        rep = "";
                                        scoreMot = 0;
                                        multiplicateur = 1;
                                        incrementation = 1;
                                        decrementation = -1;
                                        if (nbrCoordMotX - 1 + 1 >= 0 && nbrCoordMotX - 1 + 1 <= 14 && nbrCoordMotX - 1 - 1 >= 0 && nbrCoordMotX - 1 - 1 <= 14) //vérifie si au dessus et en dessous de chaque lettre posée sur le plateau il y a déjà une lettre d'un autre mot placée
                                        {
                                            if ((monplateau.Matrice[nbrCoordMotX - 1 + 1, nbrCoordMotY + i] != "_" && monplateau.Matrice[nbrCoordMotX - 1 + 1, nbrCoordMotY + i] != "2" && monplateau.Matrice[nbrCoordMotX - 1 + 1, nbrCoordMotY + i] != "3" && monplateau.Matrice[nbrCoordMotX - 1 + 1, nbrCoordMotY + i] != "4" && monplateau.Matrice[nbrCoordMotX - 1 + 1, nbrCoordMotY + i] != "5") || (monplateau.Matrice[nbrCoordMotX - 1 - 1, nbrCoordMotY + i] != "_" && monplateau.Matrice[nbrCoordMotX - 1 - 1, nbrCoordMotY + i] != "2" && monplateau.Matrice[nbrCoordMotX - 1 - 1, nbrCoordMotY + i] != "3" && monplateau.Matrice[nbrCoordMotX - 1 - 1, nbrCoordMotY + i] != "4" && monplateau.Matrice[nbrCoordMotX - 1 - 1, nbrCoordMotY + i] != "5"))
                                            {
                                                while (nbrCoordMotX - 1 + incrementation <= 14 && monplateau.Matrice[nbrCoordMotX - 1 + incrementation, nbrCoordMotY + i] != "_" && monplateau.Matrice[nbrCoordMotX - 1 + incrementation, nbrCoordMotY + i] != "2" && monplateau.Matrice[nbrCoordMotX - 1 + incrementation, nbrCoordMotY + i] != "3" && monplateau.Matrice[nbrCoordMotX - 1 + incrementation, nbrCoordMotY + i] != "4" && monplateau.Matrice[nbrCoordMotX - 1 + incrementation, nbrCoordMotY + i] != "5")
                                                {
                                                    incrementation++; //descend d'une ligne (et augmente incrementation) tant que la case du dessous est occupée par une lettre
                                                }
                                                while (nbrCoordMotX - 1 + decrementation >= 0 && monplateau.Matrice[nbrCoordMotX - 1 + decrementation, nbrCoordMotY + i] != "_" && monplateau.Matrice[nbrCoordMotX - 1 + decrementation, nbrCoordMotY + i] != "2" && monplateau.Matrice[nbrCoordMotX - 1 + decrementation, nbrCoordMotY + i] != "3" && monplateau.Matrice[nbrCoordMotX - 1 + decrementation, nbrCoordMotY + i] != "4" && monplateau.Matrice[nbrCoordMotX - 1 + decrementation, nbrCoordMotY + i] != "5")
                                                {
                                                    decrementation--; //monte d'une ligne (et diminue decrementation) tant que la case du dessus est occupée par une lettre
                                                }
                                                for (int n = decrementation + 1; n < incrementation; n++) //on calcule puis ajoute le score du nouveau mot formé au score total du joueur
                                                {
                                                    rep += monplateau.Matrice[nbrCoordMotX - 1 + n, nbrCoordMotY + i];
                                                    if (monplateau.Matrice[nbrCoordMotX - 1 + n, nbrCoordMotY + i] == "2" || (verifCasePremiereLettre[i] == "2" && n == 0)) scoreMot += monsac_jetons.TrouveJeton(monplateau.Matrice[nbrCoordMotX - 1 + n, nbrCoordMotY + i]).Score * 2;
                                                    else if (monplateau.Matrice[nbrCoordMotX - 1 + n, nbrCoordMotY + i] == "3" || (verifCasePremiereLettre[i] == "3" && n == 0)) scoreMot += monsac_jetons.TrouveJeton(monplateau.Matrice[nbrCoordMotX - 1 + n, nbrCoordMotY + i]).Score * 3;
                                                    else if (monplateau.Matrice[nbrCoordMotX - 1 + n, nbrCoordMotY + i] == "4" || (verifCasePremiereLettre[i] == "4" && n == 0))
                                                    {
                                                        scoreMot += monsac_jetons.TrouveJeton(monplateau.Matrice[nbrCoordMotX - 1 + n, nbrCoordMotY + i]).Score;
                                                        multiplicateur = 2;
                                                    }
                                                    else if (monplateau.Matrice[nbrCoordMotX - 1 + n, nbrCoordMotY + i] == "5" || (verifCasePremiereLettre[i] == "5" && n == 0))
                                                    {
                                                        scoreMot += monsac_jetons.TrouveJeton(monplateau.Matrice[nbrCoordMotX - 1 + n, nbrCoordMotY + i]).Score;
                                                        multiplicateur = 3;
                                                    }
                                                    else scoreMot += monsac_jetons.TrouveJeton(monplateau.Matrice[nbrCoordMotX - 1 + n, nbrCoordMotY + i]).Score;
                                                }

                                                for (int a = 0; a < listeJoueurs.Count; a++) //vérifie pour tous les joueurs de la partie si le nouveau mot formé étudié à déjà été trouvé par un autre joueur
                                                {
                                                    for (int b = 0; b < listeJoueurs[a].MotsTrouves.Count; b++) //permet d'éviter de comptabiliser le score d'un mot déjà placé à la verticale si l'on utilise une seule de ses lettres pour écrire un nouveau mot à l'horizontale
                                                    {
                                                        if (listeJoueurs[a].MotsTrouves[b] == rep) compteurMotsTrouves++; //si l'un des joueurs a déjà trouvé ce mot on augmente compteurMotsTrouves
                                                    }
                                                }
                                                if (compteurMotsTrouves == 0) //si aucun autre joueur n'a déjà trouvé ce mot (c'est-à-dire si le joueur a formé un nouveau mot sur le plateau en ajoutant le mot qu'il a écrit initialement) on l'ajoute à sa liste de mots trouvés et on ajoute son score au score total du joueur
                                                {
                                                    listeJoueurs[numéroJoueur - 1].MotsTrouves.Add(rep);
                                                    listeJoueurs[numéroJoueur - 1].Score += scoreMot * multiplicateur;
                                                }
                                            }
                                        }
                                    }
                                    Console.WriteLine("Le tour du joueur {0} est terminé, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                                    tourFini = true; //fin du tour
                                    Console.ReadKey();
                                }
                            }
                        }
                        break;
                    case "3": //le joueur va poser un mot à la verticale (même principe que pour l'horizontale mais en adaptant les valeurs des cases étudiées du plateau)
                        compteurPasseTour = 0;
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
                            DateTime newTimer = DateTime.Now;
                            double Delta = newTimer.Subtract(oldTimer).TotalSeconds;
                            oldTimer = newTimer;
                            if (Delta >= 300)
                            {
                                Console.WriteLine("Temps imparti écoulé, votre action n'est pas comptabilisée");
                                tourFini = true;
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                while (motAAjouter.Length > 15 || motAAjouter.Length < 2)
                                {
                                    if (motAAjouter == "Q")
                                    {
                                        Console.WriteLine("Le joueur {0} a passé son tour, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                                        tourFini = true;
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ce mot ne rentre pas dans le plateau\nVeuillez choisir un mot valable :");
                                        motAAjouter = Console.ReadLine().ToUpper();
                                    }
                                }
                                if (motAAjouter != "Q") existence = mondico[motAAjouter.Length - 2].RechDichoRecursif(motAAjouter);
                                else existence = true;
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
                                    Console.WriteLine("Vous allez placer un mot à la verticale");
                                }
                                else
                                {
                                    int compteurLettre = 0;
                                    int compteurJoker = 0;
                                    scoreMot = 0;
                                    multiplicateur = 1;
                                    string[] verifCasePremiereLettre = new string[motAAjouter.Length];
                                    Jeton nouveauJetonTire = null;
                                    for (int i = 0; i < motAAjouter.Length; i++)
                                    {
                                        if (monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY] == "_" || monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY] == Convert.ToString(motAAjouter[i]))
                                        {
                                            scoreMot += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[i])).Score;
                                            verifCasePremiereLettre[i] = "_";
                                        }
                                        if (monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY] == "2")
                                        {
                                            scoreMot += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[i])).Score * 2;
                                            verifCasePremiereLettre[i] = "2";
                                        }
                                        if (monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY] == "3")
                                        {
                                            scoreMot += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[i])).Score * 3;
                                            verifCasePremiereLettre[i] = "3";
                                        }
                                        if (monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY] == "4")
                                        {
                                            scoreMot += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[i])).Score;
                                            multiplicateur *= 2;
                                            verifCasePremiereLettre[i] = "4";
                                        }
                                        if (monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY] == "5")
                                        {
                                            scoreMot += monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[i])).Score;
                                            multiplicateur *= 3;
                                            verifCasePremiereLettre[i] = "5";
                                        }
                                    }
                                    for (int j = nbrCoordMotX - 1; j < nbrCoordMotX - 1 + motAAjouter.Length; j++)
                                    {
                                        compteurJoker = 0;
                                        if (monplateau.Matrice[j, nbrCoordMotY] != "*" && monplateau.Matrice[j, nbrCoordMotY] != Convert.ToString(motAAjouter[compteurLettre]))
                                        {
                                            for (int i = 0; i < listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Count; i++)
                                            {
                                                if (listeJoueurs[numéroJoueur - 1].ListeJetons_lettre[i] == Convert.ToString(motAAjouter[compteurLettre])) compteurJoker++;
                                            }
                                            if (compteurJoker != 0)
                                            {
                                                listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Remove(Convert.ToString(motAAjouter[compteurLettre]));
                                            }
                                            else
                                            {
                                                listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Remove("*");
                                                if (monplateau.Matrice[j, nbrCoordMotY] == "_") scoreMot -= monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[compteurLettre])).Score;
                                                if (monplateau.Matrice[j, nbrCoordMotY] == "2") scoreMot -= monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[compteurLettre])).Score * 2;
                                                if (monplateau.Matrice[j, nbrCoordMotY] == "3") scoreMot -= monsac_jetons.TrouveJeton(Convert.ToString(motAAjouter[compteurLettre])).Score * 3;
                                            }
                                            if (monsac_jetons.Sac.Count > 0) nouveauJetonTire = monsac_jetons.Retire_Jeton(aleatoire);
                                            monplateau.Matrice[j, nbrCoordMotY] = Convert.ToString(motAAjouter[compteurLettre]);
                                            if (monsac_jetons.Sac.Count > 0) listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Add(nouveauJetonTire.Lettre);
                                            if (monsac_jetons.Sac.Count > 0) Sac_Jetons.NbJetons--;
                                            if (monsac_jetons.Sac.Count > 0) monsac_jetons.Sac.Remove(nouveauJetonTire);
                                            compteurJetonPose++;
                                        }
                                        compteurLettre++;
                                    }
                                    listeJoueurs[numéroJoueur - 1].Score += scoreMot * multiplicateur;
                                    if (compteurJetonPose == 7) listeJoueurs[numéroJoueur - 1].Score += 50;
                                    int incrementation = 0;
                                    int decrementation = 0;
                                    int compteurMotsTrouves = 0;
                                    string rep = "";
                                    listeJoueurs[numéroJoueur - 1].MotsTrouves.Add(motAAjouter);
                                    for (int i = 0; i < motAAjouter.Length; i++)
                                    {
                                        compteurMotsTrouves = 0;
                                        rep = "";
                                        incrementation = 1;
                                        decrementation = -1;
                                        scoreMot = 0;
                                        multiplicateur = 1;
                                        if (nbrCoordMotY + 1 >= 0 && nbrCoordMotY + 1 <= 14 && nbrCoordMotY - 1 >= 0 && nbrCoordMotY - 1 <= 14)
                                        {
                                            if ((monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + 1] != "_" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + 1] != "2" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + 1] != "3" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + 1] != "4" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + 1] != "5") || (monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY - 1] != "_" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY - 1] != "2" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY - 1] != "3" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY - 1] != "4" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY - 1] != "5"))
                                            {
                                                while (nbrCoordMotY + incrementation <= 14 && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + incrementation] != "_" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + incrementation] != "2" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + incrementation] != "3" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + incrementation] != "4" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + incrementation] != "5")
                                                {
                                                    incrementation++;
                                                }
                                                while (nbrCoordMotY + decrementation >= 0 && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + decrementation] != "_" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + decrementation] != "2" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + decrementation] != "3" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + decrementation] != "4" && monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + decrementation] != "5")
                                                {
                                                    decrementation--;
                                                }
                                                for (int n = decrementation + 1; n < incrementation; n++)
                                                {
                                                    rep += monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + n];
                                                    if (monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + n] == "2" || (verifCasePremiereLettre[i] == "2" && n == 0)) scoreMot += monsac_jetons.TrouveJeton(monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + n]).Score * 2;
                                                    else if (monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + n] == "3" || (verifCasePremiereLettre[i] == "3" && n == 0)) scoreMot += monsac_jetons.TrouveJeton(monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + n]).Score * 3;
                                                    else if (monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + n] == "4" || (verifCasePremiereLettre[i] == "4" && n == 0))
                                                    {
                                                        scoreMot += monsac_jetons.TrouveJeton(monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + n]).Score;
                                                        multiplicateur = 2;
                                                    }
                                                    else if (monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + n] == "5" || (verifCasePremiereLettre[i] == "5" && n == 0))
                                                    {
                                                        scoreMot += monsac_jetons.TrouveJeton(monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + n]).Score;
                                                        multiplicateur = 3;
                                                    }
                                                    else scoreMot += monsac_jetons.TrouveJeton(monplateau.Matrice[nbrCoordMotX - 1 + i, nbrCoordMotY + n]).Score;
                                                }
                                                for (int a = 0; a < listeJoueurs.Count; a++)
                                                {
                                                    for (int b = 0; b < listeJoueurs[a].MotsTrouves.Count; b++)
                                                    {
                                                        if (listeJoueurs[a].MotsTrouves[b] == rep) compteurMotsTrouves++;
                                                    }
                                                }
                                                if (compteurMotsTrouves == 0)
                                                {
                                                    listeJoueurs[numéroJoueur - 1].MotsTrouves.Add(rep);
                                                    listeJoueurs[numéroJoueur - 1].Score += scoreMot * multiplicateur;
                                                }
                                            }
                                        }
                                    }
                                    Console.WriteLine("Le tour du joueur {0} est terminé, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                                    tourFini = true;
                                    Console.ReadKey();
                                }
                            }
                        }
                        break;
                    case "4": //le joueur souhaite passer son tour
                        compteurPasseTour++; //on incrémente le compteurPasseTour pour finir le jeu au bout d'un certain nombre si le sac contient moins de 7 jetons
                        Console.WriteLine("Le joueur {0} a passé son tour, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                        tourFini = true; //fin du tour
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Action non reconnue, veuillez taper '1', '2', '3' ou '4' pour réaliser une action.");
                        réponseJoueur = Console.ReadLine();
                        break;
                }
            }
        }
    }
}
