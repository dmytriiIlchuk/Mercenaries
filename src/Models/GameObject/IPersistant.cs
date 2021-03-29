using Godot.Collections;

interface IPersistant
{
    Dictionary<string, object> Save();

    void Load(Dictionary<string, object> config);
}