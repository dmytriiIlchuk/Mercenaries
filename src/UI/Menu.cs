using Godot;
using System;

public class Menu : Node2D
{
    public override void _Ready()
    {
    }

    public void StartGame()
    {
        this.GetTree().ChangeScene(ResourcePath.Models.Main.MainScenePath);
    }

    public void StartEditor()
    {
        this.GetTree().ChangeScene(ResourcePath.Editor.EditorScenePath);
    }

    public void Exit()
    {
        this.GetTree().Quit();
    }
}
