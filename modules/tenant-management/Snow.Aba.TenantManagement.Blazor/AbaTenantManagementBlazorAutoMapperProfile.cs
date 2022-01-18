using AutoMapper;
using Volo.Abp.TenantManagement;

namespace Snow.Aba.TenantManagement.Blazor
{
    public class AbaTenantManagementBlazorAutoMapperProfile : Profile
    {
        public AbaTenantManagementBlazorAutoMapperProfile()
        {
            CreateMap<TenantDto, TenantUpdateDto>()
                .MapExtraProperties();
        }
    }
}
