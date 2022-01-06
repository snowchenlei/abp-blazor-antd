using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Volo.Abp;

namespace Snow.Aba.AspNetCore.Components.WebAssembly
{
    public class AbpWebAssemblyApplicationCreationOptions
    {
        public WebAssemblyHostBuilder HostBuilder { get; }

        public AbpApplicationCreationOptions ApplicationCreationOptions { get; }

        public AbpWebAssemblyApplicationCreationOptions(
            WebAssemblyHostBuilder hostBuilder,
            AbpApplicationCreationOptions applicationCreationOptions)
        {
            HostBuilder = hostBuilder;
            ApplicationCreationOptions = applicationCreationOptions;
        }
    }
}
