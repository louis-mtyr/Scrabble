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
        /*[TestMethod]
        public void TestTo_StringJoueur()
        {
            bool rep = true;
            List<string> motstrouv?s = new List<string>();
            List<string> listeJetons_lettre = new List<string>();
            listeJetons_lettre.Add("A");
            motstrouv?s.Add("Bonjour");
            Joueur joueur = new Joueur("L?o", 0, motstrouv?s, listeJetons_lettre);
            if ((joueur.ToString() != "Nom du joueur : L?o" + "\nScore : 0" + "\nMots trouv?s : Bonjour" + "\nLettres disponibles dans sa main : A ") rep = false;
            Assert.AreEqual(rep, true);
        }*/
        [TestMethod]
        public void TestRetire_un_nombre()
        {
            bool rep = true;
            Jeton A = new Jeton("A", 1, 10);
            A.Retire_un_nombre();
            if (A.NombreJ!=9) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestToStringJeton()
        {
            bool rep = true;
            Jeton A = new Jeton("A", 1, 10);
            if (A.ToString()!= "Lettre : A" + "\nScore : 1" + "\nLettres A" + " restantes : 10") rep = false;
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
        public void TestRetire_Jeton(Random r)
        {
            bool rep = true;
            Sac_Jetons sac = new Sac_Jetons("Jetons.txt");
            int[] stock = new int[27];
            for (int i = 0; i < 27; i++) stock[i] = sac.Sac[i].NombreJ;
            for (int i=0; i<27; i++)
            {
               
                if (sac.Retire_Jeton(r).NombreJ != stock[i] - 1) rep = false;
            }
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestAjoute_Jeton()
        {
            bool rep = true;
            Sac_Jetons sac = new Sac_Jetons("Jetons.txt");
            int stock= sac.Sac[0].NombreJ;
            sac.Ajoute_Jeton("a");
            if (sac.Sac[0].NombreJ != stock + 1) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestTrouveJeton()
        {
            bool rep = true;
            Sac_Jetons sac = new Sac_Jetons("Jetons.txt");
            Jeton Jetoncherche = sac.TrouveJeton("a");
            Jeton resulattendu = new Jeton("A", 1, 9);
            if (Jetoncherche.NombreJ != resulattendu.NombreJ|| Jetoncherche.Lettre != resulattendu.Lettre|| Jetoncherche.Score != resulattendu.Score) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestToString_SacJetons()
        {
            bool rep = true;
            Sac_Jetons sac = new Sac_Jetons("Jetonstest.txt");
            if (sac.ToString() != "Il reste dans le sac : "+ 9 +" jeton(s) de la lettre A qui valent "+1+" point(s).\n") rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestToStringPlateau()
        {
            bool rep = true;
            Sac_Jetons sac = new Sac_Jetons("Jetons.txt");
            Random r = new Random();
         
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void Test_Test_Plateau()
        {
            bool rep = true;
            Sac_Jetons sac = new Sac_Jetons("Jetons.txt");
            Random r = new Random();
    
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestJouer()
        {
            bool rep = true;
            Sac_Jetons sac = new Sac_Jetons("Jetons.txt");
            Random r = new Random();
            
            Assert.AreEqual(rep, true);
        }
    }
}
