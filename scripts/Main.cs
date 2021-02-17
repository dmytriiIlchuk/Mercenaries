using Godot;

public class Main : Node
{
    public override void _Ready()
    {
        this.AddChild(GameObjectFactory.MakeResourceNode(new Vector2(5, 5)));
        this.AddChild(GameObjectFactory.MakeResourceNode(new Vector2(1, 1)));
        this.AddChild(GameObjectFactory.MakeResourceNode(new Vector2(10, 10)));
    }
}
