using UnityEngine.Events;

public class Mage : NPC
{
    public UnityEvent<string, bool> OnMageInteracted = null;

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        base.Interact();
        FindObjectOfType<MainSceneManager>().Dialogue("I can teach you a spell that will help you get around the troll. But I need a favor first. Collect 5 Berries for me and I will teach you the spell.");
        OnMageInteracted?.Invoke("Mage", true);

        if (inventory.GetItemCount("Bush") == 5)
        {
            inventory.Remove("Bush", 5);
            inventory.Add("InvisibilitySpell");
            FindObjectOfType<MainSceneManager>().Dialogue("This spell will turn you invisible when you approach the troll. Be careful out there.");
        }
    }
}