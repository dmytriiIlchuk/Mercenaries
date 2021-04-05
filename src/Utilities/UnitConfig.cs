
public class UnitConfig: GameObjectConfig
{
    public static UnitConfig Default = new UnitConfig()
    {
        Attack = 10.0f
    };

    /// <summary>
    /// Maximum amount of healthpoints
    /// </summary>
    public float HealthPointsMax;

    public float Speed;

    public float Attack;

    public UnitType unitType;
}