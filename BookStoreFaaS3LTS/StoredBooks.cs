using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace BookStoreFaaS3LTS
{
    public static class StoredBooks
    {
        [FunctionName("StoredBooks")]
        public static async Task<List<Book>> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
           [Table("Books")] CloudTable cloudTable)
        {
            // query
            var rangeQuery = new TableQuery<Book>();
            // execute query
            var q = await cloudTable.ExecuteQuerySegmentedAsync(rangeQuery, null);
            // build response
            var ls = new List<Book>();
            foreach (Book i in q) {
                ls.Add(new Book {
                    RowKey = i.RowKey,
                    Author = i.Author,
                    Title = i.Title,
                });
            }
            return ls;
        }
    }
}
