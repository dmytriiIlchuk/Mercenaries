using Godot;

public class SpawnGameObjectTask<T> : TimedTask<T> where T : Node
{
    private Node gameObject;
    private int spawnAmount;


    public SpawnGameObjectTask(float spawnTime, Node gameObject, int? spawnAmount = 1) : base(spawnTime)
    {
        this.gameObject = gameObject;
    }

    public override bool Achieved(T performer)
    {
        return spawnAmount == 0;
    }

    public override void Action(T performer, float delta)
    {
        performer.GetParent().AddChild(gameObject);
        gameObject = gameObject.Duplicate();
        spawnAmount--;
    }
}