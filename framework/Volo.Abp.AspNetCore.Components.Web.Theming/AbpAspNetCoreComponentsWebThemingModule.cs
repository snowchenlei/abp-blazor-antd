using Volo.Abp.AntdBlazorUI;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Volo.Abp.AspNetCore.Components.Web.Theming
{
    [DependsOn(
        typeof(AbpAntdBlazorUIModule),
        typeof(AbpUiNavigationModule)
        )]
    public class AbpAspNetCoreComponentsWebThemingModule : AbpModule
    {
        
    }
}