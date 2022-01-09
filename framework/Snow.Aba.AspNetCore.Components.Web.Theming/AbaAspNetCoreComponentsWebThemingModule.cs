using Snow.Aba.AntdBlazorUI;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Snow.Aba.AspNetCore.Components.Web.Theming
{
    [DependsOn(
        typeof(AabAntdBlazorUIModule),
        typeof(AbpUiNavigationModule)
        )]
    public class AbaAspNetCoreComponentsWebThemingModule : AbpModule
    {

    }
}