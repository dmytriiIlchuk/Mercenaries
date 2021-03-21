using Godot;

public interface IMoving
{
    void MoveTo(Vector2 to, float delta);

    bool At(Vector2 target, float distance);
}