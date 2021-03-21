using Godot;
using Godot.Collections;

class UnitGatherResourceTask : Task<Unit>
{
    readonly MoveToPointTask<Unit> moveToResourceTask;
    readonly MoveToPointTask<Unit> moveToDropOffTask;
    bool hasResource = false;

    public UnitGatherResourceTask(string resourceGroup, Node2D dropOff)
    {
        Array resources = dropOff.GetTree().GetNodesInGroup(resourceGroup);
        this.moveToResourceTask = new MoveToPointTask<Unit>(((Node2D)resources[0]).Position);
        this.moveToDropOffTask = new MoveToPointTask<Unit>(dropOff.Position);
    }

    public override bool Achieved(Unit performer)
    {
        return false;
    }

    public override void Action(Unit performer, float delta)
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
    }
}