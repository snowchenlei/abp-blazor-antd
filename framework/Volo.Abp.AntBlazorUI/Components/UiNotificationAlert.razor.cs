using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Snow.Aba.AntdBlazorUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Notifications;

namespace Snow.Aba.AntdBlazorUI.Components
{
    public partial class UiNotificationAlert : ComponentBase, IDisposable
    {
        [Parameter] public UiNotificationType UiNotificationType { get; set; }

        [Parameter] public string Message { get; set; }

        [Parameter] public string Title { get; set; }

        [Parameter] public UiNotificationOptions Options { get; set; }

        [Parameter] public EventCallback Okayed { get; set; }

        [Parameter] public EventCallback Closed { get; set; }

        [Inject] protected AntdBlazorUiNotificationService UiNotificationService { get; set; }

        [Inject] protected IStringLocalizerFactory StringLocalizerFactory { get; set; }

        [Inject]protected NotificationService NotificationService { get; set; }

        protected virtual NotificationType GetNotificationType(UiNotificationType notificationType)
        {
            return notificationType switch
            {
                UiNotificationType.Info => NotificationType.Info,
                UiNotificationType.Success => NotificationType.Success,
                UiNotificationType.Warning => NotificationType.Warning,
                UiNotificationType.Error => NotificationType.Error,
                _ => NotificationType.Info,
            };
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            UiNotificationService.NotificationReceived += OnNotificationReceived;
        }

        protected virtual async void OnNotificationReceived(object sender, UiNotificationEventArgs e)
        {
            UiNotificationType = e.NotificationType;
            Message = e.Message;
            Title = e.Title;
            Options = e.Options;

            var okButtonText = Options?.OkButtonText?.Localize(StringLocalizerFactory);

            var notificationRef = await NotificationService.CreateRefAsync(new NotificationConfig()
            {
                Message = Title,
                Description = Message,
                NotificationType = GetNotificationType(e.NotificationType)
            });
            notificationRef.OnClose = () =>
            {
                // 根据是否用户点击判断
                Okayed.InvokeAsync();//用户关闭
                Closed.InvokeAsync();//自动关闭
                return Task.CompletedTask;
            };
        }

        public virtual void Dispose()
        {
            if (UiNotificationService != null)
            {
                UiNotificationService.NotificationReceived -= OnNotificationReceived;
            }
        }
    }
}
