using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BookStoreFaaS
{
    public static class PlacedBookNotification
    {
        [Function("PlacedBookNotification")]
        [SignalROutput(HubName = Negotiate.HUBNAME)]
        public static SignalRMessage Run(
            // received a queue message for execute the transmission
            [QueueTrigger(PlaceBook.QUEUE_NOTIFICATION)] Book book,
			// logger, others	
            FunctionContext executionContext)
        {
            var log = executionContext.GetLogger("PlacedBookNotification");
            log.LogInformation("** SignalR Notification");

            // all clients receive the new instance across event placedBook
            return new SignalRMessage {
                Target = "placedBook",
                Arguments = new[] { book }
            };
        }
    }

    public class SignalRMessage
    {
        public string Target { get; set; }
        public Book[] Arguments { get; set; }
    }
}
