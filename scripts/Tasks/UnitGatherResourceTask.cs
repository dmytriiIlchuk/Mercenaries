using Godot;

class UnitGatherResourceTask : Task<Unit>
{
    readonly MoveTask<Unit> moveToResourceTask;
    readonly MoveTask<Unit> moveToDropOffTask;
    bool hasResource = false;

    public UnitGatherResourceTask(Node2D resource, Node2D dropOff)
    {
        this.moveToResourceTask = new MoveTask<Unit>(resource.Position);
        this.moveToDropOffTask = new MoveTask<Unit>(dropOff.Position);
    }

    public override bool Execute(Unit performer, float delta)
    {
        if (hasResource)
        {
            if (moveToDropOffTask.Execute(performer, delta))
            {
                hasResource = false;
            }
        }
        else
        {
            if (moveToResourceTask.Execute(performer, delta))
            {
                hasResource = true;
            }
        }

        return false;
    }
}
