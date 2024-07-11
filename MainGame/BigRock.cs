using UnityEngine;

public class BigRock : InteractableBase
{
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private Animator animator = null;

    protected override void Start()
    {
        base.Start();
        uniqueInteraction = true;
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Rumble, rumble");
        audioSource.Play();
        audioSource.SetScheduledEndTime(AudioSettings.dspTime+(4.0f));
        if (animator.GetBool("IsMoved") == false) animator.SetBool("IsMoved", true);
    }

    public override string ValidateInteract()
    {
        if (inventory.GetItemCount("TelekinesisSpell") != 1) return "";
        return "Move rock";
    }
}
