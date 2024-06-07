using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tarea1.Models
{
    public class Persona
    {
        [PrimaryKey]
        public string IdCedula { get; set; }
        public string Nombre { get; set; }
    }
}
