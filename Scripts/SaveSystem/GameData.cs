using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPos;
    public Inv playerInv;
    public int money;

    public GameData()
    {
        money = 0;
        playerPos = Character_Controller.playerVec;
    }
}
