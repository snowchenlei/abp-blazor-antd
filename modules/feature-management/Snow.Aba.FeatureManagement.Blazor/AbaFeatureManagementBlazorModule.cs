using Snow.Aba.AspNetCore.Components.Web.Theming;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Features;
using Volo.Abp.Modularity;

namespace Snow.Aba.FeatureManagement.Blazor
{
    [DependsOn(
        typeof(AbaAspNetCoreComponentsWebThemingModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpFeaturesModule)
    )]
    public class AbaFeatureManagementBlazorModule : AbpModule
    {

    }
}