using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Volo.Abp.UI.Navigation;

namespace Snow.Aba.AspNetCore.Components.WebAssembly.BasicTheme.Themes.Basic
{
    public partial class LoginDisplay : IDisposable
    {
        [Inject]
        protected IMenuManager MenuManager { get; set; }

        //[Inject] protected IProfileAppService ProfileAppService { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected AccountInfoModal AccountInfoModal;

        protected ApplicationMenu Menu { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetUserInformations();

            Menu = await MenuManager.GetAsync(StandardMenus.User);

            Navigation.LocationChanged += OnLocationChanged;

            AuthenticationStateProvider.AuthenticationStateChanged += async (task) =>
            {
                Menu = await MenuManager.GetAsync(StandardMenus.User);
                await InvokeAsync(StateHasChanged);
            };
        }

        protected virtual void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Navigation.LocationChanged -= OnLocationChanged;
        }

        protected async Task GetUserInformations()
        {
            // TODO:获取用户信息
            if (CurrentUser.IsAuthenticated)
            {
                //var user = await ProfileAppService.GetAsync();
                //AccountInfoModal = ObjectMapper.Map<ProfileDto, AccountInfoModal>(user);
            }
            else
            {
                AccountInfoModal = new AccountInfoModal() { UserName = "ad1" };
            }
        }
    }

    public class AccountInfoModal
    {
        public string UserName { get; set; }
        public string Logo { get; set; }
    }
}
