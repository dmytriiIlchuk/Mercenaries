using Godot;

class GameObjectFactory
{
    public static Unit MakeUnit(Node2D parent, Vector2 position, UnitType unitType, KnowledgeBase knowledgeBase)
    {
        Unit unit = MakeUnit(position, unitType, knowledgeBase);
        parent.AddChild(unit);

        return unit;
    }

    public static Unit MakeUnit(Vector2 position, UnitType unitType, KnowledgeBase knowledgeBase)
    {
        var unitScene = GD.Load<PackedScene>(ResourcePath.Models.Units.UnitScenePath);

        Unit instance = (Unit)unitScene.Instance();
        instance.Position = position;
        instance.KnowledgeBase = knowledgeBase;
        switch (unitType)
        {
            case UnitType.Worker:
                instance.UnitLabel = nameof(UnitType.Worker);
                instance.AddToGroup("workers");
                break;
            case UnitType.Archer:
                instance.UnitLabel = nameof(UnitType.Archer);
                instance.AddToGroup("warriors");
                break;
            case UnitType.Swordsman:
                instance.UnitLabel = nameof(UnitType.Swordsman);
                instance.AddToGroup("warriors");
                break;
        }

        return instance;
    }

    public static Formation MakeFormation(Node2D parent, Vector2 position)
    {
        Formation formation = MakeFormation(position);
        parent.AddChild(formation);

        return formation;
    }

    public static Formation MakeFormation(Vector2 position)
    {
        var formationScene = GD.Load<PackedScene>(ResourcePath.Models.Units.FormationScenePath);

        Formation instance = (Formation)formationScene.Instance();
        instance.Position = position;

        return instance;
    }

    public static Stone MakeResourceNode(Vector2 position)
    {
        var unitScene = GD.Load<PackedScene>(ResourcePath.Models.Buildings.StonePath);

        Stone instance = (Stone)unitScene.Instance();
        instance.Scale = new Vector2(0.1f, 0.1f);

        instance.Position = position;

        return instance;
    }
}

