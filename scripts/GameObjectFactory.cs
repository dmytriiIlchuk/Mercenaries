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
        return instance;
    }

    public static Node2D MakeResourceNode(Vector2 position)
    {
        Node2D parent = new Node2D();
        Sprite sprite = new Sprite();
        ImageTexture imageTexture = new ImageTexture();
        Image image = new Image();
        image.Load("res://assets/sprites/buildings/stone.png");
        imageTexture.CreateFromImage(new Image());
        sprite.Texture = imageTexture;
        parent.AddChild(sprite);

        parent.Position = position;

        return parent;
    }
}

