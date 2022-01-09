using AutoMapper;

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
