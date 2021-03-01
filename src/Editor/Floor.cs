using Godot;
using System.Collections.Generic;
using System.Linq;

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

    public Godot.Collections.Dictionary<string, object> Save()
    {
        return new Godot.Collections.Dictionary<string, object>()
        {
            { "Filename", this.Filename },
            { "Parent", this.GetParent().GetPath() },
            { "PosX", Position.x }, // Vector2 is not supported by JSON
            { "PosY", Position.y },
            { "ScaleX", this.Scale.x },
            { "ScaleY", this.Scale.y },
            { "UsedCells", this.GetUsedCells() }
        };
    }

    public void Load(Godot.Collections.Dictionary<string, object> config)
    {
        this.Position = new Vector2((float)config["PosX"], (float)config["PosY"]);
        this.Scale = new Vector2((float)config["ScaleX"], (float)config["ScaleY"]);

        Godot.Collections.Array tiles = (Godot.Collections.Array)config["UsedCells"];

        foreach (string tileStr in tiles)
        {
            IEnumerable<float> xy = tileStr.Trim('(', ')').Split(',').Select(value => float.Parse(value));
            Vector2 tile = new Vector2(xy.ElementAt(0), xy.ElementAt(1));

            this.SetCellv(tile, 1);
        }
    }
}
