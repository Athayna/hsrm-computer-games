using UnityEngine.Events;

namespace Assets.Scripts
{
    public class Portal : InteractableBase
    {
        public UnityEvent<string, bool> OnPortalFinished = null;

        protected override void Start()
        {
            base.Start();
            uniqueInteraction = true;
        }

        // TODO: Play animations inserted stones and change Scene to finish screen.
        public override void Interact()
        {
            base.Interact();
            OnPortalFinished?.Invoke("Portal", true);

            transform.GetChild(0).gameObject.SetActive(true);
        }

        public override string ValidateInteract()
        {
            if (inventory.GetItemCount("RuneStone") != 3) return "";
            return "Insert Rune Stones";
        }
    }
}