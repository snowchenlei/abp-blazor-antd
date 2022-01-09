using Volo.Abp.Bundling;

namespace Snow.Aba.AspNetCore.Components.WebAssembly.BasicTheme
{
    public class BasicThemeBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {

        }

        public void AddStyles(BundleContext context)
        {
            // TOTO:Bundle Third styles/scripts
            context.Add("_content/Volo.Abp.AspNetCore.Components.Web.BasicTheme/libs/abp/css/theme.css");
            //context.Add("_content/AntDesign.ProLayout/css/ant-design-pro-layout-blazor.css");
        }
    }
}
