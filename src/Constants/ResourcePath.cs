public class ResourcePath
{
    public static readonly string BaseResource = "res://";
    public static readonly string BaseSrc = "res://src";
    public static readonly string BaseUser = "user://";

    public class Editor
    {
        public static string BasePath = $"{BaseSrc}/Editor";
        public static readonly string CreateUnitConfigPath = $"{BasePath}/config/createUnit.config";
        public static readonly string EditorScenePath = $"{BasePath}/Editor.tscn";
    }

    public class UI
    {
        public static string BasePath = $"{BaseSrc}/{nameof(UI)}";
        public static string MenuScenePath = $"{BasePath}/Menu.tscn";
    }

    public class Models
    {
        public static string BasePath = $"{BaseSrc}/Models";

        public class Units
        {
            public static string BasePath = $"{Models.BasePath}/Units";
            public static string UnitScenePath = $"{BasePath}/Unit/Unit.tscn";
            public static string FormationScenePath = $"{BasePath}/Formations/Formation.tscn";
        }

        public class Buildings
        {
            public static string BasePath = $"{Models.BasePath}/Buildings";
            public static string ConstructionsPath = $"{BasePath}/Constructions";
            public static string ResourcesPath = $"{BasePath}/Resources";
            public static string StonePath = $"{BasePath}/Unit";
        }

        public class Main
        {
            public static string BasePath = $"{Models.BasePath}/World";
            public static string WorldPath = $"{BasePath}/World.tscn";
            public static string MainScenePath = $"{BasePath}/Main.tscn";
        }
    }
}

