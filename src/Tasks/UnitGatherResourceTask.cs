using Godot;
using Godot.Collections;

class UnitGatherResourceTask : Task<Unit>
{
    readonly MoveTask<Unit> moveToResourceTask;
    readonly MoveTask<Unit> moveToDropOffTask;
    bool hasResource = false;

    public UnitGatherResourceTask(string resourceGroup, Node2D dropOff)
    {
        Array resources = dropOff.GetTree().GetNodesInGroup(resourceGroup);
        this.moveToResourceTask = new MoveTask<Unit>(((Node2D)resources[0]).Position);
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
