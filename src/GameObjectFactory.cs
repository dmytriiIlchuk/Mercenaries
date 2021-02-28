using Godot;

class GameObjectFactory
{
    public static void MakeUnit(Node2D parent, Vector2 position)
    {
        var unitScene = GD.Load<PackedScene>(ResourcePath.Models.Units.UnitPath);

        Unit instance = (Unit)unitScene.Instance();
        instance.Scale = new Vector2(0.5f, 0.5f);
        instance.Position = position;

        parent.AddChild(instance);
    }

    public static Unit MakeUnit(Vector2 position)
    {
        var unitScene = GD.Load<PackedScene>(ResourcePath.Models.Units.UnitPath);

        Unit instance = (Unit)unitScene.Instance();
        instance.Scale = new Vector2(0.5f, 0.5f);
        instance.Position = position;
        instance.AddToGroup("workers");
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

