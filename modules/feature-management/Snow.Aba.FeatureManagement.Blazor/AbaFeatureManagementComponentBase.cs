using Volo.Abp.AspNetCore.Components;
using Volo.Abp.FeatureManagement.Localization;

namespace Snow.Aba.FeatureManagement.Blazor
{
    public abstract class AbaFeatureManagementComponentBase : AbpComponentBase
    {
        protected AbaFeatureManagementComponentBase()
        {
            LocalizationResource = typeof(AbpFeatureManagementResource);
        }
    }
}