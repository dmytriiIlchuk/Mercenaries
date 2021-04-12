using Godot;
using System;

class GameObjectFactory
{
    public static GameObject MakeObject(Node2D parent, Vector2 position, GameObjectConfig config)
    {
        GameObject instance = MakeObject(position, config);

        parent.AddChild(instance);

        return instance;
    }

    public static GameObject MakeObject(Vector2 position, GameObjectConfig config)
    {
        GameObject instance;
        switch (config.ObjectType)
        {
            case GameObjectType.Unit:
                instance = MakeUnit(position, (UnitConfig)config);
                break;
            case GameObjectType.Formation:
                instance = MakeFormation(position, (UnitConfig)config);
                break;
            default:
                throw new NotImplementedException();
        }

        return instance;
    }

    public static Unit MakeUnit(Vector2 position, UnitConfig config)
    {
        var unitScene = GD.Load<PackedScene>(ResourcePath.Models.Units.UnitScenePath);

        Unit instance = (Unit)unitScene.Instance();
        instance.Position = position;
        switch (config.unitType)
        {
            case UnitType.Worker:
                instance.UnitLabel = nameof(UnitType.Worker);
                instance.AddToGroup("workers");
                break;
            case UnitType.Archer:
                instance.UnitLabel = nameof(UnitType.Archer);
                instance.AddToGroup("warriors");
                break;
            case UnitType.Swordsman:
                instance.UnitLabel = nameof(UnitType.Swordsman);
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

