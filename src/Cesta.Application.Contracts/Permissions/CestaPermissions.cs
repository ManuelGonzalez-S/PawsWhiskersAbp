namespace Cesta.Permissions;

public static class CestaPermissions
{
    public const string GroupName = "Cesta";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class  Productos
    {
        public const string Default = GroupName + ".Productos";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

}
