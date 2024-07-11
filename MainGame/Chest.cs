using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableBase
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private List<Collectable> collectables = null;

    protected override void Start()
    {
        base.Start();
        uniqueInteraction = true;
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Nice, some loot!");
        audioSource.Play();
        if (animator.GetBool("IsOpen") == false) animator.SetBool("IsOpen", true);

        // enables all trigger colliders on collectables when chest is opened
        foreach (Collectable collectable in collectables)
        {
            foreach (Collider collider in collectable.GetComponents<Collider>())
            {
                if (collider.isTrigger)
                {
                    collider.enabled = true;
                }
            }
        }
    }

    public override string ValidateInteract()
    {
        return "Open";
    }
}
