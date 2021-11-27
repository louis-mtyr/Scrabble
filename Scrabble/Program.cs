using System;
using System.Diagnostics;
using System.Threading;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Joueur joueur1 = new Joueur("Joueurs.txt");
            Dictionnaire dico = new Dictionnaire(null, 0, "turc");
            Plateau monplateau = new Plateau("InstancePlateau.txt",dico, joueur1);
            Console.WriteLine(monplateau.ToString());

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Thread.Sleep(5000);
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
