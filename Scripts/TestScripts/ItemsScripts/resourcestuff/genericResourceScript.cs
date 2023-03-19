using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "resource", menuName = "ScriptableObjects/resourceObject", order = 1)]


public class genericResourceScript : ScriptableObject
{
    public int amount;

    public int getAmount()
    {
        return amount;
    }
    public void addAmount(int amt)
    {
        amount += amt;
    }
    public void removeAmount(int amt)
    {
        amount -= amt;
        if (amount < 0)
            amount = 0;
    }

    public void resetAmt()
    {
        amount = 0;
    }
}
