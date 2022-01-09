using Microsoft.Extensions.DependencyInjection;
using Snow.Aba.FeatureManagement.Blazor;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.TenantManagement;
using Volo.Abp.Threading;
using Volo.Abp.UI.Navigation;

namespace Snow.Aba.TenantManagement.Blazor
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbaFeatureManagementBlazorModule)
    )]
    public class AbaTenantManagementBlazorModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new();

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbaTenantManagementBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbaTenantManagementBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new TenantManagementBlazorMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(AbaTenantManagementBlazorModule).Assembly);
            });
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper
                    .ApplyEntityConfigurationToUi(
                        TenantManagementModuleExtensionConsts.ModuleName,
                        TenantManagementModuleExtensionConsts.EntityNames.Tenant,
                        createFormTypes: new[] { typeof(TenantCreateDto) },
                        editFormTypes: new[] { typeof(TenantUpdateDto) }
                    );
            });
        }
    }
}
