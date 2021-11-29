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
            while (tourFini == false)
            {
                switch (réponseJoueur)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine(this.monplateau.ToString());
                        Console.WriteLine(listeJoueurs[numéroJoueur - 1].ToString());
                        Jeton jetonPioché = this.monsac_jetons.Retire_Jeton(aleatoire);
                        Console.WriteLine("Le jeton pioché est : " + jetonPioché.Lettre + "\nLequel de vos jetons souhaitez-vous remplacer par ce nouveau jeton ?");
                        string jetonARemplacer = Console.ReadLine().ToUpper();
                        bool appartient = false;
                        while (appartient == false)
                        {
                            for (int i = 0; i < listeJoueurs[numéroJoueur - 1].ListeJetons_lettre.Count; i++)
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
                        Console.WriteLine("Le tour du joueur {0} est terminé, appuyez sur une touche pour passer au tour suivant.", numéroJoueur);
                        tourFini = true;
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.WriteLine("Quel est le mot que vous voulez ajouter sur le plateau ?");
                        string motAAjouter = Console.ReadLine().ToUpper();
                        while (motAAjouter.Length > 15 || motAAjouter.Length < 2)
                        {
                            Console.WriteLine("Ce mot ne rentre pas dans le plateau\nVeuillez choisir un mot valable :");
                            motAAjouter = Console.ReadLine().ToUpper();
                        }
                        bool existence = mondico[motAAjouter.Length-2].RechDichoRecursif(motAAjouter); //possibilité de devoir ajuster en fonction du dico / tableau de dicos --> on peut faire un dico par taille des mots
                        while (existence==false)                                    //par exemple mondico[0] = dico des mots de 2 lettres, mondico[1] = dico des mots de 3 lettres etc...
                        {                                                           //problème : à quoi sert la langue dans les attributs du Dictionnaire ?
                            Console.WriteLine("Ce mot n'existe pas dans le dictionnaire choisi\nVeuillez choisir un mot valable :");
                            motAAjouter = Console.ReadLine();
                            existence = mondico[motAAjouter.Length-2].RechDichoRecursif(motAAjouter);
                        }
                        Console.WriteLine("Veuillez indiquer la ligne de la 1ere lettre de votre mot sur le plateau");
                        string coordMotX = Console.ReadLine();
                        int nbrCoordMotX;
                        do
                        {
                            if (int.TryParse(coordMotX, out nbrCoordMotX))
                            {
                                Console.WriteLine("Cette coordonnée est invalide");
                                coordMotX = Console.ReadLine();
                            }
                            else
                            {
                                nbrCoordMotX = 0;
                                Console.WriteLine("Cette coordonnée est invalide");
                                coordMotX = Console.ReadLine();
                            }
                        } while (nbrCoordMotX < 1 && nbrCoordMotX > 15);
                        Console.WriteLine("Veuillez indiquer la colonne de la 1ere lettre de votre mot sur le plateau");
                        string coordMotY = Console.ReadLine().ToUpper();
                        do
                        {
                            switch (coordMotY)
                            {
                                case "A":
                                    break;
                                default:
                                    Console.WriteLine("Cette coordonnée est invalide");
                                    coordMotY = Console.ReadLine().ToUpper();
                                    break;
                            }
                        } while (coordMotY != "A" && coordMotY != "B" && coordMotY != "C" && coordMotY != "D" && coordMotY != "E" && coordMotY != "F" && coordMotY != "G" && coordMotY != "H" && coordMotY != "I" && coordMotY != "J" && coordMotY != "K" && coordMotY != "L" && coordMotY != "M" && coordMotY != "N" && coordMotY != "O");

                        //coord en Y a faire
                        break;
                    case "3":
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
