using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Volo.Abp.UI.Navigation;

namespace Snow.Aba.AspNetCore.Components.Web.BasicTheme.Themes.Basic
{
    public partial class NavMenu: ComponentBase
    {
        [Inject]
        protected IMenuManager MenuManager { get; set; }

        protected ApplicationMenu Menu { get; set; }

        [Parameter]
        public MenuTheme MenuTheme { get; set; } = MenuTheme.Dark;

        protected override async Task OnInitializedAsync()
        {
            Menu = await MenuManager.GetAsync(StandardMenus.Main);
        }
    }
}
