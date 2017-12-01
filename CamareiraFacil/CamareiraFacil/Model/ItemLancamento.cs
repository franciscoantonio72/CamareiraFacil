using System;
using System.Collections.Generic;
using System.Text;

namespace CamareiraFacil.Model
{
    public class ItemLancamento
    {
        public string Cod_Emp { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public string Codigo_PDV { get; set; }
        public string Codigo_Apto { get; set; }
        public string Operador { get; set; }
    }
}
