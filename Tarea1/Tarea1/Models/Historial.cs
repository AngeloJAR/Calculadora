using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace Tarea1.Models
{
    public class Historial
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string Fecha { get; set; }
        public string operacion { get; set; }
        public string Numero1 { get; set; }
        public string Numero2 { get; set; }
        public string Resultado { get; set; }
    }
}
