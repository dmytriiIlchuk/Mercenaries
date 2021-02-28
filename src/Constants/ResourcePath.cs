class ResourcePath
{
    public static readonly string BaseResource = "res://";
    public static readonly string BaseSrc = "res://src";
    public static readonly string BaseUser = "user://";

    public class EditorConfig
    {
        public static string BasePath = $"{BaseSrc}/Editor/config";
        public static readonly string CreateUnitConfigPath = $"{BasePath}/createUnit.config";
    }

    public class Models
    {
        public static string BasePath = $"{BaseSrc}/Models";

        public class Units
        {
            public static string BasePath = $"{Models.BasePath}/Units";
            public static string UnitPath = $"{BasePath}/Unit/Unit.tscn";
        }

        public class Buildings
        {
            public static string BasePath = $"{Models.BasePath}/Buildings";
            public static string ConstructionsPath = $"{BasePath}/Constructions";
            public static string ResourcesPath = $"{BasePath}/Resources";
            public static string StonePath = $"{BasePath}/Unit";
        }
    }
}

