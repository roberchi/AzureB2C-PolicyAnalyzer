using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using AzureB2C.PolicyAnalyzer.Core.Models;
using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Formatting;

namespace Azure_B2C.PolicyAnalyzer.Jupyter.Extensions
{
    public class PolicyAnalyzerKernelExtension : IKernelExtension
    {
        public async Task OnLoadAsync(IKernel kernel)
        {
            Formatter<Policy>.Register((p, writer) =>
            {
                writer.Write(p.DrawItem());
            }, "text/html");

            Formatter<RelyingParty>.Register((p, writer) =>
            {
                writer.Write(p.DrawItem());
            }, "text/html");

            Formatter<UserJourney>.Register((p, writer) =>
            {
                writer.Write(p.DrawItem());
            }, "text/html");

            //if (kernel is KernelBase kernelBase)
            //{
            //    var clockCommand = new Command("#!clock", "Displays a clock showing the current or specified time.")
            //    {
            //        new Option<int>(new[]{"-o","--hour"},
            //                        "The position of the hour hand"),
            //        new Option<int>(new[]{"-m","--minute"},
            //                        "The position of the minute hand"),
            //        new Option<int>(new[]{"-s","--second"},
            //                        "The position of the second hand")
            //    };

            //    clockCommand.Handler = CommandHandler.Create(
            //        async (int hour, int minute, int second, KernelInvocationContext context) =>
            //        {
            //            await context.DisplayAsync(SvgClock.DrawSvgClock(hour, minute, second));
            //        });

            //    kernelBase.AddDirective(clockCommand);
            //}
            if (KernelInvocationContext.Current is { } context)
            {
                await context.DisplayAsync($"`{nameof(PolicyAnalyzerKernelExtension)}` is loaded. It adds visualizations for **policy models**`.", "text/markdown");
            }
        }
    }
}
