﻿using System;
using DevExpress.ExpressApp.Security;

namespace eXpand.ExpressApp.MemberLevelSecurity {
    public class MemberAccessPermissionItem {
        public MemberAccessPermissionItem() {
        }

        public MemberAccessPermissionItem(MemberAccessPermissionItem source) {
            MemberName = source.MemberName;
            ObjectType = source.ObjectType;
            Operation = source.Operation;
            Modifier = source.Modifier;
        }

        public Type ObjectType { get; set; }

        public string MemberName { get; set; }

        public MemberOperation Operation { get; set; }

        public ObjectAccessModifier Modifier { get; set; }
        public string Criteria { get; set; }
    }
}