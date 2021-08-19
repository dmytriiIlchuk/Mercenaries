using Godot;

public class Building : GameObject
{
    public bool Completed { get; private set; }

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
                this.Completed = true;
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
        this.Modulate = Colors.Yellow;
    }

    public void Construct(int constructionPoints)
    {
        this.ConstructionProgress += constructionPoints;
        if (this.Completed)
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
        this.ConstructionPoints= config.ConstructionPoints;
        this.Label = gameObjectConfig.Name;
        this.ConstructionProgress = 0;
    }
}