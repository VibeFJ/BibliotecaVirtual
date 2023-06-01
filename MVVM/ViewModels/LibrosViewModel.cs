using PropertyChanged;
using System.Collections.ObjectModel;
using BibliotecaVirtual.MVVM.Models;
using BibliotecaVirtual.MVVM.Views;
using System.Windows.Input;
using SQLiteNetExtensions.Extensions;
using Command = Microsoft.Maui.Controls.Command;
using static Bogus.DataSets.Name;

namespace BibliotecaVirtual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class LibrosViewModel
    {
        public bool validacion = true;
        public string Nombre { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }

        private ObservableCollection<Libro> _lvm;
        public ObservableCollection<Libro> LVM
        {
            get { return _lvm; }
            set
            {
                _lvm = value;
            }
        }

        public LibrosViewModel()
        {
            if(validacion)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    ObtenerLibros();
                });
            }
        }

        public void ObtenerLibros()
        {
            validacion = false;
            LVM = new ObservableCollection<Libro>();

            var libros = App.CustomerRepo.conexion.Table<Libro>().ToList();
            foreach (var libro in libros)
            {
                var autor = App.CustomerRepo.conexion.Table<Autor>().FirstOrDefault(u => u.AutorId == libro.AutorId);
                var genero = App.CustomerRepo.conexion.Table<Genero>().FirstOrDefault(u => u.GeneroId == libro.GeneroId);

                LVM.Add(new Libro()
                {
                    Nombre = libro.Nombre,
                    Autor = autor.Nombre + " " + autor.ApellidoPaterno + " " + autor.ApellidoMaterno,
                    Genero = genero.Nombre
                });
            }
        }
    }
}
