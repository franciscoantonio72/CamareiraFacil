using System;
using System.Collections.Generic;
using System.Text;

namespace CamareiraFacil.Model
{
    public class OrdemServico
    {
        public string Cod_Emp { get; set; }
        public DateTime Data_Cad { get; set; }
        public string Hora { get; set; }
        public string Status { get; set; }
        public string Descricao { get; set; }
        public string Remetente { get; set; }
        public string Operador { get; set; }
        public string Setor { get; set; }
        public string Cod_LocalManutencao { get; set; }
    }
}
