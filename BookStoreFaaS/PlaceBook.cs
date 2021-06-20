using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Threading.Tasks;

namespace BookStoreFaaS
{
    // add entity book to table store and create a queue for signalr notification
    public static class PlaceBook
    {
        public const string QUEUE_NOTIFICATION = "new-book-notifications";
        public const string STORE_TABLE = "Books";

        [Function("PlaceBook")]
        public static async Task<PlaceBookAndNotify> Run(
            // endpoint to client send data
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            var bookPlacement = await req.ReadFromJsonAsync<Book>();
            var book = new Book {
                PartitionKey = "US",
                RowKey = Guid.NewGuid().ToString(),
                Author = bookPlacement.Author,
                Title = bookPlacement.Title,
            };
            return new PlaceBookAndNotify() {
                QueueBook = book,
                EntityBook = book
            };
        }
    }

    // Multiple output bindings
    public class PlaceBookAndNotify
    {
        [QueueOutput(PlaceBook.QUEUE_NOTIFICATION)]
        public Book QueueBook { get; set; }

        [TableOutput(PlaceBook.STORE_TABLE)]
        public Book EntityBook { get; set; }
    }
}
