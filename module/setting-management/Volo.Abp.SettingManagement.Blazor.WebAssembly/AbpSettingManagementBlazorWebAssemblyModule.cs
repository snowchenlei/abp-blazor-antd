using Snow.Aba.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Snow.Aba.SettingManagement.Blazor.WebAssembly
{
    [DependsOn(
        typeof(AbaSettingManagementBlazorModule),
        typeof(AbaAspNetCoreComponentsWebAssemblyThemingModule),
        typeof(AbpSettingManagementHttpApiClientModule)
    )]
    public class AbpSettingManagementBlazorWebAssemblyModule : AbpModule
    {
    }
}
