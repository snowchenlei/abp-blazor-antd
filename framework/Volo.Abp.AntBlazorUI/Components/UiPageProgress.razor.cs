using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Progression;

namespace Volo.Abp.AntdBlazorUI.Components
{
    public partial class UiPageProgress : ComponentBase, IDisposable
    {
        protected int? Percentage { get; set; }

        protected bool Visible { get; set; }

        protected string Color { get; set; }

        [Inject] protected IUiPageProgressService UiPageProgressService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            UiPageProgressService.ProgressChanged += OnProgressChanged;
        }

        private async void OnProgressChanged(object sender, UiPageProgressEventArgs e)
        {
            Percentage = e.Percentage;
            Visible = e.Percentage == null || (e.Percentage >= 0 && e.Percentage <= 100);
            Color = GetColor(e.Options.Type);

            await InvokeAsync(StateHasChanged);
        }

        public virtual void Dispose()
        {
            if (UiPageProgressService != null)
            {
                UiPageProgressService.ProgressChanged -= OnProgressChanged;
            }
        }

        protected virtual string GetColor(UiPageProgressType pageProgressType)
        {
            // 颜色规范化
            return pageProgressType switch
            {
                UiPageProgressType.Info => "#17a2b8",
                UiPageProgressType.Success => "#28a745",
                UiPageProgressType.Warning => "#ffc107",
                UiPageProgressType.Error => "#dc3545",
                _ => "#007bff",
            };
        }
    }
}
