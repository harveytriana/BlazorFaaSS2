// ======================================
// BlazorSpread.net
// ======================================
using Microsoft.Azure.Cosmos.Table;

namespace BookStoreFaaS3LTS
{
    public class Book : TableEntity
    {
        public string Author { get; set; }
        public string Title { get; set; }
    }
}
