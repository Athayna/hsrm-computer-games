using UnityEngine.Events;

public class OldMan : NPC
{
    public UnityEvent<string, bool> OnOldManInteracted = null;

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        base.Interact();
        FindObjectOfType<MainSceneManager>().Dialogue("When I was a young man I used to be an adventurer like you. I hid a rune stone under a big rock in the forest. If you find the combination to my chest, you will find a telekinesis spell to move the rock.");
        OnOldManInteracted?.Invoke("OldMan", true);
    }

    public override string ValidateInteract()
    {
        return "Talk";
    }
}