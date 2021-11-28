﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    class Joueur
    {
        private string nom;
        private int score;
        private List<string> motsTrouves;
        private List<string> listeJetons_lettre;

        public Joueur(string nom, int score = 0)
        {
            this.nom = nom;
            this.score = score;
            this.motsTrouves = new List<string>();
            this.listeJetons_lettre = new List<string>();
        }

        public Joueur(string nom, int score, List<string> motsTrouves, List<string> listeJetons_lettre)
        {
            this.nom = nom;
            this.score = score;
            this.motsTrouves = motsTrouves;
            this.listeJetons_lettre = listeJetons_lettre;
        }

        public Joueur(string fichier)
        {
            StreamReader sr = new StreamReader(fichier);
            string mot = sr.ReadLine();
            string[] ligne;

            ligne = mot.Split(';');
            this.nom = ligne[0];
            this.score = Convert.ToInt32(ligne[1]);

            mot = sr.ReadLine();
            ligne = mot.Split(';');
            this.motsTrouves = new List<string>();
            foreach (string mots in ligne) this.motsTrouves.Add(mots);

            mot = sr.ReadLine();
            ligne = mot.Split(';');
            this.listeJetons_lettre = new List<string>();
            foreach (string lettres in ligne) this.listeJetons_lettre.Add(lettres);   
        }

        public string Nom
        {
            get { return this.nom; }
        }

        public int Score
        {
            get { return this.score; }
        }

        public List<string> MotsTrouves
        {
            get { return this.motsTrouves; }
        }

        public List<string> ListeJetons_lettre
        {
            get { return this.listeJetons_lettre; }
        }

        public void Add_Mot(string mot)
        {
            this.motsTrouves.Add(mot);
        }

        public override string ToString()
        {
            Console.Write("Nom du joueur : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(this.nom);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nScore : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(this.score);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nMots trouvés : ");
            for(int i=0; i<this.motsTrouves.Count; i++)
            {
                if (i != this.motsTrouves.Count - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(this.motsTrouves[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ; ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(this.motsTrouves[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.Write("\nLettres disponibles dans sa main : ");
            for (int j=0; j<this.listeJetons_lettre.Count; j++)
            {
                if (j != this.listeJetons_lettre.Count - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(this.listeJetons_lettre[j]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ; ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(this.listeJetons_lettre[j]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            return "";
        }

        public void Add_Score(int val)
        {
            this.score += val;
        }

        public void Add_Main_Courante(Jeton monjeton)
        {
            this.listeJetons_lettre.Add(monjeton.Lettre);
        }

        public void Remove_Main_Courante(Jeton monjeton)
        {
            this.listeJetons_lettre.Remove(monjeton.Lettre);
        }
    }
}
