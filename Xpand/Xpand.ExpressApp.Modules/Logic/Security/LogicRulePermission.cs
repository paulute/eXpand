using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Xpand.ExpressApp.Logic.NodeUpdaters;
using Xpand.Persistent.Base.Logic;
using Xpand.Persistent.Base.Logic.Model;
using IRule = Xpand.Persistent.Base.Logic.IRule;
using PermissionBase = Xpand.ExpressApp.Security.Permissions.PermissionBase;

namespace Xpand.ExpressApp.Logic.Security {
    public abstract class LogicRulePermission : PermissionBase, ILogicRule {
        protected LogicRulePermission() {
            ExecutionContextGroup = LogicDefaultGroupContextNodeUpdater<IModelLogic,IModelNode>.Default;
        }

        public string ViewId { get; set; }


        [RuleRequiredField]
        public Type ObjectType { get; set; }
        [RuleRequiredField]
        public string ID { get; set; }

        public string ExecutionContextGroup { get; set; }
        public string ActionExecutionContextGroup { get; set; }


        public string ViewContextGroup { get; set; }
        public string FrameTemplateContextGroup { get; set; }
        public FrameTemplateContext FrameTemplateContext { get; set; }

        public bool? IsRootView { get; set; }


        public int? Index { get; set; }

        public ViewType ViewType { get; set; }
        public ViewEditMode? ViewEditMode { get; set; }

        IModelView ILogicRule.View { get; set; }

        public Nesting Nesting { get; set; }


        string IRule.Id {
            get { return ID; }
            set { ID = value; }
        }
        ITypeInfo ILogicRule.TypeInfo {
            get { return XafTypesInfo.Instance.FindTypeInfo(ObjectType); }
            set { }
        }
        [Size(SizeAttribute.Unlimited)]
        public string Description { get; set; }


    }
}