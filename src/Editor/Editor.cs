using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Editor : Node2D
{
    Node2D world;
    CreateUnit createUnitButton;
    PutFloor putFloorButton;
    Action<Vector2> action;
    Floor floor;

    private bool dragging = false;

    public override void _Ready()
    {
        this.createUnitButton = this.GetNode<CreateUnit>("HUD/Panel/CreateUnit");
        LoadCreateUnitConfig();

        this.putFloorButton = this.GetNode<PutFloor>("HUD/Panel/PutFloor");
        LoadPutFloorConfig();

        this.world = this.GetNode<Node2D>("World");
        this.floor = this.GetNode<Floor>("World/Floor");
    }

    public void LoadCreateUnitConfig()
    {
        foreach (UnitType type in Enum.GetValues(typeof(UnitType)))
        {
            this.createUnitButton.AddItem(type.ToString(), (int)type);
        }
    }

    public void LoadPutFloorConfig()
    {
        this.putFloorButton.AddItem("grass", 1);
        this.putFloorButton.AddItem("grass", 2);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton && action != null)
        {
            switch ((ButtonList)eventMouseButton.ButtonIndex)
            {
                case ButtonList.Left:
                    action(GetGlobalMousePosition());
                    if (!dragging && eventMouseButton.Pressed)
                    {
                        dragging = true;
                    }
                    break;
            }

            if (dragging && !eventMouseButton.Pressed)
            {
                dragging = false;
            }
        }
        if (@event is InputEventMouseMotion eventMouseMotion && action != null && dragging)
        {
            action(GetGlobalMousePosition());
        }
    }

    public void _on_CreateUnit_item_selected(int id)
    {
        this.action = (Vector2 position) => {
            Unit unit = (Unit)GameObjectFactory.MakeObject(UnitConfig.Default, world, position);
            unit.Connect("UnitInput", this, nameof(OnUnitInput));
        };
    }

    public void OnUnitInput(InputEvent @event, Unit unit)
    {
        if (@event is InputEventMouseButton eventMouseButton && action != null)
        {
            switch ((ButtonList)eventMouseButton.ButtonIndex)
            {
                case ButtonList.Right:
                    unit.QueueFree();
                    break;
            }

            if (dragging && !eventMouseButton.Pressed)
            {
                dragging = false;
            }
        }
        if (@event is InputEventMouseMotion eventMouseMotion && action != null && dragging)
        {
            action(GetGlobalMousePosition());
        }
    }

    public void _on_PutFloor_item_selected(int id)
    {
        this.action = (Vector2 position) => floor.SetCellvFromGlobal(position, id);
    }

    public void SaveGame()
    {
        var saveGame = new File();
        saveGame.Open("user://savegame.save", File.ModeFlags.Write);

        var saveNodes = GetTree().GetNodesInGroup("Persist");
        foreach (Node saveNode in saveNodes)
        {
            // Check the node is an instanced scene so it can be instanced again during load.
            if (saveNode.Filename.Empty())
            {
                GD.Print(String.Format("persistent node '{0}' is not an instanced scene, skipped", saveNode.Name));
                continue;
            }

            // Check the node has a save function.
            if (!saveNode.HasMethod("Save"))
            {
                GD.Print(String.Format("persistent node '{0}' is missing a Save() function, skipped", saveNode.Name));
                continue;
            }

            // Call the node's save function.
            var nodeData = saveNode.Call("Save");

            // Store the save dictionary as a new line in the save file.
            saveGame.StoreLine(JSON.Print(nodeData));
        }

        saveGame.Close();
    }

    public void LoadGame()
    {
        var saveGame = new File();
        if (!saveGame.FileExists("user://savegame.save"))
            return; // Error!  We don't have a save to load.

        // We need to revert the game state so we're not cloning objects during loading.
        // This will vary wildly depending on the needs of a project, so take care with
        // this step.
        // For our example, we will accomplish this by deleting saveable objects.
        var saveNodes = GetTree().GetNodesInGroup("Persist");
        foreach (Node saveNode in saveNodes)
            saveNode.QueueFree();

        // Load the file line by line and process that dictionary to restore the object
        // it represents.
        saveGame.Open("user://savegame.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);

            // Firstly, we need to create the object and add it to the tree and set its position.
            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (Node2D)newObjectScene.Instance();
            GD.Print((float)nodeData["PosX"]);

            GD.Print((float)nodeData["PosY"]);

            ((IPersistant)newObject).Load(nodeData);

            GetNode(nodeData["Parent"].ToString()).AddChild(newObject);

            // Now we set the remaining variables.
            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key;
                if (key == "Filename" || key == "Parent" || key == "PosX" || key == "PosY")
                    continue;
                newObject.Set(key, entry.Value);
            }
        }

        saveGame.Close();
    }
}
