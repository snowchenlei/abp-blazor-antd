using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Data;

namespace Snow.Aba.AntdBlazorUI.Components.ObjectExtending
{
    public partial class ExtensionProperties<TEntityType, TResourceType> : ComponentBase
        where TEntityType : IHasExtraProperties
    {
        [Inject]
        public IStringLocalizerFactory StringLocalizerFactory { get; set; }

        [Parameter]
        public AbpBlazorMessageLocalizerHelper<TResourceType> LH { get; set; }

        [Parameter]
        public TEntityType Entity { get; set; }
    }
}
