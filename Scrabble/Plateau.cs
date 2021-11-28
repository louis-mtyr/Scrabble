using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{ 
    class Plateau
    {
        private string[,] matrice;
        private Dictionnaire leDico;
        private Joueur leJoueur;

        public Plateau(string[,] matrice = null) //jcomprends pas ce qu'ils demandent
        {
            this.matrice = matrice;
        }
        
        public Plateau(string fichier, Dictionnaire leDico, Joueur leJoueur)
        {
            StreamReader sr = new StreamReader(fichier);
            string mot = sr.ReadLine();
            string[] ligne;
            this.matrice = new string[15, 15];
            for(int i=0; i<15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    ligne = mot.Split(';');
                    this.matrice[i, j] = ligne[j];
                }
                mot = sr.ReadLine();
            }
            this.leDico = leDico;
            this.leJoueur = leJoueur;
        }
        public string[,] Matrice
        {
            get { return this.matrice; }
        }
        public Dictionnaire LeDico
        {
            get { return this.leDico; }
        }
            public Joueur LeJoueur
        {
            get { return this.leJoueur; }
        }

        public override string ToString()
        {
            Console.Write("--------------------------------\n");
            for (int i = 0; i < 15; i++)
            {
                Console.Write("|");
                for (int j = 0; j < 15; j++)
                {
                    if (this.matrice[i, j] == "_")
                    {
                        switch (i, j)
                        {
                            case (0, 0):
                            case (0, 7):
                            case (0, 14):
                            case (7, 0):
                            case (7, 14):
                            case (14, 0):
                            case (14, 7):
                            case (14, 14):
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("5 ");
                                break;
                            case (1, 1):
                            case (2, 2):
                            case (3, 3):
                            case (4, 4):
                            case (10, 10):
                            case (11, 11):
                            case (12, 12):
                            case (13, 13):
                            case (13, 1):
                            case (12, 2):
                            case (11, 3):
                            case (10, 4):
                            case (4, 10):
                            case (3, 11):
                            case (2, 12):
                            case (1, 13):
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write("4 ");
                                break;
                            case (1, 5):
                            case (1, 9):
                            case (5, 1):
                            case (9, 1):
                            case (13, 5):
                            case (13, 9):
                            case (9, 13):
                            case (5, 13):
                            case (5, 5):
                            case (9, 9):
                            case (9, 5):
                            case (5, 9):
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("3 ");
                                break;
                            case (0, 3):
                            case (0, 11):
                            case (14, 3):
                            case (14, 11):
                            case (3, 0):
                            case (11, 0):
                            case (3, 14):
                            case (11, 14):
                            case (2, 6):
                            case (2, 8):
                            case (3, 7):
                            case (6, 2):
                            case (8, 2):
                            case (7, 3):
                            case (6, 12):
                            case (7, 11):
                            case (8, 12):
                            case (11, 7):
                            case (12, 6):
                            case (12, 8):
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("2 ");
                                break;
                            case (7, 7):
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("* ");
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("_ ");
                                break;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(matrice[i, j] + " ");
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("|\n");
            }
            Console.Write("--------------------------------");
            return "";
        }

        public bool Test_Plateau(string mot,int ligne,int colonne,char direction)
        {
            bool verif = true;
            int compteurMain = 0;
            if (ligne >= 0 && colonne >= 0 && ligne <= 14 && colonne <= 14)
            {
                verif = false;
            }
            else 
            {
                switch (direction)
                {
                    case 'h':
                        if (colonne + mot.Length > 14)
                        {
                            verif = false;
                        }
                        for(int i = 0; i < mot.Length; i++)
                        {
                            if (matrice[ligne,colonne + i]!="_")
                            {
                                if(matrice[ligne, colonne + i] != Convert.ToString(mot[i]))        //ne marche que si on met la lettre à la position [ligne, colonne]
                                {                                                                  //faire un int k=0 et l'augmenter progressivement au lieu de mot[i]
                                    verif = false;
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    if (Convert.ToString(mot[i]) == this.leJoueur.ListeJetons[j])
                                    {
                                        compteurMain++;
                                    }
                                    if (compteurMain == 0) verif = false;                           //chépa si tous les cas sont vérifiés ici
                                    compteurMain = 0;
                                }
                            }
                        }
                        break;
                    case 'v':
                        if (ligne + mot.Length > 14)
                        {
                            verif = false;
                        }
                        for (int i = 0; i < mot.Length; i++)
                        {
                            if (matrice[ligne+i, colonne] != "_")
                            {
                                if (matrice[ligne+i, colonne] != Convert.ToString(mot[i]))        //ne marche que si on met la lettre à la position [ligne, colonne]
                                {                                                                  //faire un int k=0 et l'augmenter progressivement au lieu de mot[i]
                                    verif = false;
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    if (Convert.ToString(mot[i]) == this.leJoueur.ListeJetons[j])
                                    {
                                        compteurMain++;
                                    }
                                    if (compteurMain == 0) verif = false;                           //chépa si tous les cas sont vérifiés ici
                                    compteurMain = 0;
                                }
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Direction entrée mauvaise");
                        verif = false;
                        break;
                }
            }
            if (verif == true)
            {
                if (this.leDico.RechDichoRecursif(mot) == false)
                {
                    verif = false;
                }
            }
            return verif;
        }
    }
}
