using System.Collections.Generic;
using UnityEngine;

public class SOTestBattleScript : MonoBehaviour
{
    public List<GameObject> deck = new List<GameObject>();
    public Transform cardPlace;
    public int handSize = 5;
    private int listNr = 0;

    public void Awake()
    {
        randomizeCards();
    }
    public void spawnCard()
    {
        Debug.Log($"Spawned hand");
        for (int j = 0; j < handSize; j++)
        {
            var card = Instantiate(deck[listNr], cardPlace); //creates a card in an empty UI gameobject
            if (listNr < deck.Count - 1)
            {
                listNr++;
            }
            else
            {
                listNr = 0;
                randomizeCards();
            }
        }
    }
    public void randomizeCards()
    {
        Debug.Log("deck radomized");
        for (int i = 0; i < deck.Count; i++)
        {
            GameObject temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
    public void destroyHand()
    {
        foreach (Transform child in cardPlace)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
