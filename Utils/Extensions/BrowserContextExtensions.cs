using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightShowcase.Utils.Extensions
{
    public static class BrowserContextExtensions
    {
        public static async Task StartTracingAsync(this IBrowserContext browserContext)
        {
            await browserContext.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        public static async Task StopTracingAsync(this IBrowserContext browserContext, string pathToTrace)
        {
            await browserContext.Tracing.StopAsync(new()
            {
                Path = pathToTrace
            });
        }
    }
}
