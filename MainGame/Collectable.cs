using UnityEngine;

public class Collectable : InteractableBase
{
    [SerializeField] private AudioSource audioSource = null;

    protected override void Start()
    {
        base.Start();
        uniqueInteraction = true;
        collectable = true;
    }

    public override void Interact()
    {
        if (audioSource != null) audioSource.Play();
        base.Interact();
        base.Collect(name);
    }

    public override string ValidateInteract()
    {
        return "Collect";
    }
}
