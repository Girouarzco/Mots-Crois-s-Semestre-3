using System;
using System.Collections.Generic;
using System.IO;

namespace ProjetS3
{
    class Program
    {
        
        
        static void Main(string[] args)
        { 
            int nbjoueur;
            Console.WriteLine("Combien de ligne pour votre grille?");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Combien de colonne pour votre grille?");
            int y = Convert.ToInt32(Console.ReadLine());
            char[,] grille = new char[x, y]; // creation dans la grille en fonction des besoins du joueurs
            List<string> listedenoms = new List<string>();
            do
            {
                Console.WriteLine("Combien de joueurs êtes vous?");
                nbjoueur = Convert.ToInt32(Console.ReadLine());      
                if (nbjoueur > 1)
                {
                    for (int index = 1; index <= nbjoueur; index++)
                    {
                        Console.WriteLine("Quel est le nom du joueur " + index + " ?");
                        string nom = Convert.ToString(Console.ReadLine());
                        listedenoms.Add(nom);  // liste des noms des joueurs        
                    }
                }
                else
                {
                    Console.WriteLine("Vous devez au moins être deux joueurs.");
                }

            } while (nbjoueur < 1); // 2 joueurs minimum
            Jeu version1 = new Jeu(listedenoms, "MotsPossibles1.txt", "lettre.txt", grille); // creation de la partie
            version1.depart();// initialisation des decks des joueurs

            while (version1.gagnant() == false) // tant que personne ne gagne les joueurs jouent tour a tour 
            {
                for (int i = 0; i < version1.Joueurs.Count; i++)
                {
                    Console.Clear();
                    version1.tour(i);
                }
            }             
            Console.ReadKey();
        }
    }
}
