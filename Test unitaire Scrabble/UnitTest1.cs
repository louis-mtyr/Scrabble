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
    }
}
