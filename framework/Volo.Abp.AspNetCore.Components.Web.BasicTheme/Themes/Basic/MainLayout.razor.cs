using System;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Snow.Aba.AspNetCore.Components.Web.BasicTheme.Themes.Basic
{
    public partial class MainLayout : IDisposable
    {
        [Inject] private NavigationManager NavigationManager { get; set; }

        public string SiderStyle => $"overflow: hidden; padding-top: 0px";//$"overflow: hidden; padding-top: {(Layout == Layout.Mix ? HeaderHeight : 0)}px;";
        public string PrefixCls { get; } = "ant-abp";
        public string BaseClassName => $"{PrefixCls}-sider";

        [Parameter] public SiderTheme SiderTheme { get; set; } = SiderTheme.Dark;

        [Parameter] public int SiderWidth { get; set; } = 208;

        private bool Collapsed { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        private void ToggleCollapse()
        {
            Collapsed = !Collapsed;
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            Collapsed = false;
            InvokeAsync(StateHasChanged);
        }
    }
}
