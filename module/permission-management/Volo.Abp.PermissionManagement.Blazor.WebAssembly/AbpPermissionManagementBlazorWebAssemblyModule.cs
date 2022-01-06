using Snow.Aba.AspNetCore.Components.WebAssembly.Theming;
using Snow.Aba.PermissionManagement.Blazor;
using Volo.Abp.Modularity;

namespace Volo.Abp.PermissionManagement.Blazor.WebAssembly
{
    [DependsOn(
        typeof(AbaPermissionManagementBlazorModule),
        typeof(AbaAspNetCoreComponentsWebAssemblyThemingModule),
        typeof(AbpPermissionManagementHttpApiClientModule)
    )]
    public class AbpPermissionManagementBlazorWebAssemblyModule : AbpModule
    {
    }
}
