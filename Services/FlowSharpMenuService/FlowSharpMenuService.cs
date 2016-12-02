﻿/* 
* Copyright (c) Marc Clifton
* The Code Project Open License (CPOL) 1.02
* http://www.codeproject.com/info/cpol10.aspx
*/

using System;
using System.Windows.Forms;

using Clifton.Core.ModuleManagement;
using Clifton.Core.Semantics;
using Clifton.Core.ServiceManagement;

using FlowSharpLib;
using FlowSharpServiceInterfaces;

namespace FlowSharpMenuService
{
    public class FlowSharpMenuModule : IModule
    {
        public void InitializeServices(IServiceManager serviceManager)
        {
            serviceManager.RegisterSingleton<IFlowSharpMenuService, FlowSharpMenuService>();
        }
    }

    public class FlowSharpMenuService : ServiceBase, IFlowSharpMenuService
    {
        protected MenuController menuController;
        protected Form mainForm;

        public override void Initialize(IServiceManager svcMgr)
        {
            base.Initialize(svcMgr);
        }

        public override void FinishedInitialization()
        {
            base.FinishedInitialization();
        }

        public void Initialize(Form mainForm)
        {
            this.mainForm = mainForm;
            menuController = new MenuController(ServiceManager, mainForm);
            mainForm.Controls.Add(menuController.MenuStrip);
        }

        public void Initialize(BaseController controller)
        {
            menuController.Initialize(controller);
        }

        public void UpdateMenu()
        {
            BaseController canvasController = ServiceManager.Get<IFlowSharpCanvasService>().ActiveController;
            menuController.UpdateMenu(canvasController.SelectedElements.Count > 0);
        }

        public bool SaveOrSaveAs()
        {
            return menuController.SaveOrSaveAs();
        }
    }

    public class FlowSharpMenuReceptor : IReceptor
    {
    }
}