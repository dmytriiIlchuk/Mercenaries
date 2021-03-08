using Godot;

public class World : Node2D
{
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        foreach (Node2D node in this.GetChildren())
        {
            if (node is Unit)
            {
                Task<Unit> task = new MoveTask<Unit>(new Vector2(0, 0));
                ((Unit)node).AddTask(task);
            }
        }
    }
}
