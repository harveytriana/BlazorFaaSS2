// ======================================
// BlazorSpread.net
// ======================================
using Microsoft.Extensions.Hosting;

namespace BookStoreFaaS
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}