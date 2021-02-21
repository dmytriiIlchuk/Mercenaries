using Godot;
using System;

public class Stone : Area2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddToGroup("stones");
    }
}
