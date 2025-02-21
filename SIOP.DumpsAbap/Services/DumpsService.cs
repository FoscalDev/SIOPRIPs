using Microsoft.Extensions.Configuration;
using SIOP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SIOP.DumpsAbap.Services
{
    public class DumpsService
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string _userApi;
        private readonly string _passApi;
        public DumpsService(HttpClient httpclient, IConfiguration configuration ) 
        {

            _httpClient = httpclient;
            _config = configuration;

            _userApi = _config.GetValue<string>("SIOP:Usuario");
            _passApi = _config.GetValue<string>("SIOP:Contrasena");

        }
        public async Task<bool> ObtenerDumps()
        {
            try
            {
                var tokenapi = await GetTocken();
                BusquedaDumpsDTO busqueda = new BusquedaDumpsDTO();
                busqueda.FechaInicial = DateTime.Now;
                busqueda.Horainicial = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                busqueda.FechaFinal = DateTime.Now;
                busqueda.HoraFinal = "235959";
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenapi);

                var ResponseHttp = await _httpClient.PostAsJsonAsync<BusquedaDumpsDTO>($"api/Monitoreo/GetDumps",busqueda);

                if (ResponseHttp.IsSuccessStatusCode) { 
                
                var Data = await ResponseHttp.Content.ReadFromJsonAsync<List<DumpsSAPDTO>>();

                    foreach (var item in Data)
                    {
                        Serilog.Log.Error($"(Dumps SAP) Usuario {item.E2E_USER} Error: {item.Field1} Programa:{item.Field4} Tx:{item.Field8} Componente:{item.Field7} Texto:{item.Field9}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("(SIOP) Error al ejecutar función para obtener dumps de SAP: " + ex);
                throw;
            }
        }

        private async Task<string> GetTocken()
        {
            try
            {
                AutenticacionDTO user = new AutenticacionDTO();

                user.UserName = _userApi;
                user.Password = _passApi;


                var response = await _httpClient.PostAsJsonAsync($"/siop/api/Autenticacion/GenerarToken", user);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                }
                var resul = response.Content.ReadFromJsonAsync<TokenDTO>().Result;

                return resul.token;
            }
            catch (Exception ex)
            {

                return null;
            }


        }
    }
}
