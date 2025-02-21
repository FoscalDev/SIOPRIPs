using Microsoft.AspNetCore.Mvc;
using SIOP.Model.DTO;
using SIOP.Services.Email;

namespace SIOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly EmailServices _services;
        public EmailController(EmailServices services)
        {

            _services = services;
        }

        [HttpPost]
        [Route("sendemail")]
        public async Task<ActionResult<bool>> sendemail(EmailDTO email)
        {

            return await _services.SendEmail(email);
        }
    }
}
