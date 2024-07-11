using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerManager : MonoBehaviour
{
    [SerializeField] private GameObject interactPrompt = null;
    [SerializeField] private AudioClip footstepGround = null;
    [SerializeField] private AudioClip footstepStone = null;
    [SerializeField] private AudioSource audioSource = null;
    private List<InteractableBase> interactables = new List<InteractableBase>();


    private void OnTriggerEnter(Collider other)
    {
        // change footstep sound when entering village
        if (other.gameObject.name == "Village")
        {
            audioSource.clip = footstepStone;
            return;
        }

        // only continue if the object is interactable
        if (!other.gameObject.TryGetComponent(out InteractableBase interactable))
        {
            return;
        }

        // if the interaction is not valid, don't show the prompt and stop
        if (interactable.ValidateInteract() == "")
        {
            interactPrompt.SetActive(false);
            return;
        }

        interactPrompt.GetComponentInChildren<TextMeshProUGUI>().text = interactable.ValidateInteract();
        interactPrompt.SetActive(true);

        interactables.Add(interactable);

        Debug.Log("Triggered " + interactable.name);
    }

    private void OnTriggerExit(Collider other)
    {
        // change footstep sound when leaving village
        if (other.gameObject.name == "Village")
        {
            audioSource.clip = footstepGround;
            return;
        }

        // only continue if the object is interactable
        if (!other.gameObject.TryGetComponent(out InteractableBase interactable))
        {
            return;
        }

        interactables.Remove(interactable);

        interactPrompt.SetActive(false);
    }

    void Update()
    {
        if (interactables.Count > 0 && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacted with " + interactables[0].name);

            interactPrompt.SetActive(false);

            // interact with the first interactable that was triggered and remove it from the list
            interactables[0].Interact();
            interactables.Remove(interactables[0]);

            if (interactables.Count == 0)
            {
                return;
            }

            // show the prompt for the next interactable if there is one
            interactPrompt.GetComponentInChildren<TextMeshProUGUI>().text = interactables[0].ValidateInteract();
            interactPrompt.SetActive(true);
        }
    }
}
