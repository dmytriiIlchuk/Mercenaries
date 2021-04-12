using Godot;
using System.Collections.Generic;

public class Main : Node
{
    private World world;
    private KnowledgeBase knowledgeBase = new KnowledgeBase();

    public override void _Ready()
    {
        world = this.GetNode<World>("World");
        // LoadGame();
        SetupScene();
    }

    private void SetupScene()
    {
        float xLimit = 50;
        float yLimit = 50;
        Engine.TimeScale = 1.0f;

        Formation formation = (Formation)GameObjectFactory.MakeObject(world, new Vector2(xLimit, yLimit)() {
            ObjectType = GameObjectType.Formation,
        });

        Formation formation1 = GameObjectFactory.MakeObject(world, new Vector2(xLimit, yLimit+200));

        formation.tasks.Add(ObjectiveProvider.AttackTargetObjective<Formation, Formation>(formation1, null, 3));
        //Unit target = GameObjectFactory.MakeUnit(world, new Vector2(xLimit, yLimit), UnitType.Swordsman, knowledgeBase);
        //Unit unit = GameObjectFactory.MakeUnit(world, new Vector2(xLimit - 100, yLimit - 100), UnitType.Swordsman, knowledgeBase);
        //knowledgeBase.Units.Add(target);
        //knowledgeBase.Units.Add(unit);
        //unit.AddTask(ObjectiveProvider.AttackTargetObjective(target, unit.statusBar, 3));
        //target.AddTask(ObjectiveProvider.AttackTargetObjective(unit, target.statusBar, 3));
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

            GetNode(nodeData["Parent"].ToString().Replace("Editor", "Main")).AddChild(newObject);

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
