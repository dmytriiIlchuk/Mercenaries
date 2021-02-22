using Godot;
using System;

public class Editor : Node2D
{
    Node2D world;
    CreateUnit createUnitButton;
    PutFloor putFloorButton;
    Action<Node2D, Vector2> action;

    public override void _Ready()
    {
        this.createUnitButton = this.GetNode<CreateUnit>("HUD/Panel/CreateUnit");

        LoadCreateUnitConfig();

        this.world = this.GetNode<Node2D>("World");
    }

    public void LoadCreateUnitConfig()
    {
        this.createUnitButton.AddItem("worker", 1);
        this.createUnitButton.AddItem("swordsman", 2);
    }

    public void LoadPutFloorConfig()
    {
        this.putFloorButton.AddItem("grass", 1);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("click"))
        {
            action(this, GetGlobalMousePosition());
        }
    }

    public void _on_CreateUnit_item_selected(int id)
    {
        this.action = GameObjectFactory.MakeUnit;
    }
}
