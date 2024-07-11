using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractableManager : MonoBehaviour
{
    private List<InteractableBase> interactables;

    private void Awake()
    {
        interactables = FindObjectsOfType<InteractableBase>(true).ToList();

        foreach (InteractableBase interactable in interactables) interactable.OnInteracted.AddListener(HandleInteracted);

        foreach (InteractableBase interactable in interactables) Debug.Log("Interactable added: " + interactable.name);
    }

    private void HandleInteracted(InteractableBase interactable)
    {   
        if (interactables.Contains(interactable) && interactable.UniqueInteraction)
        {
            Debug.Log("Removing " + interactable.name + " from interactables");
            interactables.Remove(interactable);
            interactable.OnInteracted.RemoveListener(HandleInteracted);

            // destroy interactable if it is collectable.
            if (interactable.Collectable)
            {
                Debug.Log("Destroying " + interactable.name);
                interactable.gameObject.GetComponent<Collider>().enabled = false;
                interactable.gameObject.GetComponent<MeshRenderer>().enabled = false;
                foreach (MeshRenderer renderer in interactable.gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.enabled = false;
                }
                // wait for sound to finish playing
                Destroy(interactable.gameObject, 1.5f);
                return;
            }

            // disable trigger colliders if interactable is not collectable but has a unique interaction.
            foreach (Collider collider in interactable.GetComponents<Collider>())
            {
                if (collider.isTrigger)
                {
                    Debug.Log("Disabling trigger collider on " + interactable.name);
                    collider.enabled = false;
                }
            }
        }
    }
}
