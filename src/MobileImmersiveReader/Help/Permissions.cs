using System.Threading.Tasks;

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace MobileImmersiveReader
{
    public static partial class Help
    {
        public static class Permissions
        {
            public static async Task<bool> IsGranted(Permission permission)
            {
                return (await CrossPermissions.Current.CheckPermissionStatusAsync(permission) == PermissionStatus.Granted);
            }

            public static async Task<bool> Request(Permission permission)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);

                bool status = false;

                if (results.ContainsKey(permission))
                    status = (results[permission] == PermissionStatus.Granted);

                return status;
            }

            public static async Task<bool> RequestIfNotGranted(Permission permission)
            {
                if (!await IsGranted(permission))
                {
                    return await Request(permission);
                }

                return true;
            }
        }
    }
}
