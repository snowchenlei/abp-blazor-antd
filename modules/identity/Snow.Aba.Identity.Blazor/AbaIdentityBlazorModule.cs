using Microsoft.Extensions.DependencyInjection;
using Snow.Aba.AntdBlazorUI;
using Snow.Aba.AspNetCore.Components.Web.Theming.Routing;
using Snow.Aba.PermissionManagement.Blazor;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.Threading;
using Volo.Abp.UI.Navigation;

namespace Snow.Aba.Identity.Blazor
{
    [DependsOn(
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpAutoMapperModule),
        typeof(AbaPermissionManagementBlazorModule),
        typeof(AbaAntdBlazorUIModule)
        )]
    public class AbaIdentityBlazorModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbaIdentityBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbaIdentityBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new AbaIdentityWebMainMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(AbaIdentityBlazorModule).Assembly);
            });
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper
                    .ApplyEntityConfigurationToUi(
                        IdentityModuleExtensionConsts.ModuleName,
                        IdentityModuleExtensionConsts.EntityNames.Role,
                        createFormTypes: new[] { typeof(IdentityRoleCreateDto) },
                        editFormTypes: new[] { typeof(IdentityRoleUpdateDto) }
                    );

                ModuleExtensionConfigurationHelper
                    .ApplyEntityConfigurationToUi(
                        IdentityModuleExtensionConsts.ModuleName,
                        IdentityModuleExtensionConsts.EntityNames.User,
                        createFormTypes: new[] { typeof(IdentityUserCreateDto) },
                        editFormTypes: new[] { typeof(IdentityUserUpdateDto) }
                    );
            });
        }
    }
}
