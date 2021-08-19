
public class UnitConfig: GameObjectConfig
{
    public static UnitConfig Default = new UnitConfig()
    {
        Attack = 10.0f,
        HealthPointsMax = 100.0f,
        UnitType = UnitType.Swordsman,
        BodySize = new System.Tuple<int, int>(3, 3),
        Name = "Pleb",
        ObjectType = GameObjectType.Unit,
        Speed = 10.0f,
        ScenePath = ResourcePath.Models.Units.UnitScenePath,
        Groups = new string[] {
            "Workers"
        }
    };

    /// <summary>
    /// Maximum amount of healthpoints
    /// </summary>
    public float HealthPointsMax;

    public float Speed;

    public float Attack;

    public UnitType UnitType;

    public string[] Groups;
}