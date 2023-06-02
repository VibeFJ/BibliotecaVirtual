using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using BibliotecaVirtual.MVVM.Models;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Win32;

namespace BibliotecaVirtual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GraficosViewModel
    {
        public ISeries[] Series { get; set; }

        List<int> Datos = new List<int> {};

        double[] datosGrafica = new double[]{0};

        private int contador = 0;
        private int año = 2001;
        private double valorB;
        private double valorM;
        private int valor_x;
        private int valor_y;
        public int ValorX
        {
            get { return valor_x; }
            set
            {
                if (valor_x != value)
                {
                    valor_x = value;
                }
            }
        }

        public int ValorY
        {
            get { return valor_y; }
            set
            {
                if (valor_y != value)
                {
                    valor_y = value;
                }
            }
        }

        public ICommand GuardarCommand { get; set; }
        public ICommand GraficarMCommand { get; set; }
        public ICommand GraficarBCommand { get; set; }

        public GraficosViewModel()
        {
            ValorX = año;
            GuardarCommand = new Command(GuardarDatos);
            GraficarMCommand = new Command(GraficarM);
            GraficarBCommand = new Command(GraficarB);
        }

        private void GuardarDatos()
        {
            Datos.Add(valor_y);
            double[] nuevosDatos = new double[] { valor_y };

            List<double> listaTemporal = new List<double>(datosGrafica);
            listaTemporal.AddRange(nuevosDatos);
            datosGrafica = listaTemporal.ToArray();

            Series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = datosGrafica,
                    Fill = null
                }
            };

            contador++;
            ValorX++;
        }

        private void GraficarM()
        {
            double Exy = 0;
            double Ex = 0;
            double Ey = 0;
            double Ex2 = 0;
            double _Ex_ = 0;

            for (int i = 0; i < contador; i++)
            {
                //m = (n - ∑(x*y) - ∑x * ∑y) / (n * ∑x^2 - |∑x|^2)

                //Observación 1: x = 1, y = 7
                //Observación 2: x = 2, y = 5
                //Observación 3: x = 3, y = 3

                //∑(xy) = x1y1 + x2y2 + x3y3 = (1*7) + (2*5) + (3*3) = 7 + 10 + 9 = 26
                //∑x = x1 + x2 + x3 = 1 + 2 + 3 = 6
                //∑y = y1 + y2 + y3 = 7 + 5 + 3 = 15
                //∑x ^ 2 = x1 ^ 2 + x2 ^ 2 + x3 ^ 2 = 1 ^ 2 + 2 ^ 2 + 3 ^ 2 = 1 + 4 + 9 = 14
                //|∑x | ^2 = (∑x)^2 = 6 ^ 2 = 36

                //m = (n * ∑(x*y) - ∑x * ∑y) / (n * ∑x^2 - |∑x|^2)
                //= ((3 * 26) - (6 * 15)) / ((3 * 14) - 36)

                Exy += (i+1)*Datos[i];
                Ex += (i+1);
                Ey += Datos[i];
                Ex2 += (i + 1) * (i + 1);
                _Ex_ = Ex*Ex;
            }

            valorM = ((contador * Exy) - (Ex * Ey)) / ((contador * Ex2) - _Ex_);

            Application.Current.MainPage.DisplayAlert("Aviso", "El valor de M es: " + $"{valorM}", "Aceptar");
        }

        private void GraficarB()
        {
            double Exy = 0;
            double Ex = 0;
            double Ey = 0;
            double Ex2 = 0;
            double _Ex_ = 0;

            for (int i = 0; i < contador; i++)
            {
                //b = (∑y - ∑x^2 - ∑x * ∑(x*y)) / (n * ∑x^2 - |∑x|^2)

                //Observación 1: x = 1, y = 7
                //Observación 2: x = 2, y = 5
                //Observación 3: x = 3, y = 3

                //∑y = y1 + y2 + y3 = 7 + 5 + 3 = 15
                //∑x^2 = x1^2 + x2^2 + x3^2 = 1^2 + 2^2 + 3^2 = 1 + 4 + 9 = 14
                //∑x = x1 + x2 + x3 = 1 + 2 + 3 = 6
                //∑(xy) = x1y1 + x2y2 + x3y3 = (17) + (25) + (3*3) = 7 + 10 + 9 = 26
                //|∑x|^2 = (∑x)^2 = 6^2 = 36

                //b = (∑y * ∑x^2 - ∑x * ∑(x*y)) / (n * ∑x^2 - |∑x|^2)
                //= (15 * 14 - 6 * 26) / (3 * 14 - 36)

                Ey += Datos[i];
                Ex2 += (i + 1) * (i + 1);
                Ex += (i + 1);
                Exy += (i + 1) * Datos[i];
                _Ex_ = Ex * Ex;
            }
            valorB = ((Ey * Ex2) - (Ex * Exy)) / ((contador * Ex2) - _Ex_);

            Application.Current.MainPage.DisplayAlert("Aviso", "El valor de B es: " + $"{valorB}", "Aceptar");
        }
    }
}
