﻿using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TenDaysTake2.ViewModels.Helpers
{
    class PermissionsHelper
    {

        // added using Plugin.Permissions.Abstractions;
        // added using System.Threading.Tasks;
        public static async Task<PermissionStatus> GetPermission(Permission permissionType)
        {
            // added using Plugin.Permissions;
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permissionType);
            if (status != PermissionStatus.Granted)
            {
                // This is the actual permission request
                var results = await CrossPermissions.Current.RequestPermissionsAsync(permissionType);
                if (results.ContainsKey(permissionType))
                    status = results[permissionType];
            }

            return status;
        }

    }
}
