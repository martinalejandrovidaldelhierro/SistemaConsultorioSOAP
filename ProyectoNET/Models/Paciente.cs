using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaConsultorioSOAP.Models
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
    }
}