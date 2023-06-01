using SQLite;
using SQLiteNetExtensions.Attributes;
using ForeignKeyAttribute = SQLiteNetExtensions.Attributes.ForeignKeyAttribute;

namespace BibliotecaVirtual.MVVM.Models
{
    public class Libro
    {
        [PrimaryKey, AutoIncrement]
        public int LibroId { get; set; }
        public string Nombre { get; set; }

        [ForeignKey(typeof(Autor))]
        public int AutorId { get; set; }

        [ForeignKey(typeof(Genero))]
        public int GeneroId { get; set; }
        public string Resumen { get; set; }


        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public string Autor { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public string Genero { get; set; }

    }
}
