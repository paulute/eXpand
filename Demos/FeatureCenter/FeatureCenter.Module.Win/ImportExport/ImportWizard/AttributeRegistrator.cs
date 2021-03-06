﻿using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using Xpand.ExpressApp.Attributes;

namespace FeatureCenter.Module.Win.ImportExport.ImportWizard {
    public class AttributeRegistrator : Xpand.ExpressApp.Core.AttributeRegistrator {
        public override IEnumerable<Attribute> GetAttributes(ITypeInfo typesInfo) {
            if (typesInfo.Type != typeof(TestImportObject)) yield break;
            var testimportobjectListview = typeof(TestImportObject).Namespace+".TestImportObject_ListView";
            var xpandNavigationItemAttribute = new XpandNavigationItemAttribute("ImportWizard", testimportobjectListview);
            yield return xpandNavigationItemAttribute;

            yield return new DisplayFeatureModelAttribute(testimportobjectListview);
            yield return new WhatsNewAttribute(new DateTime(2011, 5, 10), xpandNavigationItemAttribute);
        }
    }
}
