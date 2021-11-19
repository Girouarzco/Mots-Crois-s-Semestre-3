using System;
using System.Collections.Generic;
using System.IO;
namespace ProjetS3
{
    class Lettres
    {
        private List<Lettre> pioche;
        public List<Lettre> Pioche
        {
            get { return pioche; }
            set { this.pioche = value; }
        }
        #region construction de la pioche
        public Lettres(string filename)
        {
            int nblettres = 0;
            pioche = new List<Lettre>();
            StreamReader texte = new StreamReader(filename); //lecture du fichier 
            string ligne = " ";
            char[] sep = { ',' }; // element qui permet de separer chaque information
            while(texte.Peek() > 0)
            {
                ligne = texte.ReadLine();
                string[] info = new string[3]; 
                info = ligne.Split(sep); // retranchement de chaque information dans un tableau separé disctinctement
                char lettre = Convert.ToChar(info[0]);
                int nombrelettre = Convert.ToInt32(info[1]);
                Lettre piochecomplete = new Lettre(lettre);
                for (int i = 0; i < nombrelettre; i++)
                {
                    pioche.Add(piochecomplete);
                    nblettres++;
                }
            }
            
        }
        #endregion 
        public void EnleverLettre(Lettre lettrepioché, int nombrelettrepioche)
        {
            if (nombrelettrepioche != 0) // verifie si la pioche n'est pas vide
            {
                pioche.Remove(lettrepioché); // retire la lettre pioché de la pioche
            }
        }
        public string Tostring()
        {
            string contenue = " ";
            for(int i = 0; i < pioche.Count; i++)
            {
                contenue = contenue + pioche[i].LEttre + " ; "; // affiche le contenue de la pioche lettre par lettre
            }
            return contenue;
        }
    }
}
