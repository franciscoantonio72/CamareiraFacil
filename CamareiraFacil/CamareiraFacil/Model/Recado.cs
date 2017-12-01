using System;
using System.Collections.Generic;
using System.Text;

namespace CamareiraFacil.Model
{
    public class Recado
    {
        public string Cod_Emp { get; set; }
        public string Destinatario { get; set; }
        public DateTime Data_Cad { get; set; }
        public string Hora { get; set; }
        public string Assunto { get; set; }
        public string Descricao { get; set; }
        public string Remetente { get; set; }
    }
}
