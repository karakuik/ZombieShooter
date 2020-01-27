using System.Collections;
using UnityEngine;

public class NPC_DropItems : MonoBehaviour
{
    private NPC_Master npcMaster;
    private ZombieSpawner zombieSpawner;

    public GameObject[] itemsToDrop;

    private float delay = 15;   // amount of time the item is going to be active before it is destroyed
    
    //PickupItemsManager pickupItemsManager;

    //public GameObject pickupItems;  // parent of items

    void OnEnable()
    {
        SetInitialReferences();
        npcMaster.EventNpcDie += DropItems;
        //pickupItemsManager = FindObjectOfType<PickupItemsManager>();
    }

    void OnDisable()
    {
        npcMaster.EventNpcDie -= DropItems;
    }

    void SetInitialReferences()
    {
        npcMaster = GetComponent<NPC_Master>();
        zombieSpawner = FindObjectOfType<ZombieSpawner>();
    }

    void DropItems()
    {
        // Drop chance is currently 50% 
        float dropChance = Random.Range(0, 100);
        Debug.Log("DropChance: " + dropChance);

        Debug.Log("Number of enemies: " + zombieSpawner.numEnemy);
        
        // Drop items if the enemy has items and the drop chance is higher than 50 OR there is only one enemy left
        if ((itemsToDrop.Length > 0 && dropChance > 50) || zombieSpawner.numEnemy == 1)
        { 
            foreach (GameObject item in itemsToDrop)
            {
                StartCoroutine(PauseBeforeDrop(item));
            }
        }
    }

    /*
     * Subscribes to NPC event die
     * When the NPC dies then each item it was carrying will go through the corroutine
     */
    IEnumerator PauseBeforeDrop(GameObject itemToDrop)
    {
        // If the item is the key AND it is not the last enemy -> deactivate the item
        if (itemToDrop.name == "Key" && zombieSpawner.numEnemy != 1)
        {
            Debug.Log("Last enemy");
            itemToDrop.SetActive(false);
        }
        else
        {
            itemToDrop.SetActive(true);
        }
        // Set pickupitems empty gameobject as parent
        //itemToDrop.transform.parent = pickupItems.transform;
        itemToDrop.transform.parent = null;

        // Update pickupitems list in PickupItemsManager
        //pickupItemsManager.pickupItemsList.Add(itemToDrop);

        yield return new WaitForSeconds(0.05f);
    }
}

