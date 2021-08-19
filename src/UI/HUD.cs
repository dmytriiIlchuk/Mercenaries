using Godot;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void ConstructBuildingSignal(Building building);

    public override void _Ready()
    {
        this.GetNode<MenuButton>("CommandBar/BuildMenu").GetPopup().Connect("id_pressed", this, nameof(OnIdPressed));

    }

    public void BackToMenu()
    {
        this.GetTree().ChangeScene(ResourcePath.UI.MenuScenePath);
    }

    public void OnIdPressed(int id)
    {
        foreach (Unit unit in this.GetTree().GetNodesInGroup("Workers"))
        {
            if (!unit.IsConnected(nameof(ConstructBuildingSignal), unit, nameof(unit.onConstructBuildingSignal)))
            {
                this.Connect(nameof(ConstructBuildingSignal), unit, nameof(unit.onConstructBuildingSignal));
            }
        };
        World world = this.GetTree().Root.GetNode<World>("Main/World");

        Building house = (Building)GameObjectFactory.MakeObject(BuildingConfig.Default, world, new Vector2(0f, 0f));

        EmitSignal("ConstructBuildingSignal", house);
    }
}