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
    public partial class RoleManagement
    {
        [Inject] protected IIdentityRoleAppService IdentityRoleAppService { get; set; }
        protected const string PermissionProviderName = "R";

        protected PermissionManagementModal PermissionManagementModal;

        protected string ManagePermissionsPolicyName;

        protected bool HasManagePermissionsPermission { get; set; }

        private Form<IdentityRoleCreateDto> IdentityRoleCreateForm;
        private Form<IdentityRoleUpdateDto> IdentityRoleEditForm;

        protected Modal EditModal;
        protected bool Loading = false;

        public RoleManagement()
        {
            NewEntity = new IdentityRoleCreateDto();
            CreatePolicyName = IdentityPermissions.Roles.Create;
            UpdatePolicyName = IdentityPermissions.Roles.Update;
            DeletePolicyName = IdentityPermissions.Roles.Delete;
            ObjectMapperContext = typeof(AbaIdentityBlazorModule);
            LocalizationResource = typeof(IdentityResource);
        }

        protected override async Task SetPermissionsAsync()
        {
            await base.SetPermissionsAsync();

            HasManagePermissionsPermission =
                await AuthorizationService.IsGrantedAsync(IdentityPermissions.Roles.ManagePermissions);
        }

        protected async Task OpenPermissionModalAsync(IdentityRoleDto entity)
        {
            await PermissionManagementModal.OpenAsync(PermissionProviderName, entity.Name.ToString());
        }

        protected override async Task OnCreatingEntityAsync()
        {
            await base.OnCreatingEntityAsync();
        }

        protected override async Task OnCreateEntityErrorAsync()
        {
            await base.OnCreateEntityErrorAsync();
        }

        protected override async Task OnCreatedEntityAsync()
        {
            await base.OnCreatedEntityAsync();
        }

        protected override async Task OnUpdatingEntityAsync()
        {
            await base.OnUpdatingEntityAsync();
        }

        protected override async Task OnUpdateEntityErrorAsync()
        {
            await base.OnUpdateEntityErrorAsync();
        }

        protected override async Task OnUpdatedEntityAsync()
        {
            await base.OnUpdatedEntityAsync();
        }
        protected override string GetDeleteConfirmationMessage(IdentityRoleDto entity)
        {
            return string.Format(L["RoleDeletionConfirmationMessage"], entity.Name);
        }
    }
}