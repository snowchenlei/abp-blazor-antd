using AutoMapper;
using Volo.Abp.SettingManagement;

namespace Snow.Aba.SettingManagement.Blazor
{
    public class SettingManagementBlazorAutoMapperProfile : Profile
    {
        public SettingManagementBlazorAutoMapperProfile()
        {
            CreateMap<EmailSettingsDto, UpdateEmailSettingsDto>();
        }
    }
}
