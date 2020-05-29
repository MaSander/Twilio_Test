using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Twilio_aplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {

            const string accountSid = "ACCOUNT SID";
            const string authToken = "TOKEN";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                from: new Twilio.Types.PhoneNumber("whatsapp:+XXXXXX"),
                body: "Hello, there!",
                to: new Twilio.Types.PhoneNumber("whatsapp:+XXXXXXX")
            );

            return message.Sid;
        }

    }
}
