﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Samba.Domain.Models.Resources;
using Samba.Presentation.Common;
using Samba.Services;

namespace Samba.Modules.ResourceModule.Widgets.ResourceSearch
{
    [Export(typeof(IWidgetCreator))]
    class ResourceSearchWidgetCreator : IWidgetCreator
    {
        private readonly IApplicationState _applicationState;
        private readonly IResourceService _resourceService;
        private readonly ICacheService _cacheService;

        [ImportingConstructor]
        public ResourceSearchWidgetCreator(IApplicationState applicationState, IResourceService resourceService,
            ICacheService cacheService)
        {
            _applicationState = applicationState;
            _resourceService = resourceService;
            _cacheService = cacheService;
        }

        public string GetCreatorName()
        {
            return "ResourceSearch";
        }

        public string GetCreatorDescription()
        {
            return "Resource Search";
        }

        public FrameworkElement CreateWidgetControl(IDiagram widget, ContextMenu contextMenu)
        {
            var viewModel = widget as ResourceSearchWidgetViewModel;
            viewModel.ResourceSearchViewModel.IsKeyboardVisible = false;
            var ret = new ResourceSearchView(viewModel.ResourceSearchViewModel) { DataContext = viewModel.ResourceSearchViewModel, ContextMenu = contextMenu };

            var heightBinding = new Binding("Height") { Source = viewModel, Mode = BindingMode.TwoWay };
            var widthBinding = new Binding("Width") { Source = viewModel, Mode = BindingMode.TwoWay };
            var xBinding = new Binding("X") { Source = viewModel, Mode = BindingMode.TwoWay };
            var yBinding = new Binding("Y") { Source = viewModel, Mode = BindingMode.TwoWay };

            ret.SetBinding(InkCanvas.LeftProperty, xBinding);
            ret.SetBinding(InkCanvas.TopProperty, yBinding);
            ret.SetBinding(FrameworkElement.HeightProperty, heightBinding);
            ret.SetBinding(FrameworkElement.WidthProperty, widthBinding);

            return ret;
        }

        public Widget CreateNewWidget()
        {
            return new Widget { CreatorName = GetCreatorName() };
        }

        public IDiagram CreateWidgetViewModel(Widget widget)
        {
            return new ResourceSearchWidgetViewModel(widget, _applicationState, _cacheService, _resourceService);
        }
    }
}
