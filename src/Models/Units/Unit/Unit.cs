using Godot;
using System.Collections.Generic;

public class Unit : Node2D, IMoving, IPersistant
{
    // Declare member variables here. Examples:
    public int vitality = 100;
    public int attack = 10;
    public ProgressBar healthBar;
    public ProgressBar statusBar;
    private float _speed = 10.0f;
    private string _unitLabel;

    public IList<IExecutable<Unit>> tasks = new List<IExecutable<Unit>>();
    public string UnitLabel
    {
        get => this._unitLabel;
        set
        {
            this._unitLabel = value;
            this.GetNode<Label>("Label").Text = value;
        }
    }

    public KnowledgeBase KnowledgeBase { get; set; }

    [Signal]
    public delegate void UnitInput(InputEvent @event, Unit unit);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.healthBar = this.GetNode<ProgressBar>("HealthBar");
        this.statusBar = this.GetNode<ProgressBar>("StatusBar");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        this.healthBar.Value = vitality;

        if (tasks.Count > 0 && tasks[0].Execute(this, delta))
        {
            tasks.RemoveAt(0);
        }
    }

    public override void _Input(InputEvent @event)
    {
        EmitSignal(nameof(UnitInput), @event, this);
    }

    public IExecutable<Unit> GetNextTask()
    {
        return (tasks.Count > 0) ? tasks[0] : null;
    }

    public void AddTask(IExecutable<Unit> task)
    {
        tasks.Add(task);
    }

    public bool Hit(int damage)
    {
        this.vitality -= damage;
        if (this.vitality <= 0)
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
            { "Label", _unitLabel }
        };
    }

    public void Load(Godot.Collections.Dictionary<string, object> config)
    {
        this.Position = new Vector2((float)config["PosX"], (float)config["PosY"]);
        this.Scale = new Vector2((float)config["ScaleX"], (float)config["ScaleY"]);
        this.UnitLabel = (string)config["Label"];
    }

    public float GetSpeed()
    {
        return this._speed;
    }
}