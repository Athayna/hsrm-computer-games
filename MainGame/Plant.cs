using UnityEngine;

public class Plant : InteractableBase
{
    [SerializeField] private AudioSource audioSource = null;

    protected override void Start()
    {
        base.Start();
        uniqueInteraction = true;
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Burn, BURN!");
        audioSource.Play();
        audioSource.SetScheduledEndTime(AudioSettings.dspTime+(4.0f));
        transform.GetChild(0).gameObject.SetActive(true);
        Destroy(gameObject, 4.0f);
    }

    public override string ValidateInteract()
    {
        if (inventory.GetItemCount("FireSpell") != 1) return "";
        return "Burn Plant";
    }
}
