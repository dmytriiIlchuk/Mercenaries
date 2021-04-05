using Godot;
using System;

public class HUD : CanvasLayer
{
    public override void _Ready()
    {
    }

    public void BackToMenu()
    {
        this.GetTree().ChangeScene(ResourcePath.UI.MenuScenePath);
    }
}
