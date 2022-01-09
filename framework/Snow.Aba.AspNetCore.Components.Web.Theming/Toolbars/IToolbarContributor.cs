using System.Threading.Tasks;

namespace Snow.Aba.AspNetCore.Components.Web.Theming.Toolbars
{
    public interface IToolbarContributor
    {
        Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
    }
}