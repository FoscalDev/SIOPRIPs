using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIOP.Model.DTO.DockerRips;
using SIOP.Model.DTO.RIPs;
using SIOP.Services.DockerRips;

namespace SIOP.Controllers.EndPointDocker
{
    [Route("api/[controller]")]
    [ApiController]

    public class RIPsController : ControllerBase
    {     
        private readonly DockerRips _Services;
        

        public RIPsController(DockerRips Services)
        {
            _Services = Services;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CargarFevRips")]
        public async Task<ActionResult<RespuestaCargueFevRipsDTO>> CargarFevRips(CargueFevRipsDTO Param)
        {
            try {             
                var paramCargueRips = new CargueFevRipsDTO
                {
                    rips = Param.rips,
                    xmlFevFile = Param.xmlFevFile,
                    einri = Param.einri
                };
                return await _Services.CargarFevRipsJSON(paramCargueRips);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error($"(SIOP - RIPs) {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ConsultarCUV")]
        public async Task<ActionResult<RespuestaConsultarCUVDTO>> ConsultarCUV(CargueFevRipsDTO Param)
        {
            try
            {
                var paramCargueRips = new CargueFevRipsDTO
                {
                    rips = Param.rips,
                    xmlFevFile = Param.xmlFevFile,
                    einri = Param.einri
                };
                return await _Services.ConsultarCUV(paramCargueRips);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error($"(SIOP - RIPs) {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
