using Godot;

class GameObjectFactory
{
    public static Unit MakeUnit()
    {
        var unitScene = GD.Load<PackedScene>("res://scenes/unit.tscn");

        Unit instance = (Unit)unitScene.Instance();
        instance.Transform.Scaled(new Vector2(0.5f, 0.5f));
        return instance;
    }

    public static Unit MakeUnit(Vector2 position)
    {
        var unitScene = GD.Load<PackedScene>("res://scenes/unit.tscn");

        Unit instance = (Unit)unitScene.Instance();
        instance.Scale = new Vector2(0.5f, 0.5f);
        instance.Position = position;
        instance.AddToGroup("workers");
        instance.tasks.Add(new UnitGatherResourceTask()))
        return instance;
    }

    public static Stone MakeResourceNode(Vector2 position)
    {
        var unitScene = GD.Load<PackedScene>("res://scenes/boulder.tscn");

        Stone instance = (Stone)unitScene.Instance();
        instance.Scale = new Vector2(0.1f, 0.1f);

        instance.Position = position;

        return instance;
    }
}

