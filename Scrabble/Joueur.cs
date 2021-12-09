using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    public class Joueur
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
            StreamReader sr = new StreamReader(fichier); //permet de lire le fichier mis en paramètre
            string mot = sr.ReadLine(); //Le mot prend la première ligne du fichier
            string[] ligne;

            ligne = mot.Split(';'); //On rentre dans le tableau les termes du mot séparé par des ;
            this.nom = ligne[0];
            this.score = Convert.ToInt32(ligne[1]);

            mot = sr.ReadLine(); 
            ligne = mot.Split(';');
            this.motsTrouves = new List<string>();
            foreach (string mots in ligne) this.motsTrouves.Add(mots); //On rentre dans une liste les mots du fichier

            mot = sr.ReadLine();
            ligne = mot.Split(';');
            this.listeJetons_lettre = new List<string>();
            foreach (string lettres in ligne) this.listeJetons_lettre.Add(lettres); //On rentre dans une liste les lettres du fichier
        }

        public string Nom
        {
            get { return this.nom; }
        }

        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }

        public List<string> MotsTrouves
        {
            get { return this.motsTrouves; }
            set { this.motsTrouves = value; }
        }

        public List<string> ListeJetons_lettre
        {
            get { return this.listeJetons_lettre; }
            set { this.listeJetons_lettre = value; }
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
                if (i != this.motsTrouves.Count - 1 && this.motsTrouves[i]!="")
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
        /// <summary>
        /// Ajoute une valeur au score
        /// </summary>
        /// <param name="val">Valeur à ajouté au score</param>
        public void Add_Score(int val)
        {
            this.score += val;
        }
        /// <summary>
        /// Ajoute un jeton à la main courante
        /// </summary>
        /// <param name="monjeton">Jeton qui va être ajouté à notre main</param>
        public void Add_Main_Courante(string monjeton)
        {
            this.listeJetons_lettre.Add(monjeton);
        }
        /// <summary>
        /// Retire un jeton à la main courante
        /// </summary>
        /// <param name="monjeton">Jeton qui va être retiré à notre main</param>
        public void Remove_Main_Courante(string monjeton)
        {
            this.listeJetons_lettre.Remove(monjeton);
        }

        public void WriteFile(string filename)
        {
            string[] texte = new string[3];
            FileStream fichier=null;
            if (this.nom != null && this.motsTrouves != null && this.listeJetons_lettre != null)
            {
                try
                {
                    fichier = new FileStream(filename, FileMode.Open, FileAccess.Write, FileShare.Write);
                    for (int i = 0; i < texte.Length; i++)
                    {
                        if (i == 0) AddText(fichier,this.nom+";"+this.score+";\n");
                        if (i == 1)
                        {
                            if (motsTrouves.Count != 0)
                            {
                                for (int j = 0; j < motsTrouves.Count; j++)
                                {
                                    if (j != motsTrouves.Count - 1)
                                    {
                                        AddText(fichier,this.motsTrouves[j] + ";");
                                    }
                                    else
                                    {
                                        AddText(fichier,this.motsTrouves[j] + "\n");
                                    }
                                }
                            }
                            else AddText(fichier,"\n");
                        }
                        if (i == 2)
                        {
                            for (int k=0; k<7; k++)
                            {
                                if (k != 6)
                                {
                                    AddText(fichier,this.listeJetons_lettre[k] + ";");
                                }
                                else
                                {
                                    AddText(fichier,this.listeJetons_lettre[k]+"\n");
                                }
                            }
                        }
                    }
                }
                catch (FileNotFoundException f)
                {
                    Console.WriteLine("Le fichier n'existe pas.\n" + f.Message);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Problème(s) de type de valeurs entrées.\n" + fe.Message);
                }
            }
            fichier.Close();
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }


}
