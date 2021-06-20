﻿using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace BookStoreFaaS
{
    public static class Negotiate
    {
        public const string HUBNAME = "notifications";

        [Function("Negotiate")]
        public static HttpResponseData Run(
            //
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            //
            [SignalRConnectionInfoInput(HubName = HUBNAME)] string connectionInfo)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            response.WriteString(connectionInfo);
            return response;
        }
    }
}