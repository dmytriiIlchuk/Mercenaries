using Godot;

class GameObjectFactory
{
    public static Unit MakeUnit(Node2D parent, Vector2 position, UnitType unitType)
    {
        Unit unit = MakeUnit(position, unitType);
        parent.AddChild(unit);

        return unit;
    }

    public static Unit MakeUnit(Vector2 position, UnitType unitType)
    {
        var unitScene = GD.Load<PackedScene>(ResourcePath.Models.Units.UnitScenePath);

        Unit instance = (Unit)unitScene.Instance();
        instance.Position = position;
        switch (unitType)
        {
            case UnitType.Worker:
                instance.SetUnitLabel(nameof(UnitType.Worker));
                instance.AddToGroup("workers");
                break;
            case UnitType.Archer:
                instance.SetUnitLabel(nameof(UnitType.Archer));
                instance.AddToGroup("warriors");
                break;
            case UnitType.Swordsman:
                instance.SetUnitLabel(nameof(UnitType.Swordsman));
                instance.AddToGroup("warriors");
                break;
        }

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

