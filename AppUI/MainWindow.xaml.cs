using System.Windows;
using System;
using AppUI.Services.Interfaces;

namespace AppUI
{
    public partial class MainWindow : Window
    {
        private readonly IHttpClientService _httpClientService;

        // Constructor sin parámetros
        // Constructor sin parámetros
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(IHttpClientService httpClientService) : this()
        {
            InitializeComponent();
            _httpClientService = httpClientService;
            CallApi();
        }

        // Método para llamar a la API, asegúrate de llamar a este método de manera adecuada en tu UI.
        private async void CallApi()
        {
            try
            {
                var result = await _httpClientService.GetAsync<List<Persona>>("https://localhost:7231/directorio/personas");
                // Aquí manejarías el resultado, por ejemplo, mostrándolo en la UI.
            }
            catch (Exception ex)
            {
                // Manejar la excepción, por ejemplo, mostrando un mensaje al usuario.
            }
        }
    }
}
