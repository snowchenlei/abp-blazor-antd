using System;
using System.Net.Http;
using IdentityModel;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Acme.BookStore.Blazor.Menus;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.SettingManagement.Blazor.WebAssembly;
using Volo.Abp.TenantManagement.Blazor.WebAssembly;
using Snow.Aba.AspNetCore.Components.Web.Theming.Routing;
using Snow.Aba.AspNetCore.Components.Web.Theming.Toolbars;
using Snow.Aba.AspNetCore.Components.WebAssembly.BasicTheme;
using Snow.Aba.Identity.Blazor.WebAssembly;
using Snow.Aba.AspNetCore.Components.Web.BasicTheme.Themes.Basic;

namespace Acme.BookStore.Blazor
{
    [DependsOn(
        typeof(AbpAutofacWebAssemblyModule),
        typeof(BookStoreHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyBasicThemeModule),
        typeof(AbaIdentityBlazorWebAssemblyModule)
    //typeof(AbpTenantManagementBlazorWebAssemblyModule),
    //typeof(AbpSettingManagementBlazorWebAssemblyModule)
    )]
    public class BookStoreBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
            var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new MyToolbarContributor());
            });

            ConfigureAuthentication(builder);
            ConfigureHttpClient(context, environment);
            ConfigureAntDesign(context);
            ConfigureRouter(context);
            ConfigureUI(builder);
            ConfigureMenu(context);
            ConfigureAutoMapper(context);
        }

        private void ConfigureRouter(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(BookStoreBlazorModule).Assembly;
            });
        }

        private void ConfigureMenu(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new BookStoreMenuContributor(context.Services.GetConfiguration()));
            });
        }

        private void ConfigureAntDesign(ServiceConfigurationContext context)
        {
            context.Services.AddAntDesign();
        }

        private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("AuthServer", options.ProviderOptions);
                options.UserOptions.RoleClaim = JwtClaimTypes.Role;
                options.ProviderOptions.DefaultScopes.Add("BookStore");
                options.ProviderOptions.DefaultScopes.Add("role");
                options.ProviderOptions.DefaultScopes.Add("email");
                options.ProviderOptions.DefaultScopes.Add("phone");
            });
        }

        private static void ConfigureUI(WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#ApplicationContainer");
        }

        private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
        {
            context.Services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri(environment.BaseAddress)
            });
        }

        private void ConfigureAutoMapper(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BookStoreBlazorModule>();
            });
        }
    }
}
