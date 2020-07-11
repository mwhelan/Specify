using System;
using Microsoft.Extensions.Hosting;

namespace ApiTemplate.Api.Common
{
    public static class HostingEnvironmentExtensions
    {
        public static bool IsTest(this IHostEnvironment environment)
        {
            return environment.IsEnvironment("Test") || Environment.GetEnvironmentVariable("NCrunch") == "1";   //  || environment.IsEnvironment("TFS");
        }
    }
}
