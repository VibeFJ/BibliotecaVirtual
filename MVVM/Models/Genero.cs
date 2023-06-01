using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaVirtual.MVVM.Models
{
    public class Genero
    {
        [PrimaryKey, AutoIncrement]
        public int GeneroId { get; set; }
        public string Nombre { get; set; }
        public string Descripción { get; set; }
    }
}
