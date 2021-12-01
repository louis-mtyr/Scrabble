using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Scrabble
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd_Mot()
        {
            bool rep=true;
            List<string> motstrouvés =new List<string>();
            List<string> listeJetons_lettre = new List<string>();
            motstrouvés.Add("Salut");
            string mot = "Bonjour";
            Joueur joueur = new Joueur ("Léo",0,motstrouvés,listeJetons_lettre);
            joueur.Add_Mot(mot);
            if (mot != joueur.MotsTrouves[1]) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestAdd_Score()
        {
            bool rep = true;
            List<string> motstrouvés = new List<string>();
            List<string> listeJetons_lettre = new List<string>();
            int score = 5;
            Joueur joueur = new Joueur("Léo", 0, motstrouvés, listeJetons_lettre);
            joueur.Add_Score(score);
            if (score != joueur.Score) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestAdd_Main_Courante()
        {
            bool rep=true;
            List<string> motstrouvés =new List<string>();
            List<string> listeJetons_lettre = new List<string>();
            string jeton = "A";
            Joueur joueur = new Joueur ("Léo",0,motstrouvés,listeJetons_lettre);
            joueur.Add_Main_Courante(jeton);
            if (jeton != joueur.ListeJetons_lettre[0]) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestRemove_Main_Courante()
        {
            bool rep = true;
            List<string> motstrouvés = new List<string>();
            List<string> listeJetons_lettre = new List<string>();
            listeJetons_lettre.Add("A");
            string jeton = "A";
            Joueur joueur = new Joueur("Léo", 0, motstrouvés, listeJetons_lettre);
            joueur.Remove_Main_Courante(jeton);
            if (joueur.ListeJetons_lettre.Count!=0) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestTo_StringJoueur()
        {
            bool rep = true;
            List<string> motstrouvés = new List<string>();
            List<string> listeJetons_lettre = new List<string>();
            listeJetons_lettre.Add("A");
            motstrouvés.Add("Bonjour");
            Joueur joueur = new Joueur("Léo", 0, motstrouvés, listeJetons_lettre);
            if (joueur.ToString() != "Nom du joueur : Léo" + "\nScore : 0" + "\nMots trouvés : Bonjour" + "\nLettres disponibles dans sa main : A ") rep = false;
            Assert.AreEqual(rep, true);
        }
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
            List<string> motstrouvés = new List<string>(3);
            Dictionnaire mondico = new Dictionnaire(motstrouvés,3,"francais");
            if (mondico.ToString() != "Le Dictionnaire contient : 3 mots de 3 lettres en francais.") rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestRechDichoRecursif()
        {
            bool rep = true;
            string mot = "AAS";
            Dictionnaire mondico = new Dictionnaire("Francais.txt",3);
            if (mondico.RechDichoRecursif(mot) !=true) rep = false;
            Assert.AreEqual(rep, true);
        }
        [TestMethod]
        public void TestRetireJeton()
        {
            bool rep = true;
            string mot = "AAS";
            Dictionnaire mondico = new Dictionnaire("Francais.txt", 3);
            if (mondico.RechDichoRecursif(mot) != true) rep = false;
            Assert.AreEqual(rep, true);
        }
    }
}
