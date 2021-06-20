using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

// https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-table-input?tabs=csharp

namespace BookStoreFaaS
{
    public static class StoredBooks
    {
        [Function("StoredBooks")]
        public static async Task<List<Book>> Run(

           [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)

           //- [Table("Books")] CloudTable cloudTable)
        {
            // by sample
            //- var filter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "US");
            // query
            //- var rangeQuery = new TableQuery<Book>().Where(filter);
            // execute query
            //- var q = await cloudTable.ExecuteQuerySegmentedAsync(rangeQuery, null);
            //- scape until use real data
            var q = new List<Book> {
                new Book { Author = "Harvey Triana", Title = "Blazor Spread", RowKey = Guid.NewGuid().ToString() }
            };
            await Task.Delay(300);

            // build response
            var ls = new List<Book>();
            foreach (Book i in q) {
                ls.Add(new Book {
                    PartitionKey = i.PartitionKey,
                    RowKey = i.RowKey,
                    Timestamp = i.Timestamp,
                    Author = i.Author,
                    Title = i.Title,
                });
            }
            return ls;
        }
    }
}
