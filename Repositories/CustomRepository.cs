using BibliotecaVirtual.MVVM.Models;
using Bogus;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace BibliotecaVirtual.Repositories
{
    public class CustomRepository
    {
        private Autor ObjAutor;
        private Genero ObjGenero;
        private Libro ObjLibro;

        public SQLiteConnection conexion;
        public string EstatusMensaje { get; set; }

        public CustomRepository()
        {
            var faker = new Faker("es");

            conexion = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);

            if (!TablasExisten())
            {
                conexion.CreateTable<Usuario>();
                conexion.CreateTable<Autor>();
                conexion.CreateTable<Genero>();
                conexion.CreateTable<Libro>();

                List<string> generos = new List<string>()
                {
                    "Terror",
                    "Ciencia ficción",
                    "Romance",
                    "Fantasía",
                    "Misterio",
                    "Aventura"
                };

                for (int i = 1; i < 11; i++)
                {
                    ObjAutor = new Autor()
                    {
                        Nombre = faker.Name.FirstName(),
                        ApellidoPaterno = faker.Name.LastName(),
                        ApellidoMaterno = faker.Name.LastName(),
                        FechaNacimiento = FechaNacimientoAleatoria().ToString("dd/MM/yyyy")
                    };
                    conexion.Insert(ObjAutor);

                    ObjGenero = new Genero()
                    {
                        Nombre = faker.PickRandom(generos),
                        Descripción = faker.Lorem.Paragraph()
                    };

                    conexion.Insert(ObjGenero);
                }

                var objAutores = conexion.Table<Autor>().ToList();
                var objGeneros = conexion.Table<Genero>().ToList();

                int indiceAutor = faker.Random.Int(0, objAutores.Count - 1);
                int indiceGenero = faker.Random.Int(0, objGeneros.Count - 1);

                for (int i = 1; i < 8; i++)
                {
                    ObjLibro = new Libro()
                    {
                        Nombre = "Libro " + i.ToString(),
                        AutorId = objAutores[indiceAutor].AutorId,
                        GeneroId = objGeneros[indiceGenero].GeneroId,
                        Resumen = "Descripción de el Libro " + i.ToString(),
                    };

                    conexion.Insert(ObjLibro);
                }
            }
        }

        public bool TablasExisten()
        {
            var tablasExistentes = conexion.GetTableInfo("Usuario").Any() &&
                                   conexion.GetTableInfo("Autor").Any() &&
                                   conexion.GetTableInfo("Genero").Any() &&
                                   conexion.GetTableInfo("Libro").Any();

            return tablasExistentes;
        }

        private DateTime FechaNacimientoAleatoria()
        {
            var faker = new Faker("es");
            var fechaMinima = new DateTime(1950, 1, 1); // Establece una fecha mínima de nacimiento
            var fechaMaxima = new DateTime(2000, 12, 31); // Establece una fecha máxima de nacimiento

            return faker.Date.Between(fechaMinima, fechaMaxima);
        }
    }
}
