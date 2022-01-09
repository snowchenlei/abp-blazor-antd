using Snow.Aba.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.WebAssembly;
using Volo.Abp.Modularity;

namespace Snow.Aba.AspNetCore.Components.WebAssembly.Theming
{
    [DependsOn(
        typeof(AbaAspNetCoreComponentsWebThemingModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyModule)
    )]
    public class AbaAspNetCoreComponentsWebAssemblyThemingModule : AbpModule
    {

    }
}
