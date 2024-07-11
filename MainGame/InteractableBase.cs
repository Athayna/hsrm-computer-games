using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableBase : MonoBehaviour
{
    protected Inventory inventory = null;
    protected bool uniqueInteraction;
    public bool UniqueInteraction => uniqueInteraction;
    protected bool collectable = false;
    public bool Collectable => collectable;
    public UnityEvent<InteractableBase> OnInteracted = null;

    protected virtual void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public virtual void Interact()
    {
        OnInteracted?.Invoke(this);
    }

    public virtual void Collect(string name)
    {
        inventory.Add(name);
    }

    public virtual void Drop(string name, int quantity)
    {
        inventory.Remove(name, quantity);
    }

    public abstract string ValidateInteract();
}
