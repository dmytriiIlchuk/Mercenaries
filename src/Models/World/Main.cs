using Godot;
using Godot.Collections;

public class Main : Node
{
    public override void _Ready()
    {
        this.AddChild(GameObjectFactory.MakeResourceNode(new Vector2(100, 100)));
        this.AddChild(GameObjectFactory.MakeResourceNode(new Vector2(1, 1)));
        this.AddChild(GameObjectFactory.MakeResourceNode(new Vector2(100, 500)));
    }
}
