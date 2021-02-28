using Godot;
using Godot.Collections;
using System;

public class Floor : TileMap, IPersistant
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void SetCellvFromGlobal(Vector2 vector, int tile)
    {
        Vector2 mapPosition = this.WorldToMap(vector);
        this.SetCellv(mapPosition, tile);
    }

    public Dictionary<string, object> Save()
    {
        return new Dictionary<string, object>()
        {
            { "Filename", this.Filename },
            { "Parent", this.GetParent().GetPath() },
            { "PosX", Position.x }, // Vector2 is not supported by JSON
            { "PosY", Position.y },
            { "ScaleX", this.Scale.x },
            { "ScaleY", this.Scale.y },
        };
    }

    public void Load(Dictionary<string, object> config)
    {
        throw new NotImplementedException();
    }
}
