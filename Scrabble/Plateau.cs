using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    public class Plateau
    {
        private string[,] matrice;
        private Dictionnaire[] leDico;
        private Joueur leJoueur;

        public Plateau(string[,] matrice = null) //jcomprends pas ce qu'ils demandent
        {
            this.matrice = matrice;
        }
        
        public Plateau(string fichier, Dictionnaire[] leDico, Joueur leJoueur)
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

        public Plateau(string[,] matrice, Dictionnaire[] leDico, Joueur leJoueur)
        {
            this.matrice = matrice;
            this.leDico = leDico;
            this.leJoueur = leJoueur;
        }

        public string[,] Matrice
        {
            get { return this.matrice; }
        }
        public Dictionnaire[] LeDico
        {
            get { return this.leDico; }
        }
            public Joueur LeJoueur
        {
            get { return this.leJoueur; }
        }
        /// <summary>
        /// Retourne une chaîne de caractères qui décrit le plateau
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("    A B C D E F G H I J K L M N O\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("   --------------------------------\n");
            for (int i = 0; i < 15; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (i <= 8) Console.Write(" " + (i + 1) + " ");
                else Console.Write(i + 1 + " ");
                Console.ForegroundColor = ConsoleColor.White;
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
                                Console.Write("3 ");
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
                                Console.Write("2 ");
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
            Console.Write("   --------------------------------");
            return "";
        }
        /// <summary>
        /// Teste si le mot passé en paramètre est un mot éligible aux positions ligne et colonne et dans la direction indiquée
        /// </summary>
        /// <param name="mot">Mot que l'on veut posé</param>
        /// <param name="ligne">Index de la ligne où l'on veut posé la première lettre du mot</param>
        /// <param name="colonne">Index de la colonne où l'on veut posé la première lettre du mot</param>
        /// <param name="direction">Direction dans laquelle on veut posé le mot</param>
        /// <returns></returns>
        public bool Test_Plateau(string mot,int ligne,int colonne,char direction)
        {
            bool verif = true;
            int compteurMain = 0;
            int compteurCasesNonVides = 0;
            int compteurCasesDifferentesCentre = 0;
            int compteurCasesDifferentesLettresPosees = 0;
            string rep = "";
            int accrémentation = 1;
            if (ligne < 0 || colonne < 0 || ligne > 14 || colonne > 14)
            {
                verif = false;
            }
            else 
            {
                switch (direction)
                {
                    case 'h':
                        if (colonne + mot.Length > 15)
                        {
                            verif = false;
                            Console.WriteLine("Le mot ne rentre pas dans le plateau");
                        }
                        else
                        {
                            for (int i = 0; i < mot.Length; i++)
                            {
                                if (matrice[ligne, colonne+i] != "_" && matrice[ligne, colonne+i] != "*")
                                {
                                    if (matrice[ligne, colonne+i] != Convert.ToString(mot[i]))
                                    {
                                        verif = false;
                                        Console.WriteLine("Votre mot va empiéter sur un mot déjà présent sur le plateau");
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < 7; j++)
                                    {
                                        if (Convert.ToString(mot[i]) == this.leJoueur.ListeJetons_lettre[j] || this.leJoueur.ListeJetons_lettre[j] == "*" || Convert.ToString(mot[i]) == this.matrice[ligne, colonne + i])
                                        {
                                            compteurMain++;
                                        }                           
                                    }
                                    if (compteurMain == 0)
                                    {
                                        verif = false;
                                        Console.WriteLine("Il vous manque une lettre ou plus du mot que vous voulez écrire");
                                    }
                                    compteurMain = 0;
                                }
                            }
                        }
                        for (int i = 0; i < this.matrice.GetLength(0) && compteurCasesNonVides==0; i++)
                        {
                            for (int j = 0; j < this.matrice.GetLength(1) && compteurCasesNonVides==0; j++)
                            {
                                if (matrice[i, j] != "_") compteurCasesNonVides++;
                            }
                        }
                        if (compteurCasesNonVides == 0)
                        {
                            for (int k = 0; k < mot.Length; k++)
                            {
                                if (ligne == 7 && colonne + k == 7) compteurCasesDifferentesCentre++;
                            }
                            if (compteurCasesDifferentesCentre == 0)
                            {
                                verif = false;
                                Console.WriteLine("Le premier mot à écrire sur le plateau doit avoir une lettre sur la case (8,H)");
                            }
                        }
                        else
                        {
                            for (int k = -1; k < mot.Length + 1; k++)
                            {
                                for (int n = -1; n <= 1; n++)
                                {
                                    if ((ligne + n >= 0 && ligne + n <= 14) && (colonne + k >= 0 && colonne + k <= 14))
                                    {
                                        if (matrice[ligne + n, colonne + k] != "_") compteurCasesDifferentesLettresPosees++;
                                    }
                                }
                            }
                            if (compteurCasesDifferentesLettresPosees == 0)
                            {
                                verif = false;
                                Console.WriteLine("Votre mot doit être relié d'une certaine manière à un mot déjà présent sur le plateau");
                            }


                            if (verif == true)
                            {
                                if (matrice[ligne, colonne - accrémentation] != "_" && matrice[ligne, colonne + mot.Length - 1 + accrémentation] == "_")
                                {
                                    while (matrice[ligne, colonne - accrémentation] != "_" && colonne - accrémentation >= 0)
                                    {
                                        rep = matrice[ligne, colonne - accrémentation] + rep;
                                        accrémentation++;
                                    }
                                    rep = rep + mot;
                                    if (leDico[rep.Length-2].RechDichoRecursif(rep) == false)
                                    {
                                        verif = false;
                                        Console.WriteLine("L'emplacement de votre mot va créer un nouveau mot qui n'existe pas dans le dictionnaire");
                                    }
                                    accrémentation = 1;
                                    rep = "";
                                }
                                if (matrice[ligne, colonne + mot.Length - 1 + accrémentation] != "_" && matrice[ligne, colonne - accrémentation] == "_")
                                {
                                    while (matrice[ligne, colonne + mot.Length - 1 + accrémentation] != "_" && colonne + mot.Length - 1 + accrémentation <= 14)
                                    {
                                        rep = rep + matrice[ligne, colonne + mot.Length - 1 + accrémentation];
                                        accrémentation++;
                                    }
                                    rep = mot + rep;
                                    if (leDico[rep.Length-2].RechDichoRecursif(rep) == false)
                                    {
                                        verif = false;
                                        Console.WriteLine("L'emplacement de votre mot va créer un nouveau mot qui n'existe pas dans le dictionnaire");
                                    }
                                    accrémentation = 1;
                                    rep = "";
                                }
                                if (matrice[ligne, colonne + mot.Length - 1 + accrémentation] != "_" && matrice[ligne, colonne - accrémentation] != "_")
                                {
                                    while (matrice[ligne, colonne - accrémentation] != "_" && colonne - accrémentation >= 0)
                                    {
                                        rep = matrice[ligne, colonne - accrémentation] + rep;
                                        accrémentation++;
                                    }
                                    accrémentation = 1;
                                    rep = rep + mot;
                                    while (matrice[ligne, colonne + mot.Length - 1 + accrémentation] != "_" && colonne + mot.Length - 1 + accrémentation <= 14)
                                    {
                                        rep = rep + matrice[ligne, colonne + mot.Length - 1 + accrémentation];
                                        accrémentation++;
                                    }
                                    if (leDico[rep.Length-2].RechDichoRecursif(rep) == false)
                                    {
                                        verif = false;
                                        Console.WriteLine("L'emplacement de votre mot va créer un nouveau mot qui n'existe pas dans le dictionnaire");
                                    }
                                    accrémentation = 1;
                                    rep = "";
                                }
                                for (int i = 0; i < mot.Length; i++)
                                {
                                    if (matrice[ligne - accrémentation, colonne + i] != "_" || matrice[ligne + accrémentation, colonne + i] != "_")
                                    {
                                        rep = Convert.ToString(mot[i]);
                                        while (matrice[ligne - accrémentation, colonne + i] != "_" && ligne - accrémentation >= 0)
                                        {
                                            rep = matrice[ligne - accrémentation, colonne + i] + rep;
                                            accrémentation++;
                                        }
                                        accrémentation = 1;
                                        while (matrice[ligne + accrémentation, colonne + i] != "_" && ligne + accrémentation <= 14)
                                        {
                                            rep = rep + matrice[ligne + accrémentation, colonne + i];
                                            accrémentation++;
                                        }
                                        if (leDico[rep.Length-2].RechDichoRecursif(rep) == false)
                                        {
                                            verif = false;
                                            Console.WriteLine("L'emplacement de votre mot va créer un nouveau mot qui n'existe pas dans le dictionnaire");
                                        }
                                        accrémentation = 1;
                                        rep = "";
                                    }
                                }
                            }
                        }
                        break;
                    case 'v':
                        if (ligne + mot.Length > 15)
                        {
                            verif = false;
                            Console.WriteLine("Le mot ne rentre pas dans le plateau");
                        }
                        for (int i = 0; i < mot.Length; i++)
                        {
                            if (matrice[ligne+i, colonne] != "_" && matrice[ligne+i, colonne] != "*")
                            {
                                if (matrice[ligne+i, colonne] != Convert.ToString(mot[i]))
                                {
                                    verif = false;
                                    Console.WriteLine("Votre mot va empiéter sur un mot déjà présent sur le plateau");
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    if (Convert.ToString(mot[i]) == this.leJoueur.ListeJetons_lettre[j] || this.leJoueur.ListeJetons_lettre[j] == "*" || Convert.ToString(mot[i]) == this.matrice[ligne + i, colonne])
                                    {
                                        compteurMain++;
                                    }
                                }
                                if (compteurMain == 0)
                                {
                                    verif = false;
                                    Console.WriteLine("Il vous manque une ou plusieurs lettre pour écrire votre mot");
                                }
                                compteurMain = 0;
                            }
                        }
                        for (int i = 0; i < this.matrice.GetLength(0); i++)
                        {
                            for (int j = 0; j < this.matrice.GetLength(1); j++)
                            {
                                if (matrice[i, j] != "_") compteurCasesNonVides++;
                            }
                        }
                        if (compteurCasesNonVides == 0)
                        {
                            for (int k = 0; k < mot.Length; k++)
                            {
                                if (ligne + k == 7 && colonne == 7) compteurCasesDifferentesCentre++;
                            }
                            if (compteurCasesDifferentesCentre == 0)
                            {
                                verif = false;
                                Console.WriteLine("Le premier mot à être placé sur le plateau doit avoir une lettre sur la case (8,H)");
                            }
                        }
                        else
                        {
                            for (int k = -1; k < mot.Length + 1; k++)
                            {
                                for (int n = -1; n <= 1; n++)
                                {
                                    if ((ligne + k >= 0 && ligne + k <= 14) && (colonne + n >= 0 && colonne + n <= 14))
                                    {
                                        if (matrice[ligne + k, colonne + n] != "_") compteurCasesDifferentesLettresPosees++;
                                    }
                                }
                            }
                            if (compteurCasesDifferentesLettresPosees == 0)
                            {
                                verif = false;
                                Console.WriteLine("Votre mot doit être relié d'une certaine manière à un mot déjà présent sur le plateau");
                            }

                            if (verif == true)
                            {
                                if (matrice[ligne - accrémentation, colonne] != "_" && matrice[ligne + mot.Length - 1 + accrémentation, colonne] == "_")
                                {
                                    while (matrice[ligne - accrémentation, colonne] != "_" && (ligne - accrémentation) >= 0)
                                    {
                                        rep = matrice[ligne - accrémentation, colonne] + rep;
                                        accrémentation++;
                                    }
                                    rep = rep + mot;
                                    if (leDico[rep.Length-2].RechDichoRecursif(rep) == false)
                                    {
                                        verif = false;
                                        Console.WriteLine("L'emplacement de votre mot va créer un nouveau mot qui n'existe pas dans le dictionnaire");
                                    }
                                    accrémentation = 1;
                                    rep = "";
                                }
                                if (matrice[ligne + mot.Length - 1 + accrémentation, colonne] != "_" && matrice[ligne - accrémentation, colonne] == "_")
                                {
                                    while (matrice[ligne + mot.Length - 1 + accrémentation, colonne] != "_" && ligne + mot.Length - 1 + accrémentation <= 14)
                                    {
                                        rep = rep + matrice[ligne + mot.Length - 1 + accrémentation, colonne];
                                        accrémentation++;
                                    }
                                    rep = mot + rep;
                                    if (leDico[rep.Length-2].RechDichoRecursif(rep) == false)
                                    {
                                        verif = false;
                                        Console.WriteLine("L'emplacement de votre mot va créer un nouveau mot qui n'existe pas dans le dictionnaire");
                                    }
                                    accrémentation = 1;
                                    rep = "";
                                }
                                if (matrice[ligne - accrémentation, colonne] != "_" && matrice[ligne + mot.Length - 1 + accrémentation, colonne] != "_")
                                {
                                    while (matrice[ligne - accrémentation, colonne] != "_" && (ligne - accrémentation) >= 0)
                                    {
                                        rep = matrice[ligne - accrémentation, colonne] + rep;
                                        accrémentation++;
                                    }
                                    accrémentation = 1;
                                    rep = rep + mot;
                                    while (matrice[ligne + mot.Length - 1 + accrémentation, colonne] != "_" && ligne + mot.Length - 1 + accrémentation <= 14)
                                    {
                                        rep = rep + matrice[ligne + mot.Length - 1 + accrémentation, colonne];
                                        accrémentation++;
                                    }
                                    if (leDico[rep.Length-2].RechDichoRecursif(rep) == false)
                                    {
                                        verif = false;
                                        Console.WriteLine("L'emplacement de votre mot va créer un nouveau mot qui n'existe pas dans le dictionnaire");
                                    }
                                    accrémentation = 1;
                                    rep = "";
                                }
                                for (int i = 0; i < mot.Length; i++)
                                {
                                    if (matrice[ligne + i, colonne - accrémentation] != "_" || matrice[ligne + i, colonne + accrémentation] != "_")
                                    {
                                        rep = Convert.ToString(mot[i]);
                                        while (matrice[ligne + i, colonne - accrémentation] != "_" && colonne - accrémentation >= 0)
                                        {
                                            rep = matrice[ligne + i, colonne - accrémentation] + rep;
                                            accrémentation++;
                                        }
                                        accrémentation = 1;
                                        while (matrice[ligne + i, colonne + accrémentation] != "_" && colonne + accrémentation <= 14)
                                        {
                                            rep = rep + matrice[ligne + i, colonne + accrémentation];
                                            accrémentation++;
                                        }
                                        if (leDico[rep.Length-2].RechDichoRecursif(rep) == false)
                                        {
                                            verif = false;
                                            Console.WriteLine("L'emplacement de votre mot va créer un nouveau mot qui n'existe pas dans le dictionnaire");
                                        }
                                        accrémentation = 1;
                                        rep = "";
                                    }
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
            return verif;
        }
    }
}
