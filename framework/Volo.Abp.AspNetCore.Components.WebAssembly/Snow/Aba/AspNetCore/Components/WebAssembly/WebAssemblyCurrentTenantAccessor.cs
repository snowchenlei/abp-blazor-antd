using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Snow.Aba.AspNetCore.Components.WebAssembly
{
    [Dependency(ReplaceServices = true)]
    public class WebAssemblyCurrentTenantAccessor : ICurrentTenantAccessor, ISingletonDependency
    {
        public BasicTenantInfo Current { get; set; }
    }
}
