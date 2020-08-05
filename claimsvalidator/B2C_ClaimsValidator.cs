using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


namespace b2c_custompolicy
{
    public static class B2C_ClaimsValidator
    {
        [FunctionName("B2C_ClaimsValidator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous , "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            List<userClaims> ulist = new List<userClaims> { 
                new userClaims() { email = "user1-b2c-webappaaa@mailinator.com", promocode = "289372974" },
                new userClaims() { email = "jjsamoa@romervegmail.onmicrosoft.com", promocode = "445093458" },
                new userClaims() { email = "user2-b2c-webappaaa@mailinator.com", promocode = "3453456456" }
            };

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonSerializer.Deserialize<userClaims>(requestBody);

            dynamic valuser = null;
            foreach (var user in ulist)
            {
                if ( user.email == data.email )
                    valuser = user;
            }

            return new OkObjectResult(valuser);
        }
    }

    public class userClaims {
        public string email { get; set; }
        public string promocode { get; set; }
    }
}
