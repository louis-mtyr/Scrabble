using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.IO;

namespace Scrabble
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd_Mot()
        {
            bool rep=true;
            string mot = "BONJOUR";
            Joueur joueur = new Joueur ("Joueurs.txt");
            joueur.Add_Mot(mot);
            if (joueur.MotsTrouves[0]!="BAT"|| joueur.MotsTrouves[1] != "SEBUM"|| joueur.MotsTrouves[2] != "LEXEMES"|| joueur.MotsTrouves[3] != "BONJOUR") rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestAdd_Score()
        {
            bool rep = true;
            Joueur joueur = new Joueur("Joueurs.txt");
            int val = 5;
            joueur.Add_Score(val);
            if (61 != joueur.Score) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestAdd_Main_Courante()
        {
            bool rep=true;
            Joueur joueur = new Joueur("Joueurs.txt");
            string jeton = "A";
            joueur.Add_Main_Courante(jeton);
            if (jeton != joueur.ListeJetons_lettre[7]) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestRemove_Main_Courante()
        {
            bool rep = true;
            Joueur joueur = new Joueur("Joueurs.txt");
            string jeton = "A";
            joueur.Remove_Main_Courante(jeton);
            if (joueur.ListeJetons_lettre[0]!="B") rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestToStringJeton()
        {
            bool rep = true;
            Jeton A = new Jeton("A", 1);
            if (A.ToString()!= "Lettre : A" + "\nScore : 1") rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestToStringDictionnaire()
        {
            bool rep = true;
            List<string> motstrouvés = new List<string>();
            for (int i = 1; i <= 3; i++) motstrouvés.Add(Convert.ToString(i));
            Dictionnaire mondico = new Dictionnaire(motstrouvés,3,"francais");
            if (mondico.ToString() != "Le Dictionnaire contient : " + 3 + " mots de " + 3 + " lettres en francais.") rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestRechDichoRecursif()
        {
            bool rep = true;
            string mot = "aas";
            Dictionnaire mondico = new Dictionnaire("Francais.txt",3);
            if (mondico.RechDichoRecursif(mot) != true) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestAjoute_Jeton()
        {
            bool rep = true;
            Sac_Jetons sac = new Sac_Jetons("Jetons.txt");
            Jeton stock= sac.Sac[10];
            sac.Ajoute_Jeton("a");
            if (sac.Sac[9] != stock) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestTrouveJeton()
        {
            bool rep = true;
            Sac_Jetons sac = new Sac_Jetons("Jetons.txt");
            Jeton Jetoncherche = sac.TrouveJeton("a");
            Jeton resulattendu = new Jeton("A", 1);
            if (Jetoncherche.Lettre != resulattendu.Lettre|| Jetoncherche.Score != resulattendu.Score) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void Test_Test_Plateau()
        {
            string mot1 = "DJJZZ";
            int colonne = 7;
            int ligne = 7;
            char direction = 'h';
            Dictionnaire[] ledico = new Dictionnaire[13];
            for(int i = 0; i < 13; i++)
            {
                ledico[i] = new Dictionnaire("Francais.txt", i + 2);
            }
            Joueur joueur = new Joueur("Léo", 0);
            joueur.ListeJetons_lettre.Add("A");
            joueur.ListeJetons_lettre.Add("V");
            joueur.ListeJetons_lettre.Add("I");
            joueur.ListeJetons_lettre.Add("O");
            joueur.ListeJetons_lettre.Add("N");
            joueur.ListeJetons_lettre.Add("P");
            joueur.ListeJetons_lettre.Add("Q");
            Plateau plateau = new Plateau("TestPlateau.txt", ledico, joueur);
            bool rep = true;
            if (plateau.Test_Plateau(mot1, ligne, colonne, direction) == true) rep = false;
            mot1 = "AVION";
            if (plateau.Test_Plateau(mot1, ligne, colonne, direction) == false) rep = false;
            ligne = -1;
            if (plateau.Test_Plateau(mot1, ligne, colonne, direction) == true) rep = false;
            colonne = 0;
            if (plateau.Test_Plateau(mot1, ligne, colonne, direction) == true) rep = false;
            Assert.AreEqual(rep, true);
        }
    }
}
