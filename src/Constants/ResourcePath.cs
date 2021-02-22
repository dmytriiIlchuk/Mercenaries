namespace Mercenaries.src.Constants
{
    class ResourcePath
    {
        public static readonly string BaseResource = "res://";
        public static readonly string BaseUser = "user://";

        class EditorConfig
        {
            public static string BasePath = $"{BaseResource}/Editor/config";
            public static readonly string CreateUnitConfigPath = $"{BasePath}/createUnit.config";
        }
    }
}
