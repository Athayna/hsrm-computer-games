using System.Collections;
using UnityEngine;

public class TrollArea : MonoBehaviour
{
    [SerializeField] private GameObject troll = null;
    [SerializeField] private Animator trollAnimator = null;
    [SerializeField] private MainSceneManager mainSceneManager = null;
    private Vector3 position;
    private Quaternion rotation;
    private bool attack = false;

    void Start()
    {
        troll.transform.GetPositionAndRotation(out position, out rotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out Player player))
        {
            return;
        }

        if (other.gameObject.GetComponent<Inventory>().GetItemCount("InvisibilitySpell") == 1)
        {
            player.SetInvisibility(true);
            return;
        }

        trollAnimator.SetTrigger("run");
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out Player player))
        {
            return;
        }

        if (other.gameObject.GetComponent<Inventory>().GetItemCount("InvisibilitySpell") == 1)
        {
            return;
        }

        troll.transform.LookAt(player.transform);

        if (attack) return;

        if (Vector3.Distance(troll.transform.position, player.transform.position) < 3f)
        {
            attack = true;
            StartCoroutine(Attack(player));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out Player player))
        {
            return;
        }

        player.SetInvisibility(false);

        trollAnimator.SetTrigger("idle");
        troll.transform.SetPositionAndRotation(position, rotation);
    }

    IEnumerator Attack(Player player)
    {
        player.GetComponent<ThirdPersonPlayerController>().enabled = false;

        trollAnimator.SetTrigger("attack1");

        yield return new WaitForSeconds(1.667f);

        mainSceneManager.GameOver();

        attack = false;
    }
}
