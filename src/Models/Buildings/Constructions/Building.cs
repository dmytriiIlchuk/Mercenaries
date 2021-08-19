using Godot;

public class Building : GameObject
{
    [Signal]
    public delegate void ConstructBuildingSignal(Building building);

    private enum BuildingState
    {
        Blueprint,
        Placed,
        Completed
    }

    private BuildingState _state;

    private BuildingState State
    {
        get => this._state;
        set
        {
            this._state = value;
            switch (value)
            {
                case BuildingState.Blueprint:
                    this.Modulate = Colors.Blue;
                    break;
                case BuildingState.Placed:
                    this.Modulate = Colors.Red;
                    this._labelNode.Visible = false;
                    break;
                case BuildingState.Completed:
                    this.Modulate = Colors.White;
                    this._labelNode.Visible = true;
                    break;
            }
        }
    }

    public int ConstructionPoints { get; private set; }

    private int _constructionProgress;
    private int ConstructionProgress
    {
        get => this._constructionProgress;
        set
        {
            this._constructionProgress = value;
            this.progressBar.Value = 100 * (this._constructionProgress + 0.0) / ConstructionPoints;

            if (this._constructionProgress >= ConstructionPoints)
            {
                this._constructionProgress = ConstructionPoints;
                this.State = BuildingState.Completed;
                this.progressBar.Visible = false;
                _labelNode.Visible = true;
            }
        }
    }

    private Label _labelNode;
    private string _label;

    public ProgressBar progressBar;

    public string Label
    {
        get => this._label;
        set
        {
            this._label = value;
            _labelNode.Text = value;
        }
    }

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(float delta)
    {
        if (State == BuildingState.Blueprint)
        {
            this.Position = GetGlobalMousePosition();
        }
    }

    public void Construct(int constructionPoints)
    {
        this.ConstructionProgress += constructionPoints;
        if (this.IsCompleted())
        {
            this.Modulate = Colors.White;
        }
    }

    public override void Initialize(GameObjectConfig gameObjectConfig)
    {
        // Nodes
        this._labelNode = this.GetNode<Label>("Label");
        this.progressBar = this.GetNode<ProgressBar>("ProgressBar");

        // Config
        BuildingConfig config = (BuildingConfig)gameObjectConfig;
        this.ConstructionPoints = config.ConstructionPoints;
        this.Label = gameObjectConfig.Name;
        this.ConstructionProgress = 0;
        this.State = BuildingState.Blueprint;
    }

    public void OnBuildingPlaced()
    {
        if (this.State == BuildingState.Blueprint)
        {
            this.State = BuildingState.Placed;

            this.Modulate = Colors.Red;

            foreach (Unit unit in this.GetTree().GetNodesInGroup("Workers"))
            {
                if (!unit.IsConnected(nameof(ConstructBuildingSignal), unit, nameof(unit.onConstructBuildingSignal)))
                {
                    this.Connect(nameof(ConstructBuildingSignal), unit, nameof(unit.onConstructBuildingSignal));
                }
            };

            EmitSignal("ConstructBuildingSignal", this);
        }
    }

    public bool IsCompleted()
    {
        return this.State == BuildingState.Completed;
    }

    public bool IsPlaced()
    {
        return this.State == BuildingState.Placed;
    }
}