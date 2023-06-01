using BibliotecaVirtual.MVVM.ViewModels;

namespace BibliotecaVirtual.MVVM.Views;

public partial class Libros : ContentView
{
	public Libros()
	{
		InitializeComponent();
        BindingContext = new LibrosViewModel();
    }
}