using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Tarea1.vistas
{
    //calve: AIzaSyAh0M5HesdqZzhTBxIWmYEvgHh1aGcq2y0
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ubicacion : ContentPage
    {
        public Ubicacion()
        {
            InitializeComponent();
            
        }
        // Método asincrónico que se ejecuta cuando se hace clic en el botón de ubicación
        async void OnLocationButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Obtiene la última ubicación conocida del dispositivo de forma asincrónica
                var location = await Geolocation.GetLastKnownLocationAsync();

                // Si no hay una última ubicación conocida, intenta obtener la ubicación actual
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium, // Define la precisión deseada de la ubicación
                        Timeout = TimeSpan.FromSeconds(30) // Establece un tiempo de espera de 30 segundos
                    });
                }

                // Si se obtiene una ubicación, procede a actualizar el mapa
                if (location != null)
                {
                    // Crea una nueva posición con las coordenadas obtenidas
                    var position = new Position(location.Latitude, location.Longitude);

                    // Mueve el mapa a la región centrada en la ubicación obtenida con un radio de 1 milla
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1)));

                    // Crea un nuevo pin (marcador) para mostrar en el mapa
                    var pin = new Pin
                    {
                        Type = PinType.Place, // Define el tipo de pin
                        Position = position, // Establece la posición del pin
                        Label = "Tu ubicación Angelo", // Establece la etiqueta del pin
                        Address = "Aquí estás" // Establece la dirección del pin
                    };

                    // Agrega el pin al mapa
                    map.Pins.Add(pin);
                }
                else
                {
                    // Si no se puede obtener la ubicación, muestra una alerta de error
                    await DisplayAlert("Error", "No se pudo obtener la ubicación.", "OK");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Muestra una alerta si la funcionalidad de ubicación no está soportada en el dispositivo
                await DisplayAlert("Error", "La funcionalidad de ubicación no está soportada en este dispositivo.", "OK");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Muestra una alerta si la funcionalidad de ubicación no está habilitada
                await DisplayAlert("Error", "La funcionalidad de ubicación no está habilitada.", "OK");
            }
            catch (PermissionException pEx)
            {
                // Muestra una alerta si los permisos de ubicación fueron denegados
                await DisplayAlert("Error", "Permisos de ubicación denegados.", "OK");
            }
            catch (Exception ex)
            {
                // Muestra una alerta para cualquier otro tipo de error
                await DisplayAlert("Error", "No se pudo obtener la ubicación.", "OK");
            }
        }
    }
}