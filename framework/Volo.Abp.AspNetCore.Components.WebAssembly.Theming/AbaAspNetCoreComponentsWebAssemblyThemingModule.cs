using Snow.Aba.AspNetCore.Components.Web.Theming;
using Volo.Abp.Modularity;

namespace Snow.Aba.AspNetCore.Components.WebAssembly.Theming
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsWebThemingModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyModule)
    )]
    public class AbaAspNetCoreComponentsWebAssemblyThemingModule : AbpModule
    {

    }
}
