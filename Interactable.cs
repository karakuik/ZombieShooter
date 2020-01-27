using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;  // in case we have objects that can only be interacted from the front (treasure chests)
    
    public string buttonPickup;

    public Transform player;

    bool hasInteracted = false;

    private void OnEnable()
    {
        player = FindObjectOfType<Player>().transform;
    }

    // The method is virtual so we can overwritte it for each type of interactable
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + interactionTransform.name);
    }

    private void Update()
    {
        if (!hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            //if (distance <= radius && Input.GetButtonDown(buttonPickup))
            if (distance <= radius && Input.GetButtonDown(buttonPickup))
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
