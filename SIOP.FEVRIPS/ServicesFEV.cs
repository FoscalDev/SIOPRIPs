using Newtonsoft.Json;
using ServiceReference;
using SIOP.DTO.FEV;

namespace SIOP.FEVRIPS
{
    public class ServicesFEV
    {
        public async Task<string> getToken(int einri, string usuario, string password)
        {
            FacturacionWebServiceClient client = new();
            var response = await client.autenticarAsync(usuario, password);
            var jsonResponse = JsonConvert.DeserializeObject<ResponseDTO>(response.@return);

            if (jsonResponse!.success)
            {
                return jsonResponse!.data!.salida!;
            }
            else
            {
                Serilog.Log.Error($"(SIOP-FEV) obtener token mye-invoice {jsonResponse.msg}");
                throw new System.Exception(jsonResponse!.msg);
            }
        }

        public async Task<string> getXml(int iddocumentoelectronico, int einri, string usuario, string password)
        {

            FacturacionWebServiceClient client = new();
            var token = await getToken(einri, usuario, password);
            try
            {

                var response = await client.getDocumentoElectronicoAsync(token, iddocumentoelectronico, false, false, false, true);
                var jsonResponse = JsonConvert.DeserializeObject<ResponseDTO>(response.@return);

                if (jsonResponse!.success)
                {
                    return jsonResponse!.data!.attachedDocument!;
                }
                else
                {
                    Serilog.Log.Error($"(SIOP-FEV) error obtener xml factura {jsonResponse.msg}");
                    throw new System.Exception(jsonResponse!.msg);
                }
            }
            catch (System.Exception ex)
            {
                Serilog.Log.Error($"(SIOP-FEV) error obtener xml factura {ex.Message}");
                throw;
            }
            finally
            {
                client?.cerrarSesionAsync(token);
            }
        }
    }
}
