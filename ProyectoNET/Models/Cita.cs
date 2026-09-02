using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaConsultorioSOAP.Models
{
    public class Cita
    {
        public int IdCita { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Motivo { get; set; }
        public string Tratamiento { get; set; }
        public bool Estado { get; set; }
        public int IdPaciente { get; set; }
    }
}