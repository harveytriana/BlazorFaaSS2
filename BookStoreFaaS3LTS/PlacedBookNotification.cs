using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BookStoreFaaS3LTS
{
    public static class PlacedBookNotifications
    {
        [FunctionName("PlacedBookNotification")]
        public static async Task Run(
            // waits a queue message for execute the transmission
            [QueueTrigger(PlaceBook.QUEUE_NOTIFICATION)] Book bookPlacement,
            // broadcast the instance to clients who have subscribed to the signal event in real time
            [SignalR(HubName = Negotiate.HUBNAME)] IAsyncCollector<SignalRMessage> signalRMessages,
			// logger
            ILogger log)
        {
            log.LogInformation("** PlacedBookNotification");

            // clients have subscribed to event with name target´s value and will receive the instance
            await signalRMessages.AddAsync(new SignalRMessage {
                Target = "placedBook",
                Arguments = new[] { bookPlacement }
            });
        }
    }
}
