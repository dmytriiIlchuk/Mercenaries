using Godot;
using System.Collections.Generic;

public class Unit : Node2D, IPersistant
{
    // Declare member variables here. Examples:
    public int speed = 10;
    public int vitality = 10;
    public int wounds = 0;

    public IList<Task<Unit>> tasks = new List<Task<Unit>>();
    private string unitLabel;

    [Signal]
    public delegate void UnitInput(InputEvent @event, Unit unit);

    public string GetUnitLabel()
    {
        return this.unitLabel;
    }

    public void SetUnitLabel(string name)
    {
        this.unitLabel = name;
        this.GetNode<Label>("Label").Text = this.unitLabel;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (tasks.Count > 0 && tasks[0].Execute(this, delta))
        {
            tasks.RemoveAt(0);
        }
    }

    public override void _Input(InputEvent @event)
    {
        EmitSignal(nameof(UnitInput), @event, this);
    }

    public Task<Unit> GetNextTask()
    {
        return (tasks.Count > 0) ? tasks[0] : null;
    }

    public void AddTask(Task<Unit> task)
    {
        tasks.Add(task);
    }

    public bool Hit()
    {
        this.wounds++;
        if (this.wounds > this.vitality)
        {
            this.Die();
            return true;
        }

        return false;
    }

    public void Die()
    {
        this.QueueFree();
    }

    public Godot.Collections.Dictionary<string, object> Save()
    {
        return new Godot.Collections.Dictionary<string, object>()
        {
            { "Filename", this.Filename },
            { "Parent", this.GetParent().GetPath() },
            { "ScaleX", this.Scale.x },
            { "ScaleY", this.Scale.y },
            { "PosX", Position.x }, // Vector2 is not supported by JSON
            { "PosY", Position.y },
            { "Label", unitLabel }
        };
    }

    public void Load(Godot.Collections.Dictionary<string, object> config)
    {
        this.Position = new Vector2((float)config["PosX"], (float)config["PosY"]);
        this.Scale = new Vector2((float)config["ScaleX"], (float)config["ScaleY"]);
        this.SetUnitLabel((string)config["Label"]);
    }
}
