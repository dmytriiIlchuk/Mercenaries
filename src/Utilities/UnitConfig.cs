
public class UnitConfig: GameObjectConfig
{
    public static UnitConfig Default = new UnitConfig()
    {
        Attack = 10.0f,
        HealthPointsMax = 100.0f,
        unitType = UnitType.Swordsman,
        BodySize = new System.Tuple<int, int>(3,3),
        Name = "ff",
        ObjectType = GameObjectType.Unit,
        Speed = 10.0f
    };

    /// <summary>
    /// Maximum amount of healthpoints
    /// </summary>
    public float HealthPointsMax;

    public float Speed;

    public float Attack;

    public UnitType unitType;
}