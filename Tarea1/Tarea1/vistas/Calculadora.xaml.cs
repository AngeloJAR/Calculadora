using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarea1.vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Calculadora : ContentPage
	{
        //Se almacena la operacion actual a realizar, luego se almacena el historial de opreaciones, y finalmente se realiza la conexion a la base de datos
        private string currentOperation;
        private ObservableCollection<Models.Historial> operaciones;
        private Controlador.db_Conexion database;
        public Calculadora()
        {
            InitializeComponent();
            //Se defne la ruta de la base de datos, luego se iniciza la conexion
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Operaciones.db3");
            database = new Controlador.db_Conexion(dbPath);
            operaciones = new ObservableCollection<Models.Historial>();
            //Se establece la tabla para el historial como un listview
            HostorialListView.ItemsSource = operaciones;
            //carga de operaciones existentes desde la base de datos
            LoadOperaciones();
        }
        //metodo para cargar las operaciones
        private async Task LoadOperaciones()
        {
            //obtiene las operaciones de la DB y agrega la operacion a una coleccion
            var ops = await database.GetOperacionesAsync();
            foreach (var op in ops)
            {
                operaciones.Add(op);
            }
        }
        //metodo para el boton de las operacions
        private void operacionesSRMD(object sender, EventArgs e)
        {
            //almacena, muestra campos para el ingreso de numeros, limpia los labels para las operaciones
            var button = (Button)sender;
            currentOperation = button.Text;
            IngresoNumeros.IsVisible = true;
            ResultadoLabel.Text = "";
            Numero1Entry.Text = "";
            Numero2Entry.Text = "";
        }
        //metodos para el boton de calcular
        private async void btnCalcular(object sender, EventArgs e)
        {
            //primero verifica que ambos campos no esten vacios si lo estan nos da un mensaje para rellenar los campos
            if (string.IsNullOrEmpty(Numero1Entry.Text) || string.IsNullOrEmpty(Numero2Entry.Text))
            {
                await DisplayAlert("Error", "Por favor, ingrese ambos números.", "OK");
                return;
            }
            //convertir los numero de entrada a numeros
            if (!double.TryParse(Numero1Entry.Text, out double numero1) || !double.TryParse(Numero2Entry.Text, out double numero2))
            {
                await DisplayAlert("Error", "Por favor, ingrese números válidos.", "OK");
                return;
            }

            double resultado = 0; //variable que almacena el resultado de la operacions
            //menu para realizar la operacion seleccionada
            switch (currentOperation)
            {
                case "Suma":
                    resultado = numero1 + numero2;
                    break;
                case "Resta":
                    resultado = numero1 - numero2;
                    break;
                case "Multiplicación":
                    resultado = numero1 * numero2;
                    break;
                case "División":
                    if (numero2 != 0)
                    {
                        resultado = numero1 / numero2;
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se puede dividir entre cero.", "OK");
                        return;
                    }
                    break;
            }
            //mostrar el resultado 
            ResultadoLabel.Text = "Resultado: " + resultado.ToString();

            //crear una nueva instancia con los datos de la operacion es decir solo con esos datos ingresados se realiza la operacion 
            var operacion = new Models.Historial
            {
                Fecha = DateTime.Now.ToString("g"), //nos da la fecha y la hora acutal
                operacion = currentOperation,
                Numero1 = numero1.ToString(),
                Numero2 = numero2.ToString(),
                Resultado = resultado.ToString()
            };
            //guarda la operacion y añade la operacion a la tabla 
            await database.SaveOperacionAsync(operacion);
            operaciones.Add(operacion);
            //limpiar campos
            Numero1Entry.Text = "";
            Numero2Entry.Text = "";
        }
        //metodo para el boton eliminar
        private async void btnEliminar(object sender, EventArgs e)
        {

            var button = (Button)sender;
            //obtener la operacion vinculada al boton 
            var operacion = (Models.Historial)button.CommandParameter;
            //elimina la operacion de la DB
            await database.DeleteOperacionAsync(operacion);
            //eliminar la operacion de la tabla
            operaciones.Remove(operacion);
        }
        private async void btnLayout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

    }
}