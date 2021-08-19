
public class BuildingConfig : GameObjectConfig
{
    public static BuildingConfig Default = new BuildingConfig()
    {
        HealthPointsMax = 100.0f,
        ConstructionPoints = 1000,
        BodySize = new System.Tuple<int, int>(3, 3),
        Name = "House",
        ObjectType = GameObjectType.Unit,
        ScenePath = ResourcePath.Models.Buildings.BuildingScenePath
    };

    /// <summary>
    /// Maximum amount of healthpoints
    /// </summary>
    public float HealthPointsMax;

    public int ConstructionPoints;
}