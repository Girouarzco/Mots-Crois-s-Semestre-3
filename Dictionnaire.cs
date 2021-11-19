using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
namespace ProjetS3
{
    class Dictionnaire
    {
        private Dictionary<int,List<string>> dico;
        public Dictionnaire()
        {
            dico = new Dictionary<int, List<string>>(); 
        }
        public void Remplissage(string Filename)
        {
            using (StreamReader Fichier = new StreamReader(Filename))
            {
                string ligne;
                int nombre;
                int nbrLettres = 1;
                while(Fichier.Peek() > 0)
                { 
                    ligne = Fichier.ReadLine();
                    if(Int32.TryParse(ligne,out nombre)==false && ligne!= null)
                    {
                        nbrLettres++;
                        string[] valeur = ligne.Split(' ');
                        List<string> datas = new List<string>();
                        for (int i = 0; i < valeur.Length; i++)
                        {
                            datas.Add(valeur[i]);
                        }
                        dico.Add(nbrLettres, datas);
                    }

                }
            }
        }
        public Dictionary<int, List<string>> Dico
        { get { return dico; } }
        public bool RechdichoRecursif(int debut,int fin, string mot) // recherche si le mot est valide parmi tous les mots du dictionnaire
        {
            mot = mot.ToUpper();
            int longueur = mot.Length;
            int milieu = (debut + fin) / 2;
            string milieududico = dico[longueur][milieu];
            if (debut > fin) return false;
            if (milieududico == mot) return true;
            for (int i = 0; i < longueur; i++)
            {
                if (valeuralphabet(mot[i]) < valeuralphabet(dico[longueur][milieu][i]))
                { return RechdichoRecursif(debut, milieu-1, mot); }
                if (valeuralphabet(mot[i]) > valeuralphabet(dico[longueur][milieu][i]))
                { return RechdichoRecursif(milieu+1, fin, mot); }

            } return true;
            

        }
        static int valeuralphabet (char lettre)
        {
            int valeur = 0;
            if(lettre == 'A')
            {
               valeur= 1;
            }
            if (lettre == 'B')
            {
                valeur = 2;
            }
            if (lettre == 'C')
            {
                valeur = 3;
            }
            if (lettre == 'D')
            {
                valeur = 4;
            }
            if (lettre == 'E')
            {
                valeur = 5;
            }
            if (lettre == 'F')
            {
                valeur = 6;
            }
            if (lettre == 'G')
            {
                valeur = 7;
            }
            if (lettre == 'H')
            {
                valeur = 8;
            }
            if (lettre == 'I')
            {
                valeur = 9;
            }
            if (lettre == 'J')
            {
                valeur = 10;
            }
            if (lettre == 'K')
            {
                valeur = 11;
            }
            if (lettre == 'L')
            {
                valeur = 12;
            }
            if (lettre == 'M')
            {
                valeur = 13;
            }
            if (lettre == 'N')
            {
                valeur = 14;
            }
            if (lettre == 'O')
            {
                valeur = 15;
            }
            if (lettre == 'P')
            {
                valeur = 16;
            }
            if (lettre == 'Q')
            {
                valeur = 17;
            }
            if (lettre == 'R')
            {
                valeur = 18;
            }
            if (lettre == 'S')
            {
                valeur = 19;
            }
            if (lettre == 'T')
            {
                valeur = 20;
            }
            if (lettre == 'U')
            {
                valeur = 21;
            }
            if (lettre == 'V')
            {
                valeur = 22;
            }
            if (lettre == 'W')
            {
                valeur = 23;
            }
            if (lettre == 'X')
            {
                valeur = 24;
            }
            if (lettre == 'Y')
            {
                valeur = 25;
            }
            if (lettre == 'Z')
            {
                valeur = 26;
            }
            return valeur;
        } // emplacement des lettres dans l'alphabet
        #region Contenue du dictionnaire
        public string Tostring()
        {
            string txt = "Le dictionnaire est composé des mots suivants : ";
            for (int i = 0; i < dico.Count; i++)
            {
                for (int j = 0; j < dico[i].Count; j++)
                {
                    txt = txt + dico[i][j] + "; ";
                }
            }
            return txt;
        }
        #endregion 
    }
}
