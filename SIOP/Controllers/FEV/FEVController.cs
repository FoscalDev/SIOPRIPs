using SIOP.FEVRIPS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace SIOP.Controllers.FEV
{
    [Route("api/[controller]")]
    [ApiController]
    public class FEVController : ControllerBase
    {
        private readonly ServicesFEV _service;
        private readonly IConfiguration Config;

        public FEVController(ServicesFEV service, IConfiguration config)
        {
            _service = service;
            Config = config;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getXML/{id}/{einri}")]
        public async Task<ActionResult<string>> getXML(int id, int einri)
        {

                string
                    base64User = $"{Config.GetValue<string>($"MyEnvoice:{einri}:Usuario")}",
                    base64Password = $"{Config.GetValue<string>($"MyEnvoice:{einri}:Password")}",
                    User = Encoding.UTF8.GetString(Convert.FromBase64String(base64User)),
                    Password = Encoding.UTF8.GetString(Convert.FromBase64String(base64Password));

                return await _service.getXml(id, einri, User, Password);

        }
    }
}
