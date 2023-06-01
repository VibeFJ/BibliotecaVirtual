using System;
using BibliotecaVirtual.MVVM.Models;
using BibliotecaVirtual.MVVM.ViewModels;
using BibliotecaVirtual.MVVM.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;

namespace BibliotecaVirtual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class PrincipalViewModel
    {
        private ContentView detallesView;
        public ContentView DetallesView
        {
            get => detallesView;
            set
            {
                if (detallesView != value)
                {
                    detallesView = value;
                }
            }
        }

        public ICommand ProgramacionMovilCommand { get; }
        public ICommand MineriaDatosCommand { get; }

        public PrincipalViewModel()
        {
            ProgramacionMovilCommand = new Command(NavigateToProgramacionMovil);
            MineriaDatosCommand = new Command(NavigateToMineriaDatos);

            Task.Run(async () =>
            {
                await Task.Delay(2000);
                NavigateToProgramacionMovil();
            });
        }

        private void NavigateToProgramacionMovil()
        {
            DetallesView = new Libros();
        }

        private void NavigateToMineriaDatos()
        {
            DetallesView = new Graficos();
        }
    }
}
