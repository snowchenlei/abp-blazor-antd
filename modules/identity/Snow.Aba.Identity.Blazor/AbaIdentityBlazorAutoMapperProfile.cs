using AutoMapper;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace Snow.Aba.Identity.Blazor
{
    public class AbaIdentityBlazorAutoMapperProfile : Profile
    {
        public AbaIdentityBlazorAutoMapperProfile()
        {
            CreateMap<IdentityUserDto, IdentityUserUpdateDto>()
                .MapExtraProperties()
                .Ignore(x => x.Password)
                .Ignore(x => x.RoleNames);

            CreateMap<IdentityRoleDto, IdentityRoleUpdateDto>()
                .MapExtraProperties();
        }
    }
}
