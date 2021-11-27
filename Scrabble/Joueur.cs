using System;
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

        public Joueur(string nom, int score = 0, List<string> motsTrouves = null, List<string> listeJetons_lettre = null)
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

        public List<string> ListeJetons
        {
            get { return this.listeJetons_lettre; }
        }

        public void Add_Mot(string mot)
        {
            this.motsTrouves.Add(mot);
        }

        public override string ToString()
        {
            string rep = "Nom du joueur : " + this.nom + "\nScore : " + this.score + "\nMots trouvés : ";
            for(int i=0; i<this.motsTrouves.Count; i++)
            {
                if (i != this.motsTrouves.Count - 1) rep += this.motsTrouves[i] + " ; ";
                else rep += this.motsTrouves[i];
            }
            return rep;
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
