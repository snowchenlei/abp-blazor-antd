using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace Snow.Aba.AntdBlazorUI.Components.ObjectExtending
{
    public partial class TimeExtensionProperty<TEntity, TResourceType> : ComponentBase
        where TEntity : IHasExtraProperties
    {
        [Inject]
        public IStringLocalizerFactory StringLocalizerFactory { get; set; }

        [Parameter]
        public TEntity Entity { get; set; }

        [Parameter]
        public ObjectExtensionPropertyInfo PropertyInfo { get; set; }

        // TODO:如何使用TimeSpan
        protected DateTime? Value
        {
            get
            {
                return PropertyInfo.GetInputValueOrDefault<DateTime?>(Entity.GetProperty(PropertyInfo.Name));
            }
            set
            {
                Entity.SetProperty(PropertyInfo.Name, value, false);
            }
        }
    }
}
