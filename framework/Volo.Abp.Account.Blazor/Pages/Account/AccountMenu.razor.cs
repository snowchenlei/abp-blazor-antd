using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Volo.Abp.Account.Blazor.Pages.Account
{
    public partial class AccountMenu
    {
        [Inject] protected IProfileAppService ProfileAppService { get; set; }
        [Parameter] public EventCallback<MenuItem> OnItemSelected { get; set; }
        //[Parameter]
        public IEnumerable<AvatarMenuItem> MenuItems { get; set; }
        

        protected AccountInfoModal AccountInfoModal;
        protected ProfileDto User;
        protected override async Task OnInitializedAsync()
        {
            this.MenuItems = new AvatarMenuItem[]
            {
                new AvatarMenuItem { Key = "center", IconType = "user", Option = "个人中心", Url = "/my-profile/info"},
                new AvatarMenuItem { Key = "setting", IconType = "setting", Option = "设置"},
                new AvatarMenuItem { IsDivider = true },
                new AvatarMenuItem { Key = "logout", IconType = "logout", Option = "退出", Url="/authentication/logout"}
            };
            await GetUserInformations();
        }

        protected async Task GetUserInformations()
        {
            
            // TODO:获取用户信息
            if (CurrentUser.IsAuthenticated)
            {
                var user = await ProfileAppService.GetAsync();
                AccountInfoModal = ObjectMapper.Map<ProfileDto, AccountInfoModal>(user);
            }
            else
            {
                AccountInfoModal = new AccountInfoModal() { UserName = "ad1" };
            }
        }
    }

    public class AvatarMenuItem
    {
        public string Key { get; set; }
        public string IconType { get; set; }
        public string IconTheme { get; set; } = "outline";
        public string Option { get; set; }
        public string Url { get; set; }
        public bool IsDivider { get; set; }
    }
    public class AccountInfoModal
    {
        public string UserName { get; set; }
        public string Logo { get; set; }
    }
}
