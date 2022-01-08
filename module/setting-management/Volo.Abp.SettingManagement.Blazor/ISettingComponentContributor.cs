using System.Threading.Tasks;

namespace Snow.Aba.SettingManagement.Blazor
{
    public interface ISettingComponentContributor
    {
        Task ConfigureAsync(SettingComponentCreationContext context);

        Task<bool> CheckPermissionsAsync(SettingComponentCreationContext context);
    }
}