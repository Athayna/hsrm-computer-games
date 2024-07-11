using System.Collections;
using UnityEngine;

public class NPC : InteractableBase
{
    private string[] text;
    [SerializeField] protected RuntimeAnimatorController idleAnimation = null;
    [SerializeField] protected RuntimeAnimatorController talkAnimation = null;
    [SerializeField] protected Animator animator = null;
    protected override void Start()
    {
        base.Start();
        text = new string[4];
        text[0] = "I heard there is a treasure in the forest, protected by an invincible troll.";
        text[1] = "Maybe the retired mage by the church can help you.";
        text[2] = "If you are going to get that treasure, don't let the troll see you.";
        text[3] = "Welcome to our little village.";
    }

    public override void Interact()
    {
        Debug.Log("Interacting with NPC");
        int i = Random.Range(0, 4);
        FindObjectOfType<MainSceneManager>().Dialogue(text[i]);
        base.Interact();
        StartCoroutine(Talk());
    }

    IEnumerator Talk()
    {
        animator.runtimeAnimatorController = talkAnimation;
        yield return new WaitForSeconds(3f);
        animator.runtimeAnimatorController = idleAnimation;
    }

    public override string ValidateInteract()
    {
        return "Talk";
    }
}
