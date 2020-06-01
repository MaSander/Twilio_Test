using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Twilio_app
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string responseMessage = data["body"];

            string[] words = responseMessage.Split(" ");

            var messageReturn = "Ta bom";


            foreach (var item in words)
            {
                if(item == "sim")
                {
                    messageReturn = "Muito obrigado por nos responder, estaremos te encaminhando para um de nossos atententes";
                    break;                    
                }
            }

            const string accountSid = "ACCOUNT SID";
            const string authToken = "TOKEN";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                from: new Twilio.Types.PhoneNumber("whatsapp:+XXXXXX"),
                body: messageReturn,
                to: new Twilio.Types.PhoneNumber("whatsapp:+XXXXXXX")
            );

            return new OkObjectResult(message);
        }
    }
}
