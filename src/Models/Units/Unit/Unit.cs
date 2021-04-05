using Godot;
using System.Collections.Generic;

public class Unit : GameObject, IMoving, IPersistant, IHittable, IAttacking, ISelectable
{
    // Declare member variables here. Examples:
    public int vitality = 100;
    public int attack = 10;
    public ProgressBar healthBar;
    public ProgressBar statusBar;
    private float _speed = 50.0f;
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

    public IExecutable<Unit> GetNextTask()
    {
        return (tasks.Count > 0) ? tasks[0] : null;
    }

    public void AddTask(IExecutable<Unit> task)
    {
        tasks.Add(task);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton)
        {
            InputEventMouseButton inputEventMouseButton = (InputEventMouseButton)@event;

            if (inputEventMouseButton.ButtonIndex == (int)ButtonList.Left)
            {
                this.Select();
            }
        }
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

    public void MoveTo(Vector2 to, float delta)
    {
        this.Position = this.Position.MoveToward(to, delta * this._speed);
    }

    public bool At(Vector2 target, float distance)
    {
        return this.Position.DistanceSquaredTo(target) <= distance * distance;
    }

    public void Attack(IHittable target)
    {
        target.Hit(this.attack);
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

    public bool IsDead()
    {
        return vitality <= 0;
    }

    public void Select()
    {
        this.AddToGroup("selected");
        this.Modulate = Color.Color8(255, 0, 0);
    }

    public void Deselect()
    {
        this.RemoveFromGroup("selected");
        this.Modulate = Color.Color8(255, 255, 255);
    }
}