using Snow.Aba.FeatureManagement.Blazor.WebAssembly;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Snow.Aba.TenantManagement.Blazor.WebAssembly
{
    [DependsOn(
        typeof(AbaTenantManagementBlazorModule),
        typeof(AbaFeatureManagementBlazorWebAssemblyModule),
        typeof(AbpTenantManagementHttpApiClientModule)
        )]
    public class AbaTenantManagementBlazorWebAssemblyModule : AbpModule
    {

    }
}
