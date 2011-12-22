﻿using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Demos;
using DevExpress.ExpressApp;

namespace SecurityDemo.Module {
    public abstract class ClassLevelBaseObject : SecurityDemoBaseObject {
        private string description;
        public ClassLevelBaseObject(Session session)
            : base(session) {
        }
        public string Description {
            get {
                return description;
            }
            set {
                SetPropertyValue("Description", ref description, value);
            }
        }
    }


    [Hint(Hints.FullAccessObjectHint, ViewType.Any)]
    [NavigationItem(NavigationGroups.ClassLevelSecurity)]
    [ImageName("Demo_Security_FullAccess")]
    [Custom("Caption", "Fully Accessible Object")]
    public class FullAccessObject : ClassLevelBaseObject {
        public FullAccessObject(Session session)
            : base(session) {
        }
    }

    [Hint(Hints.ProtectedContentObjectHint, ViewType.Any)]
    [NavigationItem(NavigationGroups.ClassLevelSecurity)]
    [ImageName("Demo_Security_ProtectedContent")]
    [Custom("Caption", "Protected Object")]
    public class ProtectedContentObject : ClassLevelBaseObject {
        public ProtectedContentObject(Session session)
            : base(session) {
        }
    }

    [Hint(Hints.ReadOnlyObjectHint, ViewType.Any)]
    [NavigationItem(NavigationGroups.ClassLevelSecurity)]
    [ImageName("Demo_Security_ReadOnly")]
    [Custom("Caption", "Read-Only Object")]
    public class ReadOnlyObject : ClassLevelBaseObject {
        public ReadOnlyObject(Session session)
            : base(session) {
        }
    }

    [Hint(Hints.IrremovableObjectHint, ViewType.Any)]
    [NavigationItem(NavigationGroups.ClassLevelSecurity)]
    [ImageName("Demo_Security_Irremovable")]
    [Custom("Caption", "Protected Deletion Object")]
    public class IrremovableObject : ClassLevelBaseObject {
        public IrremovableObject(Session session)
            : base(session) {
        }
    }

    [Hint(Hints.UncreatableObjectHint, ViewType.Any)]
    [NavigationItem(NavigationGroups.ClassLevelSecurity)]
    [ImageName("Demo_Security_Uncreatable")]
    [Custom("Caption", "Protected Creation Object")]
    public class UncreatableObject : ClassLevelBaseObject {
        public UncreatableObject(Session session)
            : base(session) {
        }
    }
}
