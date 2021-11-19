using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
namespace ProjetS3
{

    #region Caracteristiques de chaque lettre 
    class Lettre
    {
        private char lettre;
        private int poids;
        
        public Lettre (char lettre)
        {
            this.lettre = lettre;
            int valeur = 0;
            if(lettre == 'K' || lettre == 'W' || lettre == 'X' || lettre == 'Y' || lettre == 'Z' ) // toutes les lettres ont un poids de 0 a part 5
            {
                valeur = 5;
            }
            this.poids = valeur;
        }

        #region Constructeur
        public char LEttre
        { get { return lettre; } }
        public int Poids
        {
          get { return poids; }
        }
        #endregion
        public override string ToString()
        {
            string txt = "La lettre : " + this.lettre + " a pour poids" + poids;
            return txt;
        }
        

    }
    #endregion
}
