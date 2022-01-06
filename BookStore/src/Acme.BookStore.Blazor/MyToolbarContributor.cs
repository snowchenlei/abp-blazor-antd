using Acme.BookStore.Blazor.Pages;
using Snow.Aba.AspNetCore.Components.Web.Theming.Toolbars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.BookStore.Blazor
{
    public class MyToolbarContributor: IToolbarContributor
    {
        public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
        {
            if (context.Toolbar.Name == StandardToolbars.Main)
            {
                context.Toolbar.Items.Insert(0, new ToolbarItem(typeof(Notification)));
            }

            return Task.CompletedTask;
        }
    }
}
