using UnityEngine;

public class EQContextMenuButtons : MonoBehaviour
{
    // public GameObject contextPanel;
    GameObject EQcontextmenu;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EQcontextmenu = GameObject.Find("EQcontextmenu");

            //EQcontextmenu.SetActive(true);
            EQcontextmenu.transform.position = new Vector3(transform.position.x, transform.position.y + 30, transform.position.z);


            //Button []buttons= EQcontextmenu.GetComponents<Button>();

            ////modify drop and equip button to fit be based on object
            //if(buttons[0].name == "Equip")
            //{

            //}
        }
    }

    //public void closePanel()
    //{
    //    contextPanel.SetActive(false);
    //}
}
