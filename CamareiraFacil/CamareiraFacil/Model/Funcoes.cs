using System;
using System.Collections.Generic;
using System.Text;

namespace CamareiraFacil.Model
{
    public class Funcoes
    {
        public string Encrypt(string prsSenha)
        {
            prsSenha = prsSenha.Trim().ToUpper();

            string lsTemp = prsSenha;
            for (int liCont = prsSenha.Length; liCont < 6; ++liCont)
            {
                lsTemp += Convert.ToChar(47 + liCont);
            }
            lsTemp = lsTemp.ToUpper();

            prsSenha = Convert.ToChar(Convert.ToInt16(lsTemp[5]) + 3).ToString() +
                       Convert.ToChar(Convert.ToInt16(lsTemp[1]) + 5).ToString() +
                       Convert.ToChar(Convert.ToInt16(lsTemp[2]) + 7).ToString() +
                       Convert.ToChar(Convert.ToInt16(lsTemp[0]) + 2).ToString() +
                       Convert.ToChar(Convert.ToInt16(lsTemp[4]) + 2).ToString() +
                       Convert.ToChar(Convert.ToInt16(lsTemp[3]) + 7).ToString();

            return prsSenha;
        }

        public string EnDecryptString(string StrValue, long Chave)
        {
            string OutValue = "";
            for (int I = 0; I < StrValue.Length; I++)
            {
                OutValue = OutValue + (char)((byte)Convert.ToChar(~+(StrValue[I] - Chave)));
            }
            return OutValue;
        }
    }
}
