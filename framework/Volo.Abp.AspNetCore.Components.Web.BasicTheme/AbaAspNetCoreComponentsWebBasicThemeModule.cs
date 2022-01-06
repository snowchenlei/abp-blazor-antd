using Snow.Aba.AspNetCore.Components.Web.Theming;
using Volo.Abp.Modularity;

namespace Snow.Aba.AspNetCore.Components.Web.BasicTheme
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsWebThemingModule)
        )]
    public class AbaAspNetCoreComponentsWebBasicThemeModule : AbpModule
    {

    }
}