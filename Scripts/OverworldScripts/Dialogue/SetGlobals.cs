using Ink.Runtime;
using UnityEngine;

public class SetGlobals : MonoBehaviour
{
    public TextAsset inkJson;
    public void ChangeVariable(string name, string value)
    {
        Story story = new(inkJson.text);
        OverWorldManager.instance.dialogueVariables.StartListening(story);
        story.variablesState[name] = value;
        OverWorldManager.instance.dialogueVariables.StopListening(story);
    }

}
