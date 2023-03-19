using System.Collections.Generic;
using UnityEngine;

public class OpenLootChest : MonoBehaviour
{
    public int goldReward;
    public List<GlobalRune.Type> runeRewards;
    private bool playerInRange;
    [SerializeField] private GameObject interactableCue;
    Animator _animator;


    public EquipmentInventoryObject inventory;
    public ItemObject amulet;
    public ItemObject helmet;
    public ItemObject chestpiece;
    public ItemObject leggings;
    public ItemObject boots;

    public genericResourceScript metalresource;


    void Awake()
    {
        playerInRange = false;
        interactableCue.SetActive(false);
        _animator = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if (playerInRange)
        {
            interactableCue.SetActive(true);
            if (Input.GetKeyDown("e"))
            {
                GiveLoot();
                _animator.SetBool("Opened", true);
                interactableCue.SetActive(false);
                Destroy(gameObject);
            }
        }
        else
        {
            interactableCue.SetActive(false);
        }
    }
    public void GiveLoot()
    {
        GameObject player = GameObject.Find("hero");
        player.GetComponent<Character_Controller>().AddMoney(goldReward);
        //foreach (GlobalRune.Type type in runeRewards)
        //{
        //    GlobalRune.addRune(type, 1);
        //}



        //add a single random rune
        int runetyp = Random.Range(0, 10);
        GlobalRune.addRune((GlobalRune.Type)runetyp, 1);

        //generate a random item
        int rand = Random.Range(1, 6);

        if (rand == 1)
            inventory.AddItem(new Item(amulet), 1);
        if (rand == 2)
            inventory.AddItem(new Item(helmet), 1);
        if (rand == 3)
            inventory.AddItem(new Item(chestpiece), 1);
        if (rand == 4)
            inventory.AddItem(new Item(leggings), 1);
        if (rand == 5)
            inventory.AddItem(new Item(boots), 1);

        //add metal
        int met = Random.Range(1, 5);
        metalresource.addAmount(met);

        foreach (Quest q in QuestManager.GetInstance().activeQuests)
        {
            for (int i = 1; i <= met; i++)
            {
                q.goal.ItemCollected(GatherType.Metal);
            }
        }
        GameEvents.instance.UpdateTrackedQuestTrigger();

        LootPopUpManager.instance.PopUpLoot("METAL", met);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerInRange = false;
        }
    }
}
