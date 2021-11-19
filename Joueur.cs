using System;
using System.Collections.Generic;
using System.IO;
namespace ProjetS3
{
    class Joueur
    {
        private string nom;
        private int score;
        private List<Lettre> listelettre;
        private List<string> motstrouvés;
        private char[,] grille;
        public Joueur( string nom,char[,] grille)
        {
            listelettre = new List<Lettre>();
            motstrouvés = new List<string>();
            if (nom != null)
            {
                this.nom = nom;
                this.grille = grille;
                score = 0;
            }
        }
        #region Constructeur
        public string Nom
        { get { return nom; } }
        public int Score
        { get { return score; }
          set { score = value; } }
        public List<Lettre> Listelettre
        { get { return listelettre; }
          set { this.listelettre = value; }
        }
        public List<string> Motstrouvés
        { get { return motstrouvés; }
          set { this.motstrouvés = value; }
        }
        public char[,] Grille
        { get { return grille; } }
        #endregion
        public bool Add_Lettres(int nb, Lettres pioche, Random r) // renvoie vrai le joueur a reussi a pioche des lettre faux sinon
        {
            if (pioche.Pioche.Count > 0) // verifie si la pioche n'est pas vide
            {
                while (nb > 0) // continue tant que le joueur doit piocher des lettres
                {
                    if (pioche.Pioche.Count > 0) // verifie si la pioche n'est pas vide
                    {
                        int aléatoire = r.Next(0, pioche.Pioche.Count); // nombre aléatoire entre 0 et le nombre de lettre dans la pioche
                        listelettre.Add(pioche.Pioche[aléatoire]); // ajoute dans la main une lettre de la pioche
                        pioche.EnleverLettre(pioche.Pioche[aléatoire], pioche.Pioche.Count);// enleve une lettre de la pioche
                        nb--;
                    }
                    else
                    {
                        Console.WriteLine("Il manque " + nb + " à ajouter"); // au cas ou il ne reste pas assez de lettre dans la pioche
                        break;
                    }
                }
                return true;
            }
            else { return false; }
        }
        #region affichage
        public string toString()

         {
            string txt = "         ";
            for(int i = 1; i <= grille.GetLength(1);i++)
            {
                if (i < 10)
                {
                    txt = txt + i + "  ";
                }
                else { txt = txt + i + " "; }
            }
            txt = txt + "\n";
            for(int i = 0; i < (8 + 3*grille.GetLength(1));i++)
            {
                txt = txt + "_";
            }
            txt = txt + "\n";
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                int a = i + 1;
                if (i <9)
                {
                    txt = txt + a + "      |";
                }
                else { txt = txt + a + "     |"; }
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    if (grille[i,j] > 64 && grille[i, j] < 91)
                    {
                        txt = txt + " " + grille[i, j] + "|";
                    }
                    else { txt = txt + " " + grille[i, j] + " |"; } 
                }
                txt = txt + "\n";
            }
            txt = txt + "\n";
            txt = txt + "Joueur : " + nom + "\n";
            for(int i = 0; i < listelettre.Count; i++)
            {
                txt = txt + listelettre[i].LEttre + " ";
            }
            txt = txt + "\n";
            return txt;
        }
        #endregion
        public void Add(string mot) // ajoute un mot a la liste des mots trouvés
        {
            
            motstrouvés.Add(mot); 
            
        }
        public void OteLettre(string mot) // enleve une lettre de la liste des lettre
        {
            
            mot = mot.ToUpper();
            for (int i = 0; i < mot.Length; i++)
            {
                
                for (int j = 0; j < listelettre.Count; j++)
                {
                    char symbole = listelettre[j].LEttre;
                    if (symbole.Equals(mot[i]))
                    {
                        listelettre.RemoveAt(j);
                            break;
                    }                                       
                }
            }         
        }
        public bool presencedeslettres(string mot) //verifie si les lettres sont present soit dans la mains soit dans la grille
        {
            mot = mot.ToUpper();
            bool presence = true;
            for (int i = 0; i < mot.Length; i++)
            {
                presence = false;
                for (int j = 0; j < listelettre.Count; j++)
                {
                    char symbole = listelettre[j].LEttre;
                    if (symbole.Equals(mot[i]))
                    {
                        presence = true;
                        break;
                    }
                    else
                    {
                        for (int k = 0; k < grille.GetLength(0); k++)
                        {
                            for (int l = 0; l < grille.GetLength(0); l++)
                            {
                                if(grille[k,l].Equals(mot[i]))
                                {
                                    presence = true;
                                }
                            }
                        }
                    }
                }
                if (presence == false)
                {
                    break;
                }
            }
            return presence;
        }
        public bool presencedeslettresmain(char mot) // verifie si cette lettre est presente dans la main 
        {
            for (int i = 0; i < Listelettre.Count; i++)
            {
               char symb = Listelettre[i].LEttre;
               if (mot.Equals(symb))
               {
               return true;
               }
            }
            return false;
        }
        public List<char> transformermot (string mot) // transforme le mot en une liste de lettre 
        {
            mot = mot.ToUpper();
            List<char> liste = new List<char>();
            for (int i = 0; i <mot.Length; i++)
            {
                liste.Add(mot[i]);
            }
            return liste;
        }
        public string retransformermot(List<char> mot)// transforme la liste de lettre en string
        {
            string mot2 ="";
            for (int i = 0; i < mot.Count; i++)
            {
                mot2 = mot2 + mot[i];
            }
            return mot2;
        }
        public bool placermot(int x, int y,int horver, List<char> mot2, string mot) // verifie et place le mot vrai si c'est possible faux sinon
        {
            mot = mot.ToUpper();
            bool placement = true;
            if (x  < grille.GetLength(0) && y < grille.GetLength(1))
            {
                if (horver == 0)
                {
                    if ((y + mot.Length - 1) < grille.GetLength(0))
                    {
                        for (int i = 0; i < mot.Length; i++)
                        {
                            if ((grille[x-1,y+i-1] < 64 || grille[x - 1, y + i - 1] > 91) && presencedeslettresmain(mot[i]) == true)
                            {
                                grille[x - 1, y + i - 1] = mot[i];
                            }
                            else if (grille[x - 1, y + i - 1] == mot[i])
                            {
                                mot2.RemoveAt(i);
                            }
                            else
                            {
                                placement = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        placement = false;
                    }
                }
                else if (horver == 1)
                {
                    if ((x + mot.Length - 1) < grille.GetLength(1))
                    {
                        for (int i = 0; i < mot.Length; i++)
                        {
                            if ((grille[x - 1 + i, y  - 1] < 64 || grille[x - 1 + i, y  - 1] > 91 )&& presencedeslettresmain(mot[i]) == true)
                            {
                                grille[x - 1 + i , y - 1] = mot[i];
                            }
                            else if (grille[x - 1, y - 1] == mot[i])
                            {
                                mot2.RemoveAt(i);
                            }
                            else
                            {
                                placement = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        placement = false;
                    }
                }
                else
                {
                    placement = false;
                }
            }
            else { placement = false; }
            return placement;

        }
        public bool MotsCroisés( Dictionnaire dictionnaire) // verifie que tous les mots constitués par le placement de lettre est un vrai mot
        {
            
            int i = 0;
            bool valide;
            while (i < Grille.GetLength(0))
            {
                int j = 0;
                while (j < Grille.GetLength(1))
                {
                    if (grille[i, j] > 64 && grille[i, j] < 91)
                    {
                        string mot = Convert.ToString(grille[i, j]);
                        j++;
                        while (grille[i, j] > 64 && grille[i, j] < 91)
                        {
                            mot = mot + grille[i, j];
                            j++;
                        }
                        if (mot.Length >= 2)
                        {
                            valide = dictionnaire.RechdichoRecursif(0, dictionnaire.Dico[mot.Length].Count, mot);
                            if (valide == false)
                            {
                                return false;
                            }
                        }
                        
                    }
                    else { j++; }
                    
                }
                i++;
            }
            i = 0;
            while (i < Grille.GetLength(1))
            {
                int j = 0;
                while (j < Grille.GetLength(0))
                {
                    if (grille[j, i] > 64 && grille[j, i] < 91)
                    {
                        string mot = Convert.ToString(grille[j, i]);
                        j++;
                        while (grille[j, i] > 64 && grille[j, i] < 91)
                        {
                            mot = mot + grille[j, i];
                            j++;
                        }
                        if (mot.Length >= 2)
                        {
                            valide = dictionnaire.RechdichoRecursif(0, dictionnaire.Dico[mot.Length].Count, mot);
                            if (valide == false)
                            {
                                return false;
                            }
                        }

                    }
                    else { j++; }
                }
                i++;
            }
            return true;
        }
        public int MotsCroisésscore() // calcul le score grace au mots trouvés
        {
            int scoretotal = 0;
            for (int i = 0; i < motstrouvés.Count; i++)
            {
                if (motstrouvés[i].Length > 5)
                {
                    scoretotal += motstrouvés[i].Length;
                }
            }
            return scoretotal;
        }
        public int calculscore() // calcul le score dans la grille en fct du poids des lettres
        {
            int score2 = 0;
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    if (grille[i, j] > 64 && grille[i, j] < 91)
                    {
                        Lettre lettre = new Lettre(grille[j, i]);
                        score2 = score2 + lettre.Poids;
                    }
                }
            }
            return score2;
            
        }
        public void matricedessais(char[,] matrice, bool motscroises) // a utiliser si placermot renvois false afin de ne pas changer la grille 
        {
   
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    if (motscroises == true)
                    {
                        matrice[i, j] = grille[i, j];
                    }
                    else { grille[i, j] = matrice[i, j]; }
                }
            }
        }
       

    }
}
