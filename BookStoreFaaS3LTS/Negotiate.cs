// ======================================
// BlazorSpread.net
// ======================================
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace BookStoreFaaS3LTS
{
    public static class Negotiate
    {
        public const string HUBNAME = "notifications";

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = HUBNAME)] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
    }
}
