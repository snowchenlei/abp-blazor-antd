using Snow.Aba.Identity.Blazor;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Blazor.WebAssembly;

namespace Snow.Aba.Identity.Blazor.WebAssembly
{
    [DependsOn(
        typeof(AbaIdentityBlazorModule),
        typeof(AbpPermissionManagementBlazorWebAssemblyModule),
        typeof(AbpIdentityHttpApiClientModule)
    )]
    public class AbaIdentityBlazorWebAssemblyModule : AbpModule
    {
    }
}
