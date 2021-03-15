using Godot;

public class SpawnGameObjectTask<T> : Task<T> where T : Node
{
    private float spawnTime;
    private float time = 0;
    private Node gameObject;
    private int spawnAmount;


    public SpawnGameObjectTask(float spawnTime, Node gameObject, int? spawnAmount = 1)
    {
        this.spawnTime = spawnTime;
        this.gameObject = gameObject;
    }
    public override void Execute(T performer, float delta)
    {
        time += delta;
        if (time > spawnTime && spawnAmount > 0)
        {
            performer.GetParent().AddChild(gameObject);
            spawnAmount--;
            gameObject = gameObject.Duplicate();
            time = 0;
        }
    }
}