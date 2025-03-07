using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SIOP.Model.DTO.DockerRips;
using SIOP.Model.DTO.RIPs;

namespace SIOP.Services.DockerRips
{
    public class DockerRips
    {
 
        private readonly IConfiguration Config;
        private readonly HttpClient _httpclient;

        public DockerRips(IConfiguration config)
        {
            Config = config;
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true,
                MaxRequestContentBufferSize = 2147483647  // 50 MB
            };

            _httpclient = new HttpClient(clientHandler)
            {

                Timeout = TimeSpan.FromMinutes(30) // Configurar timeout si es necesario
            };
        }

        public async Task<string> AuthLoginSISPRO(authLoginSISPRODTO Param)
        {
            string param = JsonConvert.SerializeObject(Param),
                fullUrl = $"{Config.GetValue<string>("EndPointDocker:Url")}api/Auth/LoginSISPRO";

            HttpClientHandler clientHandler = new();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpRequestMessage Request = new(HttpMethod.Post, fullUrl);
            HttpContent Content = new StringContent(param, Encoding.UTF8, "application/json");
            Request.Content = Content;

            HttpClient Client = new(clientHandler);
            HttpResponseMessage Response = await Client.SendAsync(Request);

            ResultLoginSISPRODTO Result = await Response.Content.ReadFromJsonAsync<ResultLoginSISPRODTO>();
            if (Response.IsSuccessStatusCode)
            {
                return Result!.token;
            }
            else
            {
                Serilog.Log.Error($"(SIOP-RIPS) Error autenticación sispro usuario {Param.persona} {Result!.errors[0]}");
                throw new Exception(Result!.errors[0]);
            }
        }

        public async Task<RespuestaCargueFevRipsDTO> CargarFevRipsJSON(CargueFevRipsDTO Json)
        {
            string
                endPoint = Json!.rips!.tipoNota == "NC" ? "CargarNC" : Json!.rips!.tipoNota == "NA" ? "CargarNotaAjuste" : "CargarFevRips",
                fullUrl = $"{Config.GetValue<string>("EndPointDocker:Url")}api/PaquetesFevRips/{endPoint}",
                base64Tipo = Config.GetValue<string>($"EndPointDocker:{Json.einri}:Tipo")!,
                base64Numero = Config.GetValue<string>($"EndPointDocker:{Json.einri}:Numero")!,
                base64Clave = Config.GetValue<string>($"EndPointDocker:{Json.einri}:Clave")!,
                base64Nit = Config.GetValue<string>($"EndPointDocker:{Json.einri}:Nit")!,
                Tipo = Encoding.UTF8.GetString(Convert.FromBase64String(base64Tipo)),
                Numero = Encoding.UTF8.GetString(Convert.FromBase64String(base64Numero)),
                Clave = Encoding.UTF8.GetString(Convert.FromBase64String(base64Clave)),
                Nit = Encoding.UTF8.GetString(Convert.FromBase64String(base64Nit));

            var token = await AuthLoginSISPRO(
                new authLoginSISPRODTO
                {
                    persona =
                        {
                            identificacion =
                            {
                                tipo = Tipo,
                                numero = Numero,
                            }
                        },
                    clave = Clave,
                    nit = Nit
                }
                );
            var NewJson = new CargueFevRipsDTO
            {
                rips = Json.rips,
                xmlFevFile = Json.xmlFevFile
            };

            string param = JsonConvert.SerializeObject(NewJson, Formatting.Indented);

            // Configuración de la solicitud HTTP
            HttpRequestMessage Request = new(HttpMethod.Post, fullUrl);
            HttpContent Content = new StringContent(param, Encoding.UTF8, "application/json");
            Request.Content = Content;

            _httpclient.DefaultRequestHeaders.Clear();
            _httpclient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
          

            // Envío de la solicitud
            HttpResponseMessage Response = await _httpclient.SendAsync(Request);

            // Manejo de la respuesta
            if (Response.IsSuccessStatusCode)
            {
            
                var respuesta = await Response.Content.ReadFromJsonAsync<RespuestaCargueFevRipsDTO>();
                return respuesta; ;
            }
            else
            {
                string errorContent = await Response.Content.ReadAsStringAsync();
                //throw new Exception($"Error en la solicitud: {Response.StatusCode}. Detalles: {errorContent}");
                // throw new Exception(JsonConvert.SerializeObject(Result));
                throw new Exception(errorContent);
            }
        }

        public async Task<RespuestaConsultarCUVDTO> ConsultarCUV(CargueFevRipsDTO Json)
        {
            string fullUrl = $"{Config.GetValue<string>("EndPointDocker:Url")}api/ConsultasFevRips/ConsultarCUV",
                base64Tipo = Config.GetValue<string>($"EndPointDocker:{Json.einri}:Tipo")!,
                base64Numero = Config.GetValue<string>($"EndPointDocker:{Json.einri}:Numero")!,
                base64Clave = Config.GetValue<string>($"EndPointDocker:{Json.einri}:Clave")!,
                base64Nit = Config.GetValue<string>($"EndPointDocker:{Json.einri}:Nit")!,
                Tipo = Encoding.UTF8.GetString(Convert.FromBase64String(base64Tipo)),
                Numero = Encoding.UTF8.GetString(Convert.FromBase64String(base64Numero)),
                Clave = Encoding.UTF8.GetString(Convert.FromBase64String(base64Clave)),
                Nit = Encoding.UTF8.GetString(Convert.FromBase64String(base64Nit)),
                pattern = @"\[(.*?)\]";

            var token = await AuthLoginSISPRO(
                new authLoginSISPRODTO
                {
                    persona =
                        {
                            identificacion =
                            {
                                tipo = Tipo,
                                numero = Numero,
                            }
                        },
                    clave = Clave,
                    nit = Nit
                }
                );
            var NewJson = new CargueFevRipsDTO
            {
                rips = Json.rips,
                xmlFevFile = Json.xmlFevFile
            };

            string param = JsonConvert.SerializeObject(NewJson, Formatting.Indented);

            HttpRequestMessage Request = new(HttpMethod.Post, fullUrl);
            HttpContent Content = new StringContent(param, Encoding.UTF8, "application/json");
            Request.Content = Content;

            _httpclient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            // HttpResponseMessage Response = await _httpclient.SendAsync(Request);

            HttpResponseMessage Response = await _httpclient.SendAsync(Request, HttpCompletionOption.ResponseHeadersRead);
            _httpclient.DefaultRequestHeaders.ExpectContinue = false;
            var respuesta = await Response.Content.ReadAsStringAsync();
            RespuestaConsultarCUVDTO Result = await Response.Content.ReadFromJsonAsync<RespuestaConsultarCUVDTO>();
            if (Response.IsSuccessStatusCode)
            {
                Match match = Regex.Match(Result?.resultadosValidacion[0].observaciones!, pattern);
                var newRespuestaConsultarCUVDTO = new RespuestaConsultarCUVDTO
                {
                    procesoId = Result!.procesoId,
                    esValido = Result.esValido,
                    codigoUnicoValidacion = match.Groups[1].Value,
                    fechaValidacion = Result.fechaValidacion,
                    numDocumentoIdObligado = Result.numDocumentoIdObligado,
                    numeroDocumento = Result.numeroDocumento,
                    fechaEmision = Result.fechaEmision,
                    totalFactura = Result.totalFactura,
                    cantidadUsuarios = Result.cantidadUsuarios,
                    cantidadAtenciones = Result.cantidadAtenciones,
                    totalValorServicios = Result.totalValorServicios,
                    identificacionAdquiriente = Result.identificacionAdquiriente,
                    codigoPrestador = Result.codigoPrestador,
                    modalidadPago = Result.modalidadPago,
                    numDocumentoReferenciado = Result.numDocumentoReferenciado,
                    urlJson = Result.urlJson,
                    urlXml = Result.urlXml,
                    jsonFile = Result.jsonFile,
                    xmlFileBase64 = Result.xmlFileBase64
                };
                return newRespuestaConsultarCUVDTO!;
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(Result));
            }
        }


        public async Task<string> Version() {

            try
            {
                string fullUrl = $"{Config.GetValue<string>("EndPointDocker:Url")}api/TestApi/Index";

                HttpClientHandler clientHandler = new();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpRequestMessage Request = new(HttpMethod.Get, fullUrl);

                HttpClient Client = new(clientHandler);
                HttpResponseMessage Response = await Client.SendAsync(Request);

                var Result = await Response.Content.ReadAsStringAsync();

                return Result;
            }
            catch (Exception ex)
            {

                return ($"sin respuesta error: { ex.Message } exception: {ex.InnerException.Message}");
            }
          
     

        }

    
    }
}
