using Godot;

public class ConstructBuildingTask<T> : Task<T> where T : Node2D
{
    private Building building;

    public ConstructBuildingTask(Building building)
    {
        this.building = building;
    }

    public override bool Achieved(T performer)
    {
        return building.Completed;
    }

    public override void Action(T performer, float delta)
    {
        building.Construct(1);
    }
}