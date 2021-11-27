using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    class Plateau
    {
        private string[,] matrice;

        public Plateau(string[,] matrice = null) //jcomprends pas ce qu'ils demandent
        {
            this.matrice = matrice;
        }

        public Plateau(string fichier)
        {
            StreamReader sr = new StreamReader(fichier);
            string mot = sr.ReadLine();
            string[] ligne;
            for(int i=0; i<15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    ligne = mot.Split(';');
                    this.matrice[i, j] = ligne[j];
                }
            }
        }
        public override string ToString()
        {
            string rep = "";
            for(int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
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
                            this.matrice[i, j] = "5";
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
                            this.matrice[i, j] = "4";
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
                            this.matrice[i, j] = "3";
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
                            this.matrice[i, j] = "2";
                            break;
                        case (7, 7):
                            this.matrice[i, j] = "*";
                            break;

                    }
                    rep = rep + this.matrice[i, j]+" ";
                }
                rep = rep + "\n";
            }
        }
    }
}
