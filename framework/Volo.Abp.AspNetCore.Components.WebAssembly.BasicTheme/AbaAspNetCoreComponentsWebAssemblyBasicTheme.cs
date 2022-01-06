using Snow.Aba.AspNetCore.Components.Web.BasicTheme;
using Snow.Aba.AspNetCore.Components.Web.Theming.Routing;
using Snow.Aba.AspNetCore.Components.Web.Theming.Toolbars;
using Snow.Aba.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Http.Client.IdentityModel.WebAssembly;
using Volo.Abp.Modularity;

namespace Snow.Aba.AspNetCore.Components.WebAssembly.BasicTheme
{
    [DependsOn(
        typeof(AbaAspNetCoreComponentsWebBasicThemeModule),
        typeof(AbaAspNetCoreComponentsWebAssemblyThemingModule),
        typeof(AbpHttpClientIdentityModelWebAssemblyModule)
        )]
    public class AbpAspNetCoreComponentsWebAssemblyBasicThemeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(AbpAspNetCoreComponentsWebAssemblyBasicThemeModule).Assembly);
            });

            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new BasicThemeToolbarContributor());
            });
        }
    }
}
