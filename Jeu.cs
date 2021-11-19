using System;
using System.Collections.Generic;
using System.IO;
namespace ProjetS3
{
    class Jeu
    {
        private List<Joueur> joueurs;
        private List<string> listenoms;
        Dictionnaire dico = new Dictionnaire();
        public Dictionnaire Dico { get { return dico; } }
        public Random aleatoire = new Random();
        private Lettres pioche;
        public Lettres Pioche { get { return pioche; } } 
        public List<Joueur> Joueurs { get { return joueurs; } }
        public List<string> Listenoms { get { return listenoms; } }
        public Jeu(List<string> listenoms, string dicofile, string piochefile, char[,] grille)
        {
            joueurs = new List<Joueur>();
            for (int i = 0; i < listenoms.Count; i++)
            {
                char[,] grille2 = new char[grille.GetLength(0), grille.GetLength(1)];
                Joueur joueur = new Joueur(listenoms[i],grille2);
                joueurs.Add(joueur);
            }
            this.listenoms = listenoms;
            pioche = new Lettres(piochefile);
            dico.Remplissage(dicofile);

        }
        public void depart()
        {
            for (int i = 0; i < joueurs.Count; i++)
            {
                bool ajout = joueurs[i].Add_Lettres(4, pioche, aleatoire);
            }
        }
        public void tour(int i) // phase de jeu d'un joueur
        {
            
            bool ajout = joueurs[i].Add_Lettres(2, pioche, aleatoire);
            string txt2 = joueurs[i].toString();
            Console.WriteLine(txt2);
            int nblettrepioche = pioche.Pioche.Count;
            Console.WriteLine("nombre de lettres dans la pioche : " + nblettrepioche);
            Console.WriteLine("Ecrire le mot trouvé! (pressez espace puis entre si aucun mot n'est trouvé)");
            string mot = Convert.ToString(Console.ReadLine());
            if (mot == " ") // si le joueur ne trouve pas de mots avec les lettres
            {
                return;
            }
            bool vraimot = Dico.RechdichoRecursif(2, Dico.Dico[mot.Length].Count, mot); // verifie la validité du mot 
            while (vraimot == false || joueurs[i].Motstrouvés.Contains(mot) )
            {
                 Console.WriteLine("Ecrire un mot valide ou qui n'a pas encore été trouvé!");
                 mot = Convert.ToString(Console.ReadLine());
                 vraimot = Dico.RechdichoRecursif(2, Dico.Dico[mot.Length].Count, mot);
            }
            joueurs[i].Motstrouvés.Add(mot);
            bool presence = joueurs[i].presencedeslettres(mot);
            while (presence == false) // tant que le joueur indique un mot qu'il ne peut pas placer
            {
                 Console.WriteLine("Vous ne disposez pas des lettres nécessaires!");
                 Console.WriteLine("Ecrire le mot trouvé!");
                 mot = Convert.ToString(Console.ReadLine());
                 presence = joueurs[i].presencedeslettres(mot);
            }
            Console.WriteLine("Ecrire la ligne sur laquelle vous voulez placer le mot!");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ecrire la colonne sur laquelle vous voulez placer le mot!");
            int y = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("0 pour a l'horizontale et 1 pour la verticale");
            int horver = Convert.ToInt32(Console.ReadLine());
            List<char> listelettremot = joueurs[i].transformermot(mot);
            char[,] matrice = new char[joueurs[i].Grille.GetLength(0), joueurs[i].Grille.GetLength(1)];
            joueurs[i].matricedessais(matrice,true);
            bool placement = joueurs[i].placermot(x, y, horver, listelettremot, mot);
            bool croisement = joueurs[i].MotsCroisés(Dico);
            while (croisement == false) // tant que la grille n'est pas valide 
            {
                while (placement == false || croisement == false)
                {
                    joueurs[i].matricedessais(matrice, croisement);
                    Console.WriteLine("Placement impossible, veuillez changer vos coordonnées! Les mots qui se croisent ou le mot forment une combinaison impossible!");
                    Console.WriteLine("");
                    Console.WriteLine("Ecrire la ligne sur laquelle vous voulez placer le mot!");
                    x = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ecrire la colonne sur laquelle vous voulez placer le mot!");
                    y = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("0 pour a l'horizontale et 1 pour la verticale");
                    horver = Convert.ToInt32(Console.ReadLine());
                    placement = joueurs[i].placermot(x, y, horver, listelettremot, mot);
                    croisement = joueurs[i].MotsCroisés(Dico);
                }
            }
            mot = joueurs[i].retransformermot(listelettremot);
            joueurs[i].OteLettre(mot);
        }
        public void joueurdoué(int i) // si un joueur utilise toutes ses lettres ajoute 2 lettres a tous les joueurs
        {
            if(joueurs[i].Listelettre.Count == 0)
            {
                for (int j = 0; j < joueurs.Count; j++)
                {
                    joueurs[j].Add_Lettres(2, pioche, aleatoire);
                }
            }
        }
        public bool gagnant() // determine le gagnant si la partie est termine
        {
            if (pioche.Pioche.Count == 0 )
            {
                for (int i = 0; i < joueurs.Count; i++)
                {
                    if (joueurs[i].Listelettre.Count < 4)
                    {
                        Console.WriteLine("La partie est terminé.");
                        for (int j = 0; j < joueurs.Count; j++)
                        {
                            joueurs[j].Score = joueurs[j].MotsCroisésscore() + joueurs[j].calculscore() ;
                            Console.WriteLine("Le score de " + joueurs[j].Nom + " est de : " + joueurs[j].Score);
                            
                        }
                        int gagnant2 = joueurgagnant();
                        Console.WriteLine("Le joueur qui a gagner est " + joueurs[gagnant2].Nom + " avec un score de " + joueurs[gagnant2].Score);
                        string txt = "il a trouvé les mots suivants : ";
                        for (int k = 0; k < joueurs[gagnant2].Motstrouvés.Count; k++)
                        {
                            txt = txt + joueurs[gagnant2].Motstrouvés[k] + "; ";
                        }
                        Console.WriteLine(txt);
                        return true;
                    }
                }
            }
            
            return false;

        }
        public int joueurgagnant()
        {
            int index = 0;
            for (int i = 1; i < Joueurs.Count; i++)
            {
                if(joueurs[index].Score < joueurs[i].Score)
                {
                    index = i;
                }
            }
            return index;
        }
    }
}
