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

    public override bool Achieved(T performer)
    {
        return spawnAmount == 0;
    }

    public override void Action(T performer, float delta)
    {
        time += delta;
        if (time > spawnTime)
        {
            performer.GetParent().AddChild(gameObject);
            gameObject = gameObject.Duplicate();
            time = 0;
            spawnAmount--;
        }
    }
}