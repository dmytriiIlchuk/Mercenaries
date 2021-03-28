using Godot;
using System.Collections.Generic;
using System.Linq;

public class Formation : Node2D, IMoving, IAttacking
{
    private IList<Unit> units = new List<Unit>();
    private IList<Vector2> positionMap = new List<Vector2>();
    private int rowSize = 10;
    private int rows = 2;
    private float positionOffset = 96.0f;
    private UnitType unitType = UnitType.Swordsman;

    public IList<IExecutable<Formation>> tasks = new List<IExecutable<Formation>>();

    public bool At(Vector2 target, float distance)
    {
        return units.All(unit => unit.At(target, distance));
    }

    public void MoveTo(Vector2 to, float delta)
    {
        this.Position = this.Position.MoveToward(to, delta);
        for (int i = 0; i < units.Count; i++)
        {
            Unit unit = units[i];

            Vector2 targetPosition = to + positionMap[i];
            unit.AddTask(new MoveToPointTask<Unit>(targetPosition));
        }
    }

    public override void _Ready()
    {
        BuildPositionMap();
        BuildUnits();
    }

    public override void _Process(float delta)
    {
        if (tasks.Count > 0 && tasks[0].Execute(this, delta))
        {
            tasks.RemoveAt(0);
        }
    }

    private void BuildPositionMap()
    {
        Vector2 currPosition = new Vector2(-positionOffset * (rowSize / 2), (-positionOffset / (rows / 2)));

        for (int row = 0; row < rows; row++)
        {
            for (int current = 0; current < rowSize; current++)
            {
                positionMap.Add(currPosition);
                currPosition.x += positionOffset;
            }
            currPosition.x = -positionOffset * (rowSize / 2);
            currPosition.y += positionOffset;
        }
    }

    private void BuildUnits()
    {
        foreach (Vector2 position in positionMap)
        {
            units.Add(GameObjectFactory.MakeUnit(this, position + this.Position, unitType, null));
        }
    }

    public void Attack(IHittable target)
    {
        throw new System.NotImplementedException();
    }
}