using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace BookStoreFaaS
{
    public static class Negotiate
    {
        public const string HUBNAME = "notifications";

        [Function("Negotiate")]
        public static SignalRConnectionInfo Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        [SignalRConnectionInfoInput(HubName = HUBNAME)] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        public class SignalRConnectionInfo
        {
            public string Url { get; set; }
            public string AccessToken { get; set; }
        }

        // Another approach, Anthony Chu´s sample, not declare class SignalRConnectionInfo
        // and return HttpResponseData isntead of SignalRConnectionInfo, needs using System.Net
        //[Function("Negotiate")]
        //public static HttpResponseData Run(
        //    //
        //    [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        //    //
        //    [SignalRConnectionInfoInput(HubName = HUBNAME)] string connectionInfo)
        //{
        //    var response = req.CreateResponse(HttpStatusCode.OK);
        //    response.Headers.Add("Content-Type", "application/json");
        //    response.WriteString(connectionInfo);
        //    return response;
        //}
    }
}