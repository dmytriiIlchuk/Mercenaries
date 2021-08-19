
using System;

public class GameObjectConfig
{
    /// <summary>
    /// Size of the game object's body in boxes.
    /// </summary>
    public Tuple<int,int> BodySize;

    /// <summary>
    /// Name of the game object.
    /// </summary>
    public string Name;

    public GameObjectType ObjectType;

    public string ScenePath;
}