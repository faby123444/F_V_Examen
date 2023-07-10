using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using F_V_Examen.Models;

namespace F_V_Examen
{
    public partial class CentroCostosPage : ContentPage
    {
        private const string BaseUrl = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/";
        private HttpClient _httpClient;

        public List<CentroCostos_f> CentroCostosList { get; set; }

        public CentroCostosPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            CentroCostosList = new List<CentroCostos_f>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshData();
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            await RefreshData();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            var codigo = "your_code_here";
            var descripcion = txtDescripcion.Text;

            var url = $"{BaseUrl}CentroCostosInsert?codigocentrocostos={codigo}&descripcioncentrocostos={descripcion}";
            await SendRequest(url, HttpMethod.Post);

            await RefreshData();
        }

        private async void EditarButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var centroCostos = (CentroCostos_f)button.BindingContext;

            var id = centroCostos.Codigo;
            var descripcion = txtDescripcion.Text;

            var url = $"{BaseUrl}CentroCostosUpdate?codigocentrocostos={id}&descripcioncentrocostos={descripcion}";
            await SendRequest(url, HttpMethod.Put);

            await RefreshData();
        }

        private async void EliminarButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var centroCostos = (CentroCostos_f)button.BindingContext;

            var id = centroCostos.Codigo;
            var descripcion = centroCostos.NombreCentroCostos;

            var url = $"{BaseUrl}CentroCostosDelete?codigocentrocostos={id}&descripcioncentrocostos={descripcion}";
            await SendRequest(url, HttpMethod.Delete);

            await RefreshData();
        }

        private async Task RefreshData()
        {
            var descripcion = txtDescripcion.Text;

            var url = $"{BaseUrl}CentroCostosSearch?descripcioncentrocostos={descripcion}";
            var response = await _httpClient.GetStringAsync(url);
            CentroCostosList = JsonConvert.DeserializeObject<List<CentroCostos_f>>(response);

            lstCentroCostos.ItemsSource = CentroCostosList;
        }

        private async Task SendRequest(string url, HttpMethod method)
        {
            var request = new HttpRequestMessage(method, url);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}

