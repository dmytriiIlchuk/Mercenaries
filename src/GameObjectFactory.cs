using Godot;

class GameObjectFactory
{
    public static GameObject MakeObject(GameObjectConfig config, Node2D parent, Vector2 position)
    {
        GameObject instance = MakeObject(config);

        parent.AddChild(instance);
        instance.Position = position;

        return instance;
    }

    public static GameObject MakeObject(GameObjectConfig config)
    {
        PackedScene scene = GD.Load<PackedScene>(config.ScenePath);
        GameObject instance = (GameObject)scene.Instance();

        instance.Initialize(config);

        return instance;
    }

    public static GameObject MakeUnit(Vector2 position, UnitConfig config)
    {
        var unitScene = GD.Load<PackedScene>(config.ScenePath);

        Unit instance = (Unit)unitScene.Instance();
        instance.Position = position;
        switch (config.UnitType)
        {
            case UnitType.Worker:
                instance.Label = nameof(UnitType.Worker);
                instance.AddToGroup("workers");
                break;
            case UnitType.Archer:
                instance.Label = nameof(UnitType.Archer);
                instance.AddToGroup("warriors");
                break;
            case UnitType.Swordsman:
                instance.Label = nameof(UnitType.Swordsman);
                instance.AddToGroup("warriors");
                break;
        }

        return instance;
    }

    public static Formation MakeFormation(Vector2 position, UnitConfig unitConfig)
    {
        var formationScene = GD.Load<PackedScene>(ResourcePath.Models.Units.FormationScenePath);

        Formation instance = (Formation)formationScene.Instance();
        instance.Position = position;
        instance.UnitConfig = unitConfig;

        return instance;
    }

    public static Stone MakeResourceNode(Vector2 position)
    {
        var unitScene = GD.Load<PackedScene>(ResourcePath.Models.Buildings.StonePath);

        Stone instance = (Stone)unitScene.Instance();
        instance.Scale = new Vector2(0.1f, 0.1f);

        instance.Position = position;

        return instance;
    }
}

