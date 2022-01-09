using Microsoft.Extensions.DependencyInjection;
using Snow.Aba.AspNetCore.Components.Web.Theming;
using Snow.Aba.AspNetCore.Components.Web.Theming.Routing;
using Snow.Aba.SettingManagement.Blazor.Menus;
using Snow.Aba.SettingManagement.Blazor.Settings;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.UI.Navigation;

namespace Snow.Aba.SettingManagement.Blazor
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbaAspNetCoreComponentsWebThemingModule),
        typeof(AbpSettingManagementApplicationContractsModule)
    )]
    public class AbaSettingManagementBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbaSettingManagementBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<SettingManagementBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new SettingManagementMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(AbaSettingManagementBlazorModule).Assembly);
            });

            Configure<SettingManagementComponentOptions>(options =>
            {
                options.Contributors.Add(new EmailingPageContributor());
            });
        }
    }
}
