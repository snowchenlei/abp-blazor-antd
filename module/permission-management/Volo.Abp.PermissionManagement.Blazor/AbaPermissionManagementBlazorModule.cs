using Snow.Aba.AspNetCore.Components.Web.Theming;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Snow.Aba.PermissionManagement.Blazor
{
    [DependsOn(
        typeof(AbaAspNetCoreComponentsWebThemingModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpPermissionManagementApplicationContractsModule)
        )]
    public class AbaPermissionManagementBlazorModule : AbpModule
    {

    }
}
