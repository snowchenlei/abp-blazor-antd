using Snow.Aba.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Blazor;

namespace Snow.Aba.SettingManagement.Blazor.WebAssembly
{
    [DependsOn(
        typeof(AbpSettingManagementBlazorModule),
        typeof(AbaAspNetCoreComponentsWebAssemblyThemingModule),
        typeof(AbpSettingManagementHttpApiClientModule)
    )]
    public class AbpSettingManagementBlazorWebAssemblyModule : AbpModule
    {
    }
}
