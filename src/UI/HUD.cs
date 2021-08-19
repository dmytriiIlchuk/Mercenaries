using Godot;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void MouseClick();

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
        World world = this.GetTree().Root.GetNode<World>("Main/World");

        Building building = (Building)GameObjectFactory.MakeObject(BuildingConfig.Default, world, new Vector2(0f, 0f));

        this.Connect(nameof(MouseClick), building, nameof(building.OnBuildingPlaced));
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton)
        {
            switch ((ButtonList)eventMouseButton.ButtonIndex)
            {
                case ButtonList.Left:
                    EmitSignal(nameof(MouseClick));
                    break;
            }
        }
    }

}