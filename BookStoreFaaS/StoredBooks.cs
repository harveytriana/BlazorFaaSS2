using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace BookStoreFaaS
{
    public static class StoredBooks
    {
        [Function("StoredBooks")]
        public static List<Book> Run(
           [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
           [TableInput("Books")] List<Book> cloudTable)
        {
            return cloudTable;
        }
    }
}
