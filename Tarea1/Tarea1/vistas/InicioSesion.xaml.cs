using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tarea1.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarea1.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioSesion : ContentPage
    {
        private Controlador.db_Conexion database;
        public InicioSesion()
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tarea1.db3");
            database = new Controlador.db_Conexion(dbPath);
        }
        private async void btnAñadir(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdCedula.Text) || string.IsNullOrEmpty(Nombre.Text))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
                return; // Detiene la ejecución del método si hay campos vacíos
            }

            var persona = new Persona
            {
                IdCedula = IdCedula.Text,
                Nombre = Nombre.Text
            };

            await database.SavePersonaAsync(persona);
            await DisplayAlert("Añadido", "Usuario añadido correctamente", "OK");
        }

        private async void btnEditar(object sender, EventArgs e)
        {
            var persona = await database.GetPersonaAsync(IdCedula.Text);
            if (persona != null)
            {
                persona.Nombre = Nombre.Text;
                await database.UpdatePersonaAsync(persona);
                await DisplayAlert("Editado", "Usuario editado correctamente", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No se encontró ningún usuario con esa cédula", "OK");
            }
        }
        private async void btnEliminar(object sender, EventArgs e)
        {
            var persona = await database.GetPersonaAsync(IdCedula.Text);
            if (persona != null)
            {
                await database.DeletePersonaAsync(persona);
                IdCedula.Text = "";
                Nombre.Text = "";
                await DisplayAlert("Eliminado", "Usuario eliminado correctamente", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No se encontró ningún usuario con esa cédula", "OK");
            }
        }
        private async void btnMostrar(object sender, EventArgs e)
        {
            var personas = await database.GetPersonasAsync();
            if (personas != null && personas.Count > 0)
            {
                UsuariosListView.ItemsSource = personas;
                UsuariosListView.IsVisible = true;
            }
            else
            {
                UsuariosListView.IsVisible = false;
                await DisplayAlert("Error", "No hay usuarios guardados", "OK");
            }
        }
    }
}