using AntDesign;
using Microsoft.AspNetCore.Components;
using Snow.Aba.AntdBlazorUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Messages;

namespace Snow.Aba.AntdBlazorUI.Components
{
    public partial class UiMessageAlert : ComponentBase, IDisposable
    {
        protected ModalRef ModalRef { get; set; }

        protected virtual bool IsConfirmation
            => MessageType == UiMessageType.Confirmation;
        protected virtual bool CenterMessage
          => Options?.CenterMessage ?? true;

        protected virtual bool ShowMessageIcon
           => Options?.ShowMessageIcon ?? true;

        protected virtual object MessageIcon => Options?.MessageIcon ?? MessageType switch
        {
            UiMessageType.Info => ConfirmIcon.Info,
            UiMessageType.Success => ConfirmIcon.Success,
            UiMessageType.Warning => ConfirmIcon.Warning,
            UiMessageType.Error => ConfirmIcon.Error,
            UiMessageType.Confirmation => ConfirmIcon.Info,
            _ => null,
        };

        protected virtual string MessageIconColor => MessageType switch
        {
            // gets the color in the order of importance: Blazorise > Bootstrap > fallback color
            UiMessageType.Info => "var(--b-theme-info, var(--info, #17a2b8))",
            UiMessageType.Success => "var(--b-theme-success, var(--success, #28a745))",
            UiMessageType.Warning => "var(--b-theme-warning, var(--warning, #ffc107))",
            UiMessageType.Error => "var(--b-theme-danger, var(--danger, #dc3545))",
            UiMessageType.Confirmation => "var(--b-theme-secondary, var(--secondary, #6c757d))",
            _ => null,
        };

        protected virtual string MessageIconStyle
        {
            get
            {
                var sb = new StringBuilder();

                sb.Append($"color:{MessageIconColor}");

                return sb.ToString();
            }
        }

        protected virtual string OkButtonText
            => Options?.OkButtonText ?? "OK";

        protected virtual string ConfirmButtonText
            => Options?.ConfirmButtonText ?? "Confirm";

        protected virtual string CancelButtonText
            => Options?.CancelButtonText ?? "Cancel";

        [Parameter] public UiMessageType MessageType { get; set; }

        [Parameter] public string Title { get; set; }

        [Parameter] public string Message { get; set; }

        [Parameter] public TaskCompletionSource<bool> Callback { get; set; }

        [Parameter] public UiMessageOptions Options { get; set; }

        [Parameter] public EventCallback Okayed { get; set; }

        [Parameter] public EventCallback Confirmed { get; set; }

        [Parameter] public EventCallback Canceled { get; set; }

        [Inject] public ModalService ModalService { get; set; }
        [Inject] public ConfirmService ConfirmService { get; set; }
        [Inject] public MessageService MessageService { get; set; }
        [Inject] protected AntdBlazorUiMessageService UiMessageService { get; set; }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            UiMessageService.MessageReceived += OnMessageReceived;
        }
        private async void OnMessageReceived(object sender, UiMessageEventArgs e)
        {
            MessageType = e.MessageType;
            Message = e.Message;
            Title = e.Title;
            Options = e.Options;
            Callback = e.Callback;

            ConfirmButtons confirmButtons = IsConfirmation ? ConfirmButtons.OKCancel : ConfirmButtons.OK;
            ConfirmIcon ConfirmIcon = ShowMessageIcon ? (ConfirmIcon)MessageIcon : ConfirmIcon.Info;
            var confirmResult = await ConfirmService.Show(Message, Title, confirmButtons, ConfirmIcon);
            if (IsConfirmation)
            {
                if (confirmResult == ConfirmResult.OK)
                {
                    await OnConfirmClicked();
                }
                else if (confirmResult == ConfirmResult.Cancel)
                {
                    await OnCancelClicked();
                }
            }
            else
            {
                if (confirmResult == ConfirmResult.OK)
                {
                    await OnOkClickedAsync();
                }
            }
        }

        public void Dispose()
        {
            if (UiMessageService != null)
            {
                UiMessageService.MessageReceived -= OnMessageReceived;
            }
        }
        protected async Task OnOkClickedAsync()
        {
            await InvokeAsync(async () =>
            {
                await Okayed.InvokeAsync(null);
            });
        }

        protected async Task OnConfirmClicked()
        {
            await InvokeAsync(async () =>
            {
                if (IsConfirmation && Callback != null)
                {
                    await InvokeAsync(() => Callback.SetResult(true));
                }

                await Confirmed.InvokeAsync(null);
            });
        }

        protected async Task OnCancelClicked()
        {
            await InvokeAsync(async () =>
            {
                if (IsConfirmation && Callback != null)
                {
                    await InvokeAsync(() => Callback.SetResult(false));
                }

                await Canceled.InvokeAsync(null);
            });
        }
    }
}