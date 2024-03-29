using Godot;
using System.Collections.Generic;

public class Unit : GameObject, IMoving, IPersistant, IHittable, IAttacking, ISelectable
{
    // Declare member variables here. Examples:
    public int vitality = 100;
    public int attack = 10;
    public ProgressBar healthBar;
    public ProgressBar progressBar;
    private float _speed = 50.0f;
    private string _label;
    private CollisionObject2D body;

    [Signal]
    public delegate void ConstructBuildingSignal(BuildingType building);

    public IList<IExecutable<Unit>> tasks = new List<IExecutable<Unit>>();
    public string Label
    {
        get => this._label;
        set
        {
            this._label = value;
            this.GetNode<Label>("Label").Text = value;
        }
    }

    public KnowledgeBase KnowledgeBase { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public void onConstructBuildingSignal(Building building)
    {
        this.AddTask(ObjectiveProvider.ConstructBuildingObjective<Unit>(building));
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

    public void OnInput(Node viewport, InputEvent @event, int shape_idx)
    {
        if (@event is InputEventMouseButton)
        {
            InputEventMouseButton inputEventMouseButton = (InputEventMouseButton)@event;

            if (inputEventMouseButton.IsPressed() && inputEventMouseButton.ButtonIndex == (int)ButtonList.Left)
            {
                if (this.IsSelected())
                {
                    this.Deselect();
                }
                else
                {
                    this.Select();
                }
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
            { "Label", _label }
        };
    }

    public void Load(Godot.Collections.Dictionary<string, object> config)
    {
        this.Position = new Vector2((float)config["PosX"], (float)config["PosY"]);
        this.Scale = new Vector2((float)config["ScaleX"], (float)config["ScaleY"]);
        this.Label = (string)config["Label"];
        this.Initialize(UnitConfig.Default);
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
        this.AddToGroup(Groups.Selected);
        this.Modulate = Colors.Red;
    }

    public void Deselect()
    {
        this.RemoveFromGroup(Groups.Selected);
        this.Modulate = Colors.White;
    }

    public bool IsSelected()
    {
        return this.IsInGroup(Groups.Selected);
    }

    public override void Initialize(GameObjectConfig gameObjectConfig)
    {
        this.healthBar = this.GetNode<ProgressBar>("HealthBar");
        this.progressBar = this.GetNode<ProgressBar>("ProgressBar");
        this.body = this.GetNode<CollisionObject2D>("Body");
        string name = nameof(OnInput);
        this.body.Connect("input_event", this, name);

        UnitConfig config = (UnitConfig)gameObjectConfig;
        this._label = config.Name;
        foreach (string group in config.Groups)
        {
            this.AddToGroup(group);
        }
    }
}