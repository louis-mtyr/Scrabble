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
        /// <summary>
        /// Ajoute le mot dans la liste des mots déjà trouvés par le joueur au cours de la partie
        /// </summary>
        /// <param name="mot">Mot trouvé</param>
        public void Add_Mot(string mot)
        {
            this.motsTrouves.Add(mot);
        }
        /// <summary>
        /// Retourne une chaîne de caractères qui décrit un joueur
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string rep = "Nom du joueur : " + this.nom + "\nScore : " + this.score + "\nMots trouvés : ";
            for(int i=0; i<this.motsTrouves.Count; i++)
            {
                if (i != this.motsTrouves.Count - 1) rep += this.motsTrouves[i] + " ; ";
                else rep += this.motsTrouves[i];
            }
            rep += "\nLettres disponibles dans sa main : ";
            for (int j=0; j<this.listeJetons_lettre.Count; j++)
            {
                if (j != this.listeJetons_lettre.Count - 1) rep += this.listeJetons_lettre[j] + " ; ";
                else rep += this.listeJetons_lettre[j];
            }
            return rep;
        }
        /// <summary>
        /// Ajoute une valeur au score
        /// </summary>
        /// <param name="val">Valeur à ajouté au score </param>
        public void Add_Score(int val)
        {
            this.score += val;
        }
        /// <summary>
        /// Ajoute un jeton à la main courante
        /// </summary>
        /// <param name="monjeton">Jeton qui va être ajouté à notre main</param>
        public void Add_Main_Courante(Jeton monjeton)
        {
            this.listeJetons_lettre.Add(monjeton.Lettre);
        }
        /// <summary>
        /// Retire un jeton à la main courante
        /// </summary>
        /// <param name="monjeton">Jeton qui va être retiré à notre main</param>
        public void Remove_Main_Courante(Jeton monjeton)
        {
            this.listeJetons_lettre.Remove(monjeton.Lettre);
        }
    }
}
