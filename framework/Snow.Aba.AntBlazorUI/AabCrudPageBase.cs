﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using AntDesign;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.Localization;
using Volo.Abp.Authorization;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using AntDesign.TableModels;

namespace Snow.Aba.AntdBlazorUI
{
    public abstract class AabCrudPageBase<
            TAppService,
            TEntityDto,
            TKey>
        : AbpCrudPageBase<
            TAppService,
            TEntityDto,
            TKey,
            PagedAndSortedResultRequestDto>
        where TAppService : ICrudAppService<
            TEntityDto,
            TKey>
        where TEntityDto : class, IEntityDto<TKey>, new()
    {
    }

    public abstract class AbpCrudPageBase<
            TAppService,
            TEntityDto,
            TKey,
            TGetListInput>
        : AbpCrudPageBase<
            TAppService,
            TEntityDto,
            TKey,
            TGetListInput,
            TEntityDto>
        where TAppService : ICrudAppService<
            TEntityDto,
            TKey,
            TGetListInput>
        where TEntityDto : class, IEntityDto<TKey>, new()
        where TGetListInput : new()
    {
    }

    public abstract class AbpCrudPageBase<
            TAppService,
            TEntityDto,
            TKey,
            TGetListInput,
            TCreateInput>
        : AbpCrudPageBase<
            TAppService,
            TEntityDto,
            TKey,
            TGetListInput,
            TCreateInput,
            TCreateInput>
        where TAppService : ICrudAppService<
            TEntityDto,
            TKey,
            TGetListInput,
            TCreateInput>
        where TEntityDto : IEntityDto<TKey>
        where TCreateInput : class, new()
        where TGetListInput : new()
    {
    }

    public abstract class AbpCrudPageBase<
            TAppService,
            TEntityDto,
            TKey,
            TGetListInput,
            TCreateInput,
            TUpdateInput>
        : AbpCrudPageBase<
            TAppService,
            TEntityDto,
            TEntityDto,
            TKey,
            TGetListInput,
            TCreateInput,
            TUpdateInput>
        where TAppService : ICrudAppService<
            TEntityDto,
            TKey,
            TGetListInput,
            TCreateInput,
            TUpdateInput>
        where TEntityDto : IEntityDto<TKey>
        where TCreateInput : class, new()
        where TUpdateInput : class, new()
        where TGetListInput : new()
    {
    }

    public abstract class AbpCrudPageBase<
            TAppService,
            TGetOutputDto,
            TGetListOutputDto,
            TKey,
            TGetListInput,
            TCreateInput,
            TUpdateInput>
        : AbpCrudPageBase<
            TAppService,
            TGetOutputDto,
            TGetListOutputDto,
            TKey,
            TGetListInput,
            TCreateInput,
            TUpdateInput,
            TGetListOutputDto,
            TCreateInput,
            TUpdateInput>
        where TAppService : ICrudAppService<
            TGetOutputDto,
            TGetListOutputDto,
            TKey,
            TGetListInput,
            TCreateInput,
            TUpdateInput>
        where TGetOutputDto : IEntityDto<TKey>
        where TGetListOutputDto : IEntityDto<TKey>
        where TCreateInput : class, new()
        where TUpdateInput : class, new()
        where TGetListInput : new()
    {
    }

    public abstract class AbpCrudPageBase<
            TAppService,
            TGetOutputDto,
            TGetListOutputDto,
            TKey,
            TGetListInput,
            TCreateInput,
            TUpdateInput,
            TListViewModel,
            TCreateViewModel,
            TUpdateViewModel>
        : AbpComponentBase
        where TAppService : ICrudAppService<
            TGetOutputDto,
            TGetListOutputDto,
            TKey,
            TGetListInput,
            TCreateInput,
            TUpdateInput>
        where TGetOutputDto : IEntityDto<TKey>
        where TGetListOutputDto : IEntityDto<TKey>
        where TCreateInput : class
        where TUpdateInput : class
        where TGetListInput : new()
        where TListViewModel : IEntityDto<TKey>
        where TCreateViewModel : class, new()
        where TUpdateViewModel : class, new()
    {
        [Inject] protected TAppService AppService { get; set; }
        [Inject] protected IStringLocalizer<AbpUiResource> UiLocalizer { get; set; }

        protected virtual int PageSize { get; set; } = LimitedResultRequestDto.DefaultMaxResultCount;

        protected int CurrentPage = 1;
        protected string CurrentSorting;
        protected int? TotalCount;
        protected TGetListInput GetListInput = new TGetListInput();
        protected IReadOnlyList<TListViewModel> Entities = Array.Empty<TListViewModel>();
        protected TCreateViewModel NewEntity;
        protected TKey EditingEntityId;
        protected TUpdateViewModel EditingEntity;
        protected bool ShowCreateModal;
        protected bool ShowEditModal;
        //protected Validations CreateValidationsRef;
        //protected Validations EditValidationsRef;
        protected List<BreadcrumbItem> BreadcrumbItems = new List<BreadcrumbItem>(2);
        protected EntityActionDictionary EntityActions { get; set; }
        protected TableColumnDictionary TableColumns { get; set; }

        protected string CreatePolicyName { get; set; }
        protected string UpdatePolicyName { get; set; }
        protected string DeletePolicyName { get; set; }

        public bool HasCreatePermission { get; set; }
        public bool HasUpdatePermission { get; set; }
        public bool HasDeletePermission { get; set; }

        protected AbpCrudPageBase()
        {
            NewEntity = new TCreateViewModel();
            EditingEntity = new TUpdateViewModel();
            TableColumns = new TableColumnDictionary();
            EntityActions = new EntityActionDictionary();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await SetEntityActionsAsync();
            await SetTableColumnsAsync();
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await base.OnAfterRenderAsync(firstRender);
                await SetToolbarItemsAsync();
                await SetBreadcrumbItemsAsync();
            }
        }

        protected virtual async Task SetPermissionsAsync()
        {
            if (CreatePolicyName != null)
            {
                HasCreatePermission = await AuthorizationService.IsGrantedAsync(CreatePolicyName);
            }

            if (UpdatePolicyName != null)
            {
                HasUpdatePermission = await AuthorizationService.IsGrantedAsync(UpdatePolicyName);
            }

            if (DeletePolicyName != null)
            {
                HasDeletePermission = await AuthorizationService.IsGrantedAsync(DeletePolicyName);
            }
        }

        protected virtual async Task GetEntitiesAsync()
        {
            try
            {
                await UpdateGetListInputAsync();
                var result = await AppService.GetListAsync(GetListInput);
                Entities = MapToListViewModel(result.Items);
                TotalCount = (int?)result.TotalCount;
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private IReadOnlyList<TListViewModel> MapToListViewModel(IReadOnlyList<TGetListOutputDto> dtos)
        {
            if (typeof(TGetListOutputDto) == typeof(TListViewModel))
            {
                return dtos.As<IReadOnlyList<TListViewModel>>();
            }

            return ObjectMapper.Map<IReadOnlyList<TGetListOutputDto>, List<TListViewModel>>(dtos);
        }

        protected virtual Task UpdateGetListInputAsync()
        {
            if (GetListInput is ISortedResultRequest sortedResultRequestInput)
            {
                sortedResultRequestInput.Sorting = CurrentSorting;
            }

            if (GetListInput is IPagedResultRequest pagedResultRequestInput)
            {
                pagedResultRequestInput.SkipCount = (CurrentPage - 1) * PageSize;
            }

            if (GetListInput is ILimitedResultRequest limitedResultRequestInput)
            {
                limitedResultRequestInput.MaxResultCount = PageSize;
            }

            return Task.CompletedTask;
        }

        protected virtual async Task SearchEntitiesAsync()
        {
            CurrentPage = 1;

            await GetEntitiesAsync();

            await InvokeAsync(StateHasChanged);
        }

        protected virtual async Task OnTableReadAsync(QueryModel<TGetListInput> e)
        {
            // TODO:排序
            CurrentSorting = e.SortModel.Select(c => c.FieldName + c.Sort).JoinAsString(",");
            CurrentPage = e.PageIndex;

            await GetEntitiesAsync();

            await InvokeAsync(StateHasChanged);
        }

        protected virtual async Task OpenCreateModalAsync()
        {
            try
            {
                //CreateValidationsRef?.ClearAll();

                await CheckCreatePolicyAsync();

                NewEntity = new TCreateViewModel();

                // Mapper will not notify Blazor that binded values are changed
                // so we need to notify it manually by calling StateHasChanged
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                    ShowCreateModal = true;
                });
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected virtual Task CloseCreateModalAsync()
        {
            ShowCreateModal = false;
            return Task.CompletedTask;
        }

        protected virtual async Task OpenEditModalAsync(TListViewModel entity)
        {
            try
            {
                //EditValidationsRef?.ClearAll();

                await CheckUpdatePolicyAsync();

                var entityDto = await AppService.GetAsync(entity.Id);

                EditingEntityId = entity.Id;
                EditingEntity = MapToEditingEntity(entityDto);

                await InvokeAsync(() =>
                {
                    StateHasChanged();
                    ShowEditModal = true;
                });
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected virtual TUpdateViewModel MapToEditingEntity(TGetOutputDto entityDto)
        {
            return ObjectMapper.Map<TGetOutputDto, TUpdateViewModel>(entityDto);
        }

        protected virtual TCreateInput MapToCreateInput(TCreateViewModel createViewModel)
        {
            if (typeof(TCreateInput) == typeof(TCreateViewModel))
            {
                return createViewModel.As<TCreateInput>();
            }

            return ObjectMapper.Map<TCreateViewModel, TCreateInput>(createViewModel);
        }

        protected virtual TUpdateInput MapToUpdateInput(TUpdateViewModel updateViewModel)
        {
            if (typeof(TUpdateInput) == typeof(TUpdateViewModel))
            {
                return updateViewModel.As<TUpdateInput>();
            }

            return ObjectMapper.Map<TUpdateViewModel, TUpdateInput>(updateViewModel);
        }

        protected virtual Task CloseEditModalAsync()
        {
            ShowEditModal = false;
            return Task.CompletedTask;
        }

        protected virtual async Task CreateEntityAsync()
        {
            try
            {
                //if (CreateValidationsRef?.ValidateAll() ?? true)
                {
                    await OnCreatingEntityAsync();

                    await CheckCreatePolicyAsync();
                    var createInput = MapToCreateInput(NewEntity);
                    await AppService.CreateAsync(createInput);

                    await OnCreatedEntityAsync();
                }
            }
            catch (Exception ex)
            {
                await OnCreateEntityErrorAsync();
                await HandleErrorAsync(ex);
            }
        }

        protected virtual Task OnCreatingEntityAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnCreateEntityErrorAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual async Task OnCreatedEntityAsync()
        {
            await GetEntitiesAsync();
            ShowCreateModal = false;
        }

        protected virtual async Task UpdateEntityAsync()
        {
            try
            {
                //if (EditValidationsRef?.ValidateAll() ?? true)
                {
                    await OnUpdatingEntityAsync();

                    await CheckUpdatePolicyAsync();
                    var updateInput = MapToUpdateInput(EditingEntity);
                    await AppService.UpdateAsync(EditingEntityId, updateInput);

                    await OnUpdatedEntityAsync();
                }
            }
            catch (Exception ex)
            {
                await OnUpdateEntityErrorAsync();
                await HandleErrorAsync(ex);
            }
        }

        protected virtual Task OnUpdatingEntityAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnUpdateEntityErrorAsync()
        {
            return Task.CompletedTask;
        }
        protected virtual async Task OnUpdatedEntityAsync()
        {
            await GetEntitiesAsync();
            ShowEditModal = false;
        }

        protected virtual async Task DeleteEntityAsync(TListViewModel entity)
        {
            try
            {
                await CheckDeletePolicyAsync();

                await AppService.DeleteAsync(entity.Id);
                await GetEntitiesAsync();
                await InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected virtual string GetDeleteConfirmationMessage(TListViewModel entity)
        {
            return UiLocalizer["ItemWillBeDeletedMessage"];
        }

        protected virtual async Task CheckCreatePolicyAsync()
        {
            await CheckPolicyAsync(CreatePolicyName);
        }

        protected virtual async Task CheckUpdatePolicyAsync()
        {
            await CheckPolicyAsync(UpdatePolicyName);
        }

        protected virtual async Task CheckDeletePolicyAsync()
        {
            await CheckPolicyAsync(DeletePolicyName);
        }

        /// <summary>
        /// Calls IAuthorizationService.CheckAsync for the given <paramref name="policyName"/>.
        /// Throws <see cref="AbpAuthorizationException"/> if given policy was not granted for the current user.
        ///
        /// Does nothing if <paramref name="policyName"/> is null or empty.
        /// </summary>
        /// <param name="policyName">A policy name to check</param>
        protected virtual async Task CheckPolicyAsync([CanBeNull] string policyName)
        {
            if (string.IsNullOrEmpty(policyName))
            {
                return;
            }

            await AuthorizationService.CheckAsync(policyName);
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetEntityActionsAsync()
        {
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetTableColumnsAsync()
        {
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            return ValueTask.CompletedTask;
        }

        protected virtual IEnumerable<TableColumn> GetExtensionTableColumns(string moduleName, string entityType)
        {
            var properties = ModuleExtensionConfigurationHelper.GetPropertyConfigurations(moduleName, entityType);
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.IsAvailableToClients && propertyInfo.UI.OnTable.IsVisible)
                {
                    if (propertyInfo.Name.EndsWith("_Text"))
                    {
                        var lookupPropertyName = propertyInfo.Name.RemovePostFix("_Text");
                        var lookupPropertyDefinition = properties.SingleOrDefault(t => t.Name == lookupPropertyName);
                        yield return new TableColumn
                        {
                            Title = lookupPropertyDefinition.GetLocalizedDisplayName(StringLocalizerFactory),
                            Data = $"ExtraProperties[{propertyInfo.Name}]"
                        };
                    }
                    else
                    {
                        var column = new TableColumn
                        {
                            Title = propertyInfo.GetLocalizedDisplayName(StringLocalizerFactory),
                            Data = $"ExtraProperties[{propertyInfo.Name}]"
                        };

                        // TODO:类型判断
                        //if (propertyInfo.IsDate() || propertyInfo.IsDateTime())
                        //{
                        //    column.DisplayFormat = propertyInfo.GetDateEditInputFormatOrNull();
                        //}

                        //if (propertyInfo.Type.IsEnum)
                        //{
                        //    column.ValueConverter = (val) =>
                        //        EnumHelper.GetLocalizedMemberName(propertyInfo.Type, val, StringLocalizerFactory);
                        //}

                        yield return column;
                    }
                }
            }
        }
    }
}