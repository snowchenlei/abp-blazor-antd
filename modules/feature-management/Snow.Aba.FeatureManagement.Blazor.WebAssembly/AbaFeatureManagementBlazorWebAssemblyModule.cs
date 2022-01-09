using Snow.Aba.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;

namespace Snow.Aba.FeatureManagement.Blazor.WebAssembly
{
    [DependsOn(
        typeof(AbaFeatureManagementBlazorModule),
        typeof(AbaAspNetCoreComponentsWebAssemblyThemingModule),
        typeof(AbpFeatureManagementHttpApiClientModule)
    )]
    public class AbaFeatureManagementBlazorWebAssemblyModule : AbpModule
    {
    }
}
