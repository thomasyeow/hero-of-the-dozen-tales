using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLog : MonoBehaviour
{
    public static List<string> Eventlog = new List<string>();
    public Text guiText;

    private static int lognum = 1;                      //the action counter
    private static int turnnum = 0;                     //the turn counter
    private int maxLines = 100;                      //max lines showed in log before lines start being deleted

    //pushupdate and updated are used to update the log (pushupdate is incremented when a new change happens, and updated is incremented when the change is showed)
    private static int pushupdate = 0;
    private static int updated = 0;
    private static bool colorB = false;

    private void Start()
    {
        GUIStyle style = new GUIStyle();
        style.richText = true;
        guiText.supportRichText = true;
        Eventlog.Clear();
        guiText.text = "";
        addEvent("nextturn");
    }

    private void Update()
    {
        if (pushupdate != updated)
        {
            showEvent();
            updated = pushupdate;
        }

    }


    //void OnGUI()
    //{
    //    GUI.Label(new Rect(0, Screen.height - (Screen.height / 3), Screen.width, Screen.height / 3), guiText, GUI.skin.textArea);
    //}

    private void showEvent()
    {
        if (Eventlog.Count >= maxLines)
            Eventlog.RemoveAt(0);

        guiText.text = "";


        foreach (string logEvent in Eventlog)
        {
            guiText.text += logEvent;
            guiText.text += "\n";
        }
    }

    public static void addEvent(string eventString)
    {
        if (eventString == "nextturn")
        {
            colorB = colorB != true;

            if (colorB == false)
                Eventlog.Insert(0, "<color=#0A00C4>                                ------- Turn " + turnnum + " -------</color>");
            else
                Eventlog.Insert(0, "<color=#000000>                                ------- Turn " + turnnum + " -------</color>");

            turnnum++;
        }
        else
        {
            if (colorB == false)
                Eventlog.Insert(0, "<color=#0A00C4>" + lognum + ":  " + eventString + " </color>");
            else
                Eventlog.Insert(0, "<color=#000000>" + lognum + ":  " + eventString + " </color>");

            lognum++;
        }

        //     Debug.Log("added log:    " + Eventlog[Eventlog.Count-1]);

        pushupdate++;
    }

    public static void resetLog()
    {
        Eventlog.Clear();
        colorB = false;
        pushupdate = 1;
        lognum = 1;
        turnnum = 0;
        updated = 1;
    }
}
