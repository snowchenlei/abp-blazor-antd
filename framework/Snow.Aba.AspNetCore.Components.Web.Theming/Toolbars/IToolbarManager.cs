using System.Threading.Tasks;

namespace Snow.Aba.AspNetCore.Components.Web.Theming.Toolbars
{
    public interface IToolbarManager
    {
        Task<Toolbar> GetAsync(string name);
    }
}
