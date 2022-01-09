using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Snow.Aba.PermissionManagement.Blazor.Components;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;

namespace Snow.Aba.Identity.Blazor.Pages.Identity
{
    public partial class UserManagement
    {
        [Inject] protected IIdentityUserAppService IdentityUserAppService { get; set; }
        protected const string PermissionProviderName = "U";

        protected PermissionManagementModal PermissionManagementModal;

        protected const string DefaultSelectedTab = "UserInformations";

        protected IReadOnlyList<IdentityRoleDto> Roles;
        protected AssignedRoleViewModel[] NewUserRoles;

        protected AssignedRoleViewModel[] EditUserRoles;
        protected string ManagePermissionsPolicyName;

        protected bool HasManagePermissionsPermission { get; set; }

        protected string CreateModalSelectedTab = DefaultSelectedTab;

        protected string EditModalSelectedTab = DefaultSelectedTab;

        private Form<IdentityUserCreateDto> IdentityUserCreateForm;
        private Form<IdentityUserUpdateDto> IdentityUserEditForm;

        protected Modal EditModal;
        protected bool Loading = false;



        public UserManagement()
        {
            NewEntity = new IdentityUserCreateDto();
            CreatePolicyName = IdentityPermissions.Users.Create;
            UpdatePolicyName = IdentityPermissions.Users.Update;
            DeletePolicyName = IdentityPermissions.Users.Delete;
            ObjectMapperContext = typeof(AbaIdentityBlazorModule);
            LocalizationResource = typeof(IdentityResource);
        }

        protected override async Task OnInitializedAsync()
        {            
            await base.OnInitializedAsync();

            try
            {
                await SetPermissionsAsync();
                Roles = (await IdentityUserAppService.GetAssignableRolesAsync()).Items;
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected override async Task SetPermissionsAsync()
        {
            await base.SetPermissionsAsync();

            HasManagePermissionsPermission =
                await AuthorizationService.IsGrantedAsync(IdentityPermissions.Users.ManagePermissions);
        }

        protected override Task OpenCreateModalAsync()
        {
            CreateModalSelectedTab = DefaultSelectedTab;
            NewUserRoles = Roles.Select(x => new AssignedRoleViewModel
            {
                Name = x.Name,
                IsAssigned = x.IsDefault
            }).ToArray();

            return base.OpenCreateModalAsync();
        }

        protected override async Task OpenEditModalAsync(IdentityUserDto entity)
        {
            try
            {
                EditModalSelectedTab = DefaultSelectedTab;

                var userRoleNames = (await IdentityUserAppService.GetRolesAsync(entity.Id)).Items.Select(r => r.Name).ToList();

                EditUserRoles = Roles.Select(x => new AssignedRoleViewModel
                {
                    Name = x.Name,
                    IsAssigned = userRoleNames.Contains(x.Name)
                }).ToArray();

                await base.OpenEditModalAsync(entity);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected async Task OpenPermissionModalAsync(IdentityUserDto entity)
        {
            await PermissionManagementModal.OpenAsync(PermissionProviderName, entity.Id.ToString());
        }

        protected override Task OnCreatingEntityAsync()
        {
            CreateModalConfirmLoading = true;

            // apply roles before saving
            NewEntity.RoleNames = NewUserRoles.Where(x => x.IsAssigned).Select(x => x.Name).ToArray();

            return Task.CompletedTask;
        }

        protected override Task OnCreateEntityErrorAsync()
        {
            CreateModalConfirmLoading = false;
            return Task.CompletedTask;
        }

        protected override async Task OnCreatedEntityAsync()
        {
            CreateModalConfirmLoading = false;
            await base.OnCreatedEntityAsync();
        }

        protected override Task OnUpdatingEntityAsync()
        {
            EditModalConfirmLoading = true;

            // apply roles before saving
            EditingEntity.RoleNames = EditUserRoles.Where(x => x.IsAssigned).Select(x => x.Name).ToArray();

            return Task.CompletedTask;
        }

        protected override Task OnUpdateEntityErrorAsync()
        {
            EditModalConfirmLoading = false;

            return Task.CompletedTask;
        }

        protected override Task OnUpdatedEntityAsync()
        {
            EditModalConfirmLoading = false;
            return base.OnUpdatedEntityAsync();
        }

        protected override string GetDeleteConfirmationMessage(IdentityUserDto entity)
        {
            return string.Format(L["UserDeletionConfirmationMessage"], entity.UserName);
        }
    }

    public class AssignedRoleViewModel
    {
        public string Name { get; set; }

        public bool IsAssigned { get; set; }
    }

    public class Data
    {
        [DisplayName("Key")]
        public string Key { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Age")]
        public int Age { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Tags")]
        public string[] Tags { get; set; }
    }
}