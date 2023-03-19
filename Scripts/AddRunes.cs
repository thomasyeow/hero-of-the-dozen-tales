using System.Collections.Generic;
using UnityEngine;

public class AddRunes : MonoBehaviour
{

    public List<GlobalRune.Type> runesToAdd;

    private void Start()
    {
        AddingRunes();
    }


    public void AddingRunes()
    {
        foreach (GlobalRune.Type t in runesToAdd)
        {
            GlobalRune.addRune(t, 1);
        }
    }
}
