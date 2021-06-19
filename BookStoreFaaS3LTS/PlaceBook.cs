using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace BookStoreFaaS3LTS
{
    public static class PlaceBook
    {
        public const string QUEUE_NOTIFICATION = "new-book-notifications";
        public const string STORE_TABLE = "Books";
		
        [FunctionName("PlaceBook")]
        public static async Task<IActionResult> Run(
            // exposes an api to the client so that it can send an instance
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] Book bookPlacement,

            // bind a storage table to allow add a row with the sent instance
            [Table(STORE_TABLE)] IAsyncCollector<Book> 
            books, // could use cosmos db etc.

            // queues a message for the notifier to take it and transmit in real time
            [Queue(QUEUE_NOTIFICATION)] IAsyncCollector<Book> 
            notifications,

             ILogger log)
        {
            log.LogInformation("** PlaceBook");

            // writes a row in Table Books
            await books.AddAsync(new Book {
                PartitionKey = "US",
                RowKey = Guid.NewGuid().ToString(),
                Author = bookPlacement.Author,
                Title = bookPlacement.Title,
            });

            log.LogInformation("** notifications.AddAsync(bookPlacement)");

            // will execute signalR function
            await notifications.AddAsync(bookPlacement);

            return new OkResult();
        }
    }
}
